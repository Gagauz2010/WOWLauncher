// Данный програмный продукт является результатом труда и стараний Jumper'а
// Все права и копирайты закреплены за ним
// (c) 2011-2019 год
//--------------------------------------------------------------------
//----### ------------------------------------------------------------
//----###---###-----##----#-------##----#######---#######---######----
//----###---###-----##----##------##-----##--###---##-------##---##---
//----###---###-----##----###----###-----##---##---##-------##---##---
//----###---###-----##---#--##--# ##-----##--##--- ##-###---##--##----
//----###---###-----##---#--##-##--##----##--------##-------##--##----
//----###----##----##----#---###---##----##--------##-------##---###--
//----###-----######----##----#----##---####------#######---###----###
//----## -------------------------------------------------------------
//---## ------------------------------------------------CODING FOR YOU

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Effects;
using System.Windows.Shell;
using Launcher.HelpClasses;
using Application = System.Windows.Application;
using Cursor = System.Windows.Input.Cursor;
using Cursors = System.Windows.Input.Cursors;
using MessageBox = System.Windows.MessageBox;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace Launcher
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        #region Variables

        #region dll's;

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);


        private const uint WmKeydown = 0x0100;
        private const uint WmKeyup = 0x0101;
        private const uint WmChar = 0x0102;
        private const int VkReturn = 0x0D;
        private const int VkTab = 0x09;

        #endregion dll's;

        #region const;
        private delegate void UpdateProgress(int percent, long bytesReceived, long totalBytesReceive);
        private delegate void MakeVisibleInvisible(bool visible);

        private readonly Queue<PatchFileInfo> _patchQueue = new Queue<PatchFileInfo>();

        private readonly string _tempPath = Path.GetTempFileName();
        private static readonly string AppRoamingPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
            Assembly.GetEntryAssembly()?.GetName().Name ?? "Launcher");
        private readonly string _updatePath = Path.Combine(AppRoamingPath, "UpdatedFiles.json");

        public Stopwatch StopWatch = new Stopwatch();

        private Dictionary<string, string> _filesCompleted = new Dictionary<string, string>();
        private int _count, _length;
        private string _gPath, _pListUri, _pListDel;
        public bool AnyDownloads;
        private long _totalBytes, _currentBytes, _currentFileBytes;
        public long CurrentBytes2, CurrentFileBytes2;

        #endregion const;

        #endregion

        /// <summary>
        /// Patch file class
        /// </summary>
        private class PatchFileInfo
        {
            public string Url { get; }
            public string Name { get; }
            public string File { get; }
            public string Md5Hash { get; }
            public long FileBytes { get; }

            /// <summary>
            /// Class initializer
            /// </summary>
            /// <param name="url">Direct download url</param>
            /// <param name="name">File name</param>
            /// <param name="file">Where to download file</param>
            /// <param name="fileBytes">File size in bytes</param>
            /// <param name="md5">MD5 hash</param>
            public PatchFileInfo(string url, string name, string file, long fileBytes, string md5)
            {
                Url = url;
                Name = name;
                Name = name;
                File = file;
                FileBytes = fileBytes;
                Md5Hash = md5;
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            var normalCursor = Application.GetResourceStream(new Uri("pack://application:,,,/img/cursors/wow.cur"))?.Stream;
            var readlCursor = Application.GetResourceStream(new Uri("pack://application:,,,/img/cursors/WOW-ESCRIVIR.cur"))?.Stream;
            var hightlCursor = Application.GetResourceStream(new Uri("pack://application:,,,/img/cursors/WOW-ENLACE-CURSOR.cur"))?.Stream;

            MainGrid.Cursor = new Cursor(normalCursor ?? throw new NullReferenceException("Не найден ресурс wow.cur"));
            version.Cursor = new Cursor(hightlCursor ?? throw new NullReferenceException("Не найден ресурс WOW-ESCRIVIR.cur"));
            NewsBox.Cursor = new Cursor(readlCursor ?? throw new NullReferenceException("Не найден ресурс WOW-ENLACE-CURSOR.cur"));

            SetProgressType(Properties.Settings.Default.ProgressBarType);

            Directory.CreateDirectory(AppRoamingPath);
            if (Utilities.Network.IsInternetConnectionAvailable())
            {
                FileSync();
            }
            else
            {
                MessageBox.Show("Невозможно подключиться к сети интернет, проверьте ваше подключение и повторите попытку", "Ошибка подключения", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        /// <summary>
        /// Start file sync by game folder (if it was set before)
        /// </summary>
        private void FileSync()
        {
            var storedPath = Properties.Settings.Default.GameFolder;
            if (File.Exists(Path.Combine(storedPath, "Wow.exe")))
            {
                UpdateClient(storedPath);
                return;
            }

            var localPath = AppDomain.CurrentDomain.BaseDirectory;
            if (File.Exists(Path.Combine(localPath, "Wow.exe")))
            {
                UpdateClient(localPath);
                return;
            }

            var result = MessageBox.Show("Файл \"Wow.exe\" не найден!\nПожалуйста посместите программу в папку с игрой или укажите путь к папке с игрой!\n\nУказать путь сейчас?", "Ошибка местоположения", MessageBoxButton.YesNo, MessageBoxImage.Question);
            TryToFindFolder(result);
        }

        /// <summary>
        /// Show folder explorer to choose game path
        /// </summary>
        private void ShowFolderDialog()
        {
            var folder = new FolderBrowserDialog
            {
                Description = @"Выберите папку с клиентом игры",
                RootFolder = Environment.SpecialFolder.MyComputer,
                ShowNewFolderButton = false
            };
            var result = folder.ShowDialog();

            if (result != System.Windows.Forms.DialogResult.OK)
            {
                Application.Current.Shutdown();
                return;
            }

            if (File.Exists(Path.Combine(folder.SelectedPath, "Wow.exe")))
            {
                var folderPath = folder.SelectedPath;
                Properties.Settings.Default.GameFolder = folderPath;
                Properties.Settings.Default.Save();
                FileSync();
                return;
            }

            var retryResult = MessageBox.Show("В выбранной папке файл \"Wow.exe\" не найден!\nПожалуйста выберите корректную папку с игрой!\n\nПовторить попытку выбора?", "Ошибка выбора папки", MessageBoxButton.YesNo, MessageBoxImage.Question);
            TryToFindFolder(retryResult);
        }

        /// <summary>
        /// Let user to choose game folder if launcher start not in game root folder
        /// </summary>
        /// <param name="retryResult">MessageBox result for </param>
        private void TryToFindFolder(MessageBoxResult retryResult)
        {
            switch (retryResult)
            {
                case MessageBoxResult.Yes:
                    ShowFolderDialog();
                    break;
                case MessageBoxResult.No:
                    Application.Current.Shutdown();
                    break;
            }
        }

        /// <summary>
        /// DETECT CLIENT VERSION AND SET UPDATE LIST, REALMLIST, DELETE CACHE
        /// </summary>
        /// <param name="gPath">Wow game root folder</param>
        private void UpdateClient(string gPath)
        {

            var clientVersion = FileVersionInfo.GetVersionInfo(Path.Combine(gPath, "Wow.exe"));
            var v = clientVersion.FileVersion.Split(char.Parse(","));

            // Client version detection. Select right patch files to downloading and deletion
            switch (v[3].Trim()) {
                case "12340":
                    _pListUri = Properties.Settings.Default.PatchDownloadURL;
                    _pListDel = Properties.Settings.Default.PatchToDelete;
                    break;
               
                // TODO: UNCOMMENT IF YOUR SERVER HAS DIFFERENT VERSION REALMS
                //case " another_client_build_version":
                
                //Create another string properties in project properties
                //    _pListUri = Properties.Settings.Default.PatchDownloadURL;
                //    _pListDel = Properties.Settings.Default.PatchToDelete;
                default:
                    //TODO: CHANGE SERVER NAME AND CLIENT VERSION
                    var result = MessageBox.Show("Для игры на сервере %SERVER-NAME% требуется клиент версии 3.3.5.12340! Поместите программу в корректную папку с игрой или укажите путь к папке!\n\nУказать путь сейчас?", "Ошибка версии клиента", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    TryToFindFolder(result);
                    break;
            }

            var rPath = Path.Combine(gPath, @"Data\ruRU\realmlist.wtf");
            var cPath = Path.Combine(gPath,  "Cache");
            _gPath = gPath;

            try
            {
                Directory.Delete(cPath, true);
            }
            catch (Exception)
            {
                // ignored
            }

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(new Uri(Properties.Settings.Default.RealmlistURL));
                var response = (HttpWebResponse)request.GetResponse();
                var sr = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException($@"Ошибка получения ответа от {Properties.Settings.Default.RealmlistURL}"));
                var realmlist = sr.ReadToEnd();

                var attributes = File.GetAttributes(rPath);
                if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
		        {
		            attributes = Utilities.File.RemoveAttribute(attributes, FileAttributes.ReadOnly);
		            File.SetAttributes(rPath, attributes);
		        } 

                //TODO: COMMENT NEXT PART OF CODE IF CLIENT DOESN'T HAVE REALMLIST
                #region <= 3.3.5 realmlist changer
                using (var writer = new StreamWriter(rPath))
                {
                    writer.WriteLine(realmlist);
                }
                #endregion

                //TODO: UNCOMMENT NEXT PART OF CODE IF CLIENT DOESN'T HAVE REALMLIST
                #region >= 3.3.5 realmlist changer
                /*

                var builder = new StringBuilder();

                using (var reader = new StreamReader(rPath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                        builder.AppendLine(line.ToLower().Contains("set realmlist") ? realmlist : line);
                }

                using (var writer = new StreamWriter(rPath))
                {
                    writer.Write(builder.ToString());
                } 

                */
                #endregion

                DeleteOldPatches();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException, ex.Source);
                Application.Current.Shutdown();
            }
        }

        /// <summary>
        /// Load news to NewsLoader Control
        /// </summary>
        private void LoadNews()
        {
            news_box.SetNews(Properties.Settings.Default.LauncherNewsFileUrl);
			
			AutoUpdate();
            IsEnabled = true;
        }
		
        /// <summary>
        /// Delete old patches or old eventual updates
        /// </summary>
        private void DeleteOldPatches()
        {
            var request = (HttpWebRequest)WebRequest.Create(new Uri(_pListDel));
            var response = (HttpWebResponse)request.GetResponse();

            var reader = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException($@"Ошибка получения ответа от {_pListDel}"));

            string line;

            while ((line = reader.ReadLine()) != null)
            {
                var path = Utilities.Updater.GetPath(_gPath, line);

                try
                {
                    File.Delete(path);
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            LoadNews();
        }

        private void SaveCurrentCompletedFiles()
        {
            using (var file = File.CreateText(_updatePath))
            {
                file.WriteLine(JsonSerializer<Dictionary<string, string>>.ToString(_filesCompleted));
            }
        }

        /// <summary>
        /// Starts get update list for self or game client
        /// </summary>
        private void AutoUpdate()
        {
            var request = (HttpWebRequest)WebRequest.Create(new Uri(Properties.Settings.Default.LauncherVersionUrl));
            var response = (HttpWebResponse)request.GetResponse();
            var sr = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException($@"Ошибка получения ответа от {Properties.Settings.Default.LauncherVersionUrl}"));
            var newVersion = sr.ReadToEnd();

            var assembly = Assembly.GetExecutingAssembly();
            var assemblyName = assembly.GetName();
            var ver = assemblyName.Version;

            var currentVersion = ver.ToString();
            version.Content = $@"ver. {currentVersion}";

            DownloadBar.Visibility = Visibility.Hidden;

            if (newVersion.Contains(currentVersion))
            {
                if (Properties.Settings.Default.PatchDownloadURL == string.Empty) return;

                var startDownloadBackgroundWorker = new BackgroundWorker();
                startDownloadBackgroundWorker.DoWork += startDownloadBackgroundWorker_DoWork;

                //DownloadBar.Visibility = Visibility.Visible;
                btn_play.IsEnabled = false;
                TaskbarPlay.IsEnabled = false;
                progress.Value = 0;
                labelmsg.Content = "Инициализация...";
                _count = 0;
                startDownloadBackgroundWorker.RunWorkerAsync();
            }
            else
            {
                var launcherNewVersion = new LNewVer();
                launcherNewVersion.ShowDialog();
            }
        }

        private void startDownloadBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var client = new WebClient();
            client.DownloadFileCompleted += client_DownloadFileCompleted;
            client.DownloadFileAsync(new Uri(_pListUri), _tempPath);
            StopWatch.Start();
        }

        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            _length = File.ReadAllLines(_tempPath).Length;

            Console.WriteLine($@"-----------------------------------------");
            Console.WriteLine($@"Start initialization in {DateTime.Now:HH:mm:ss.ffffff}.");
            Console.WriteLine($@"-----------------------------------------");

            // Get map of downloaded files
            if (File.Exists(_updatePath))
            {
                _filesCompleted = JsonSerializer<Dictionary<string, string>>.FromString(
                    File.ReadAllText(_updatePath));
            }

            using (var reader = new StreamReader(_tempPath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {

                    var ex = line.Split(char.Parse("#"));
                    var path = Utilities.Updater.GetPath(_gPath, ex[1]);
                    var pfi = new PatchFileInfo(ex[0], ex[1], path, Convert.ToInt64(ex[3]), ex[2]);

                    Console.WriteLine($@"[{_count + 1:000}/{_length:000}] Start checking {pfi.Name}.");

                    if (File.Exists(path))
                    {
                        if (_filesCompleted.TryGetValue(pfi.Name, out var hash) && hash == pfi.Md5Hash)
                        {
                            // Ignore downloaded file
                            _count++;
                            Console.WriteLine($@"{pfi.Name} is updated. {_length - _count} more left.");
                            continue;
                        }

                        var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                        MD5 md5 = new MD5CryptoServiceProvider();
                        var retVal = md5.ComputeHash(fs);
                        fs.Close();
                        var sb = new StringBuilder();
                        foreach (var b in retVal)
                        {
                            sb.Append($"{b:X2}");
                        }

                        if (pfi.Md5Hash == sb.ToString())
                        {
                            // Ignore matched file and add to completed map
                            _filesCompleted[pfi.Name] = pfi.Md5Hash;
                            _count++;
                            Console.WriteLine($@"Done with {pfi.Name}. {_length - _count} more left.");
                            SaveCurrentCompletedFiles();
                            continue;
                        }
                    }

                    _filesCompleted.Remove(pfi.Name);
                    _patchQueue.Enqueue(pfi);
                    _totalBytes += pfi.FileBytes;
                }
            }

            Console.WriteLine($@"--------------------------------------");
            Console.WriteLine($@"End initialization in {DateTime.Now:HH:mm:ss.ffffff}.");
            Console.WriteLine($@"--------------------------------------");

            while (_patchQueue.Count != 0)
            {
                StopWatch.Start();
                AnyDownloads = true;
                Dispatcher.Invoke(new MakeVisibleInvisible(DownloadCompleted), true);

                var pfi = _patchQueue.Dequeue();
                var append = false;
                var currentFile = $"{pfi.File}.{pfi.Md5Hash}.upd";

                var httpReq = (HttpWebRequest)WebRequest.Create(pfi.Url);

                if (File.Exists(currentFile))
                {
                    var destinationFileInfo = new FileInfo(currentFile);
                    var existedLength = destinationFileInfo.Length;

                    _currentFileBytes += existedLength;
                    _currentBytes += existedLength;
                    httpReq.AddRange((int)existedLength);
                    append = true;
                }

                try
                {
                    var httpRes = (HttpWebResponse)httpReq.GetResponse();
                    var resStream = httpRes.GetResponseStream();

                    using (var file = append ? new FileStream(currentFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite) : new FileStream(currentFile, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                    {
                        const int bufferSize = 1024 * 4;
                        var buffer = new byte[bufferSize];
                        int bytesReceived;

                        while (resStream != null && (bytesReceived = resStream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            file.Write(buffer, 0, bytesReceived);

                            try
                            {
                                Dispatcher.Invoke(new UpdateProgress(UpdateProgressbar), new object[] { 0, bytesReceived, Convert.ToInt32(pfi.FileBytes) });

                                if (Properties.Settings.Default.DownloadSpeedLimit > 0)
                                {
                                    if (CurrentFileBytes2 * 1000L / StopWatch.Elapsed.TotalMilliseconds > Properties.Settings.Default.DownloadSpeedLimit)
                                    {
                                        var wakeElapsed = CurrentFileBytes2 * 1000L / Properties.Settings.Default.DownloadSpeedLimit;
                                        var toSleep = (int)(wakeElapsed - StopWatch.Elapsed.TotalMilliseconds);
                                        if (toSleep > 1)
                                        {
                                            try
                                            {
                                                Thread.Sleep(toSleep);
                                            }
                                            catch (ThreadAbortException) { }
                                        }
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
                        }
                    }

                    _count++;

                    File.Delete(pfi.File);
                    File.Move(currentFile, pfi.File);
                    _filesCompleted[pfi.Name] = pfi.Md5Hash;
                    _currentFileBytes = 0; CurrentFileBytes2 = 0;
                    SaveCurrentCompletedFiles();
                }
                catch (Exception ex)
                {
                    var message = ex.Message;

                    if (ex is WebException)
                    {
                        message = $@"{pfi.Url}{Environment.NewLine}{Environment.NewLine}{ex.Message}";
                    }

                    MessageBox.Show(message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            Dispatcher.Invoke(new MakeVisibleInvisible(DownloadCompleted), false);

            AnyDownloads = false;
            StopWatch.Stop();
            StopWatch.Reset();

            new DirectoryInfo(_gPath).GetFiles("*.upd", SearchOption.AllDirectories).ToList().ForEach(file => file.Delete());
        }

        private void UpdateProgressbar(int percent, long bytesReceived, long totalBytesToReceive)
        {
            if (percent < 0) throw new ArgumentOutOfRangeException(nameof(percent));

            _currentBytes += bytesReceived;
            CurrentBytes2 += bytesReceived;
            _currentFileBytes += bytesReceived;
            CurrentFileBytes2 += bytesReceived;

            int percentTotal = Convert.ToInt16((Convert.ToDouble(double.Parse(_currentBytes.ToString(CultureInfo.InvariantCulture))) / 1024 / 1024) / (Convert.ToDouble(double.Parse(_totalBytes.ToString(CultureInfo.InvariantCulture))) / 1024 / 1024 / 100));
            percent = Convert.ToInt16((Convert.ToDouble(double.Parse(_currentFileBytes.ToString(CultureInfo.InvariantCulture))) / 1024 / 1024) / (Convert.ToDouble(double.Parse(totalBytesToReceive.ToString(CultureInfo.InvariantCulture))) / 1024 / 1024 / 100));

            TaskbarProgress.ProgressValue = Convert.ToDouble(percentTotal) / 100;
            progress.Value = percentTotal;
            progress_file.Value = percent;

            var dSpeed = (long)(Convert.ToDouble(double.Parse(CurrentBytes2.ToString(CultureInfo.InvariantCulture))) / StopWatch.Elapsed.TotalSeconds);

            var received = Utilities.Updater.DetectSize(_currentBytes);
            var total = Utilities.Updater.DetectSize(_totalBytes);
            var speed = Utilities.Updater.DetectSize(dSpeed);

            var downloaded = $@"Загружено ({_count}/{_length}) : {received} /  {total}";

            var awaiting = (Convert.ToDouble(_totalBytes - (_currentBytes - CurrentBytes2)) / 1024) / (Convert.ToDouble(CurrentBytes2) / 1024 / StopWatch.Elapsed.TotalSeconds) - (Convert.ToDouble(CurrentBytes2) / 1024) / (Convert.ToDouble(CurrentBytes2) / 1024 / StopWatch.Elapsed.TotalSeconds);

            labelprogress.Content = $@"{downloaded}  ({speed}/с) ~{((int)(awaiting / 3600)):0} ч {((int)(awaiting % 3600 / 60)):0} мин {(awaiting % 3600 % 60):0} сек";
        }

        /// <summary>
        /// Downloading done dispatcher event
        /// </summary>
        /// <param name="visible">Show download accesories or not</param>
        private void DownloadCompleted(bool visible)
        {
            if (visible)
            {
                DownloadBar.Visibility = Visibility.Visible;
                TaskbarProgress.ProgressState = TaskbarItemProgressState.Normal;
                TaskbarPlay.IsEnabled = false;
                btn_play.IsEnabled = false;
                labelmsg.Content = "Идет обновление...";
            }
            else
            {
                DownloadBar.Visibility = Visibility.Hidden;
                TaskbarProgress.ProgressState = TaskbarItemProgressState.None;
                TaskbarPlay.IsEnabled = true;
                labelmsg.Content = "Игра обновлена";
                btn_play.IsEnabled = true;
            }
        }

        /// <summary>
        /// Window moving event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed) return;
            Cursor = Cursors.SizeAll;
            DragMove();
            Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// Deleting all patches while whant leave server
        /// </summary>
        public void DPatches()
        {
            _length = File.ReadAllLines(_tempPath).Length;

            using (var reader = new StreamReader(_tempPath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    var ex = line.Split(char.Parse("#"));

                    var path = Utilities.Updater.GetPath(_gPath, ex[1]);

                    try
                    {
                        File.Delete(path);
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }

            File.Delete(_updatePath);

            MessageBox.Show("Все файлы успешно удалены", "Удаление файлов", MessageBoxButton.OK, MessageBoxImage.Information);
            btn_play.Visibility = Visibility.Hidden;
            labelmsg.Content = "Запуск клиента невозможен";
        }

        /// <summary>
        /// Set download progress type like just total progress, just current progress, mixed progres
        /// </summary>
        /// <param name="index"></param>
        public void SetProgressType(int index)
        {
            switch (index)
            {
                case 0:
                    totalProgressGrid.SetValue(Grid.RowProperty, 1);
                    totalProgressGrid.Visibility = Visibility.Visible;
                    currentProgressGrid.Visibility = Visibility.Visible;
                    break;
                case 1:
                    totalProgressGrid.SetValue(Grid.RowProperty, 2);
                    totalProgressGrid.Visibility = Visibility.Visible;
                    currentProgressGrid.Visibility = Visibility.Hidden;
                    break;
                case 2:
                    totalProgressGrid.Visibility = Visibility.Hidden;
                    currentProgressGrid.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void btn_play_Click(object sender, RoutedEventArgs e)
        {
            Play();
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Hyperlink_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            var hyperlink = (Hyperlink)sender;
            Process.Start(hyperlink.NavigateUri.ToString());
        }

        private void TaskbarPlay_Click(object sender, EventArgs e)
        {
            Play();
        }

        private void Play()
        {
            try
            {
                var process = Process.Start(string.Format(@"{0}\{1}", _gPath, Properties.Settings.Default.ClientExeName));

                if (Properties.Settings.Default.IsAutoLogin)
                {
                    var ni = new NotifyIcon
                    {
                        Icon = new Icon(
                            Application
                                .GetResourceStream(new Uri("pack://application:,,,/img/101.ico"))
                                ?.Stream 
                            ?? throw new NullReferenceException("Не найден ресурс 101.ico")
                            ),
                        Visible = true
                    };

                    ni.ShowBalloonTip(2000, "Программа запуска", "Программа запуска продолжает работать в фоновом режиме. Чтобы развернуть ее, используйте двойной щелчек левой кнопки мыши", ToolTipIcon.Info);
                    Hide();
                    ni.DoubleClick +=
                        delegate
                        {
                            Show();
                            ni.Visible = false;
                        };

                    var accountName = Properties.Settings.Default.Login;
                    Thread.Sleep(600);

                    new Thread(() =>
                    {
                        try
                        {
                            if (process == null) return;

                            Thread.CurrentThread.IsBackground = true;

                            while (!process.WaitForInputIdle())
                            {
                            }

                            Thread.Sleep(2000);

                            foreach (var accNameLetter in accountName)
                            {
                                SendMessage(process.MainWindowHandle, WmChar, new IntPtr(accNameLetter), IntPtr.Zero);
                                Thread.Sleep(30);
                            }

                            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Password))
                            {
                                SendMessage(process.MainWindowHandle, WmKeyup, new IntPtr(VkTab), IntPtr.Zero);
                                SendMessage(process.MainWindowHandle, WmKeydown, new IntPtr(VkTab), IntPtr.Zero);

                                foreach (var accPassLetter in Properties.Settings.Default.Password)
                                {
                                    SendMessage(process.MainWindowHandle, WmChar, new IntPtr(accPassLetter), IntPtr.Zero);
                                    Thread.Sleep(30);
                                }

                                SendMessage(process.MainWindowHandle, WmKeyup, new IntPtr(VkReturn), IntPtr.Zero);
                                SendMessage(process.MainWindowHandle, WmKeydown, new IntPtr(VkReturn), IntPtr.Zero);
                            }
                            Thread.CurrentThread.Abort();
                        }
                        catch
                        {
                            Thread.CurrentThread.Abort();
                        }
                    }).Start();
                }
                else
                    Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                if (ex.Message.Equals("Не удается найти указанный файл"))
                    ShowFolderDialog();
            }
        }

        /// <summary>
        /// Show WindowDialog with blur effects
        /// </summary>
        /// <param name="modalWindow">Modal window to show</param>
        private void ShowModalWithEffect (Window modalWindow)
        {
            var blur = new BlurEffect();
            var current = Background;
            blur.Radius = 15;
            Effect = blur;

            if (modalWindow is Settings settings && AnyDownloads)
            {
                settings.BtnDel.IsEnabled = false;
                settings.ResetPath.IsEnabled = false;
                settings.BtnDel.ToolTip = "Не возможно выполнить\nво время процесса обновления";
            }

            modalWindow.Owner = this;
            modalWindow.ShowDialog();
            Effect = null;
            Background = current;
        }

        private void Version_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Set motions only on Left button click
            if (e.LeftButton != MouseButtonState.Pressed) return;

            ShowModalWithEffect(new VerUpd());
        }

        private void btn_min_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btn_settings_Click(object sender, RoutedEventArgs e)
        {
            ShowModalWithEffect(new Settings());
            SetProgressType(Properties.Settings.Default.ProgressBarType);
        }
                
        private void link_main_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://your-link.domain");
        }

        private void link_cabinet_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://your-link.domain");
        }

        private void link_registration_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://your-link.domain");
        }

        private void link_social_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://your-link.domain");
        }
    }
}
