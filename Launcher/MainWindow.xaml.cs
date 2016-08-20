// Данный програмный продукт является результатом труда и стараний Jumper'а
// Все права и копирайты закреплены за ним
// (c) 2011-2016 год
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

        #region The Classic Window API
        //SetWindowLong lets you set a window style
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, long dwNewLong);
        #endregion

        const int GWL_STYLE = -16;
        const long WS_POPUP = 2147483648;

        #region dll's;
        public const int WmNclbuttondown = 0xA1;
        public const int HtCaption = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        #endregion dll's;

        #region const;
        private Queue<PatchFileInfo> patchQueue = new Queue<PatchFileInfo>();
        private delegate void UpdateProgress(int percent, long bytesReceived, long totalBytesReceive);
        private delegate void MakeVisibleInvisible(bool visible);
        private delegate void MakeLoadTextIndicator(string text);
        private string tempPath = Path.GetTempFileName();
        public Stopwatch sw = new Stopwatch();
        int _count, _length;
        string _gPath, _pListUri, _pListDel;
        public bool anyDownloads;
        private long totalBytes = 0, currentBytes = 0, currentFileBytes = 0;
        public long currentBytes2 = 0, currentFileBytes2 = 0;

        #endregion const;

        private const uint WmKeydown = 0x0100;
        private const uint WmKeyup = 0x0101;
        private const uint WmChar = 0x0102;
        private const int VkReturn = 0x0D;
        private const int VkTab = 0x09;

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
        
        #endregion

        /// <summary>
        /// Patch file class
        /// </summary>
        private class PatchFileInfo
        {
            public string Url { get; set; }
            public string File { get; set; }
            public string FileName { get; set; }
            public string Md5Hash { get; set; }
            public long FileBytes { get; set; }

            /// <summary>
            /// Class initializer
            /// </summary>
            /// <param name="url">direct download url</param>
            /// <param name="file"></param>
            /// <param name="filename">Name of file</param>
            /// <param name="filebytes">File size in bytes</param>
            /// <param name="md5">MD5 hash</param>
            public PatchFileInfo(string url, string file, string filename, long filebytes, string md5)
            {
                Url = url;
                File = file;
                FileBytes = filebytes;
                FileName = filename;
                Md5Hash = md5;
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            Stream normalCursor = Application.GetResourceStream(new Uri("pack://application:,,,/img/cursors/wow.cur")).Stream;
            Stream readlCursor = Application.GetResourceStream(new Uri("pack://application:,,,/img/cursors/WOW-ESCRIVIR.cur")).Stream;
            Stream hightlCursor = Application.GetResourceStream(new Uri("pack://application:,,,/img/cursors/WOW-ENLACE-CURSOR.cur")).Stream;

            //this.Cursor = new Cursor(_normalCursor);
            MainGrid.Cursor = new Cursor(normalCursor);
            version.Cursor = new Cursor(hightlCursor);
            NewsBox.Cursor = new Cursor(readlCursor);

            SetProgressType(Properties.Settings.Default.progressBarType);

            if (CheckForInternetConnection())
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
        /// Internet connection checker
        /// </summary>
        /// <returns>Returns true if connection exist</returns>
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Start file sync by game folder (if it was set before)
        /// </summary>
        private void FileSync()
        {
            if (Properties.Settings.Default.gameFolder.Equals("Не задано"))
                if (File.Exists(string.Format(@"{0}\Wow.exe", AppDomain.CurrentDomain.BaseDirectory)))
                {
                    UpdateClient(AppDomain.CurrentDomain.BaseDirectory);
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Файл \"Wow.exe\" не найден!\nПожалуйста посместите программу в папку с игрой или укажите путь к папке с игрой!\n\nУказать путь сейчас?", "Ошибка местоположения", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    TryToFindFolder(result);
                }
            else
                UpdateClient(Properties.Settings.Default.gameFolder);
        }

        /// <summary>
        /// Show folder explorer to choose game path
        /// </summary>
        private void ShowFolderDialog()
        {
            FolderBrowserDialog folder = new System.Windows.Forms.FolderBrowserDialog();
            folder.Description = "Выберите папку с клиентом игры";
            folder.RootFolder = Environment.SpecialFolder.MyComputer;
            folder.ShowNewFolderButton = false;
            DialogResult result = folder.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                if (File.Exists(string.Format(@"{0}\Wow.exe", folder.SelectedPath.ToString())))
                {
                    string folderPath = folder.SelectedPath.ToString();
                    Properties.Settings.Default.gameFolder = folderPath;
                    Properties.Settings.Default.Save();
                    FileSync();
                }
                else
                {
                    MessageBoxResult retryResult = MessageBox.Show("В выбранной папке файл \"Wow.exe\" не найден!\nПожалуйста выберите корректную папку с игрой!\n\nПовторить попытку выбора?", "Ошибка выбора папки", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    TryToFindFolder(retryResult);
                }
            }
            else
                Application.Current.Shutdown();
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
            FileVersionInfo clientVersion = FileVersionInfo.GetVersionInfo(String.Format(@"{0}\Wow.exe", gPath));
            string[] v = clientVersion.FileVersion.Split(char.Parse(","));

            // Client version detection. Select right patch files to downloading and deletion
            switch (v[3]) {
                case " 12340":
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
                    MessageBoxResult result = MessageBox.Show("Для игры на сервере %SERVER-NAME% требуется клиент версии 3.3.5.12340! Поместите программу в корректную папку с игрой или укажите путь к папке!\n\nУказать путь сейчас?", "Ошибка версии клиента", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    TryToFindFolder(result);
                    break;
            }

            string rPath = String.Format(@"{0}\Data\ruRU\realmlist.wtf", gPath);
            string cPath = String.Format(@"{0}\Cache", gPath);
            _gPath = gPath;

            try
            {
                Directory.Delete(cPath, true);
            }
            catch (Exception)
            { }

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(Properties.Settings.Default.realmlistURL));
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream());
                string realmlist = sr.ReadToEnd();

                //TODO: COMMENT NEXT PART OF CODE IF CLIENT DOESN'T HAVE REALMLIST
                #region <= 3.3.5 realmlist changer
                StreamWriter writer = new StreamWriter(rPath);
                writer.WriteLine(realmlist);
                writer.Flush();
                writer.Close();
                #endregion

                //TODO: UNCOMMENT NEXT PART OF CODE IF CLIENT DOESN'T HAVE REALMLIST
                #region >= 3.3.5 realmlist changer
                /*string line;

                var reader = new StreamReader(rPath);

                var builder = new StringBuilder();

                while ((line = reader.ReadLine()) != null)
                    builder.AppendLine(line.ToLower().Contains("set realmlist") ? realmlist : line);

                reader.Close();

                var writer = new StreamWriter(rPath);
                writer.Write(builder.ToString());
                writer.Flush();
                writer.Close();*/
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
            news_box.SetNews(Properties.Settings.Default.launcherNewsFileUrl);
			
			AutoUpdate();
            IsEnabled = true;
        }
		
        /// <summary>
        /// Delete old patches or old evential updates
        /// </summary>
        private void DeleteOldPatches()
        {
            var request = (HttpWebRequest)WebRequest.Create(new Uri(_pListDel));
            var response = (HttpWebResponse)request.GetResponse();

            var reader = new StreamReader(response.GetResponseStream());

            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string path = SetPath(line);

                try
                {
                    File.Delete(path);
                }
                catch (Exception)
                { }
            }

            LoadNews();
        }

        /// <summary>
        /// Set path to files from update list
        /// </summary>
        /// <param name="item">File to download for update</param>
        /// <returns>Full path to downloadable file</returns>
        private string SetPath(string item)
        {
            string path;
            if (item.ToLower().Contains(".mpq") && item.Contains("-ruRU-"))
                path = String.Format(@"{0}\Data\ruRU\{1}", _gPath, item);
            else
                path = String.Format(@"{0}\Data\{1}", _gPath, item);
            if (item.ToLower().Contains(".exe"))
                path = String.Format(@"{0}\{1}", _gPath, item);
            return path;
        }

        /// <summary>
        /// Starts get update list for self or game client
        /// </summary>
        private void AutoUpdate()
        {
            var request = (HttpWebRequest)WebRequest.Create(new Uri(Properties.Settings.Default.launcherVersionUrl));
            var response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            string newVersion = sr.ReadToEnd();

            Assembly assem = Assembly.GetExecutingAssembly();
            AssemblyName assemName = assem.GetName();
            Version ver = assemName.Version;

            string currentVersion = ver.ToString();
            version.Content = "ver. " + currentVersion;

            DownloadBar.Visibility = Visibility.Hidden;

            if (newVersion.Contains(currentVersion))
            {
                if (Properties.Settings.Default.PatchDownloadURL != string.Empty)
                {
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
            }
            else
            {
                LNewVer lver = new LNewVer();
                lver.ShowDialog();
            }
        }

        private void startDownloadBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var client = new WebClient();

            client.DownloadFileCompleted += client_DownloadFileCompleted;

            client.DownloadFileAsync(new Uri(_pListUri), tempPath);

            sw.Start();
        }

        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            _length = File.ReadAllLines(tempPath).Length;

            using (StreamReader reader = new StreamReader(tempPath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] ex = line.Split(char.Parse("#"));
                    string path = SetPath(ex[1]);

                    bool proceed = true;

                    if (File.Exists(path))
                    {
                        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                        MD5 md5 = new MD5CryptoServiceProvider();
                        byte[] retVal = md5.ComputeHash(fs);
                        fs.Close();
                        StringBuilder sb = new StringBuilder();
                        foreach (byte b in retVal)
                        {
                            sb.Append(string.Format("{0:X2}", b));
                        }

                        if (ex[2] == sb.ToString())
                        {
                            proceed = false;
                            _count++;
                        }
                    }

                    if (proceed)
                    {
                        string[] link = ex[0].Split(char.Parse("Ø"));
                        long pingValue = long.MaxValue;
                        string bestLink = string.Empty;

                        foreach (string curLink in link)
                        {
                            System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
                            System.Net.NetworkInformation.PingReply pingReply = ping.Send(curLink);
                            if (pingReply.RoundtripTime < pingValue)
                            {
                                bestLink = curLink;
                                pingValue = pingReply.RoundtripTime;
                            }
                        }

                        PatchFileInfo pfi = new PatchFileInfo(bestLink, path, ex[1], Convert.ToInt64(ex[3]), ex[2]);
                        patchQueue.Enqueue(pfi);
                        totalBytes += Convert.ToInt64(ex[3]);
                    }
                }
            }


            while (patchQueue.Count != 0)
            {
                sw.Start();
                anyDownloads = true;
                Dispatcher.Invoke(new MakeVisibleInvisible(DownloadCompleted), new object[] { true });

                PatchFileInfo pfi = patchQueue.Dequeue();
                long existLen = 0;
                bool append = false;
                string currFile = string.Format("{0}.{1}.upd", pfi.File, pfi.Md5Hash);

                var httpReq = (HttpWebRequest)WebRequest.Create(pfi.Url);

                if (File.Exists(currFile))
                {
                    FileInfo destinationFileInfo = new FileInfo(currFile);
                    existLen = destinationFileInfo.Length;

                    currentFileBytes += existLen;
                    currentBytes += existLen;
                    httpReq.AddRange((int)existLen);
                    append = true;
                }

                var httpRes = (HttpWebResponse)httpReq.GetResponse();
                Stream resStream = httpRes.GetResponseStream();

                using (var file = (append == true) ? new FileStream(currFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite) : new FileStream(currFile, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                {
                    int bufferSize = 1024*4;
                    var buffer = new byte[bufferSize];
                    int bytesReceived;

                    while ((bytesReceived = resStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        file.Write(buffer, 0, bytesReceived);

                        try
                        {
                            Dispatcher.Invoke(new UpdateProgress(UpdateProgressbar), new object[] { 0, (int)bytesReceived, Convert.ToInt32(pfi.FileBytes) });

                            if (Properties.Settings.Default.downloadSpeedLimit > 0)
                            {
                                if (currentFileBytes2 * 1000L / sw.Elapsed.TotalMilliseconds > Properties.Settings.Default.downloadSpeedLimit)
                                {
                                    long wakeElapsed = currentFileBytes2 * 1000L / Properties.Settings.Default.downloadSpeedLimit;
                                    int toSleep = (int)(wakeElapsed - sw.Elapsed.TotalMilliseconds);
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
                        catch (Exception) { }
                    }
                }

                _count++;

                File.Delete(pfi.File);
                File.Move(currFile, pfi.File);
                currentFileBytes = 0; currentFileBytes2 = 0;
            }

            Dispatcher.Invoke(new MakeVisibleInvisible(DownloadCompleted), new object[] { false });

            anyDownloads = false;
            sw.Stop();
            sw.Reset();

            var fileGenerationDir = new DirectoryInfo(_gPath);
            fileGenerationDir.GetFiles("*.upd", SearchOption.AllDirectories).ToList().ForEach(file => file.Delete());
        }

        private void UpdateProgressbar(int percent, long bytesReceived, long totalBytesToReceive)
        {
            string received;
            string total;
            string speed;

            currentBytes += bytesReceived;
            currentBytes2 += bytesReceived;
            currentFileBytes += bytesReceived;
            currentFileBytes2 += bytesReceived;

            int percentt = Convert.ToInt16((Convert.ToDouble(double.Parse(currentBytes.ToString(CultureInfo.InvariantCulture))) / 1024 / 1024) / (Convert.ToDouble(double.Parse(totalBytes.ToString(CultureInfo.InvariantCulture))) / 1024 / 1024 / 100));
            percent = Convert.ToInt16((Convert.ToDouble(double.Parse(currentFileBytes.ToString(CultureInfo.InvariantCulture))) / 1024 / 1024) / (Convert.ToDouble(double.Parse(totalBytesToReceive.ToString(CultureInfo.InvariantCulture))) / 1024 / 1024 / 100));

            TaskbarProgress.ProgressValue = Convert.ToDouble(percentt) / 100;
            progress.Value = percentt;
            progress_file.Value = percent;

            long dSpeed = (long)(Convert.ToDouble(double.Parse(currentBytes2.ToString(CultureInfo.InvariantCulture))) / sw.Elapsed.TotalSeconds);

            received = detectSize(currentBytes);
            total = detectSize(totalBytes);
            speed = detectSize(dSpeed);

            string downloaded = "Загружено (" + _count + "/" + _length + ") : " + received + "  /  " + total;

            double avaiting = (Convert.ToDouble(totalBytes - (currentBytes - currentBytes2)) / 1024) / (Convert.ToDouble(currentBytes2) / 1024 / sw.Elapsed.TotalSeconds) - (Convert.ToDouble(currentBytes2) / 1024) / (Convert.ToDouble(currentBytes2) / 1024 / sw.Elapsed.TotalSeconds);

            labelprogress.Content = downloaded + " (" + speed + "/с)" + " ~" + ((int)(avaiting / 3600)).ToString("0") + " ч " + ((int)(avaiting % 3600 / 60)).ToString("0") + " мин " + (avaiting % 3600 % 60).ToString("0") + " сек";

            //labelmsg.Content = "Оставшееся время: " + ((int)(avaiting / 3600)).ToString("0") + " ч " + ((int)(avaiting % 3600 / 60)).ToString("0") + " мин " + (avaiting % 3600 % 60).ToString("0") + " сек";
        }

        /// <summary>
        /// Convert bytes value to GB/MB/KB
        /// </summary>
        /// <param name="value">Byte value to convert into GB/MB/KB</param>
        /// <returns>Converted bytes value like GB/MB/KB</returns>
        private string detectSize(long value)
        {
            try
            {
                if (value >= 1073741824)
                    return string.Format("{0:0.00}ГБ", double.Parse(value.ToString()) / 1024 / 1024 / 1024);
                else if (value >= 1048576)
                    return string.Format("{0:0.00}МБ", double.Parse(value.ToString()) / 1024 / 1024);
                else
                    return string.Format("{0:0}КБ", double.Parse(value.ToString()) / 1024);
            }
            catch
            {
                return "∞ Б";
            }
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
                TaskbarProgress.ProgressState = System.Windows.Shell.TaskbarItemProgressState.Normal;
                TaskbarPlay.IsEnabled = false;
                btn_play.IsEnabled = false;
                labelmsg.Content = "Идет обновление...";
            }
            else
            {
                DownloadBar.Visibility = Visibility.Hidden;
                TaskbarProgress.ProgressState = System.Windows.Shell.TaskbarItemProgressState.None;
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
            _length = File.ReadAllLines(tempPath).Length;

            using (var reader = new StreamReader(tempPath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] ex = line.Split(char.Parse("#"));

                    string path = SetPath(ex[1]);

                    try
                    {
                        File.Delete(path);
                    }
                    catch
                    { }
                }

                MessageBox.Show("Все файлы успешно удалены", "Удаление файлов", MessageBoxButton.OK, MessageBoxImage.Information);

                btn_play.Visibility = Visibility.Hidden;

                labelmsg.Content = "Запуск клиента невозможен";
            }
        }

        /// <summary>
        /// Set download progress type like just total progress, just current progress, mixed progres
        /// </summary>
        /// <param name="index"></param>
        public void SetProgressType(int index)
        {
            switch (index)
            {
                // Mixed files download progress
                case (0):
                    totalProgressGrid.SetValue(Grid.RowProperty, 1);
                    totalProgressGrid.Visibility = Visibility.Visible;
                    currentProgressGrid.Visibility = Visibility.Visible;
                    break;
                // Total files download progress
                case (1):
                    totalProgressGrid.SetValue(Grid.RowProperty, 2);
                    totalProgressGrid.Visibility = Visibility.Visible;
                    currentProgressGrid.Visibility = Visibility.Hidden;
                    break;
                // Current file download progress
                case (2):
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
                var process = Process.Start(string.Format(@"{0}\{1}", _gPath, Properties.Settings.Default.clientExe));

                if (Properties.Settings.Default.autologin == true)
                {
                    NotifyIcon ni = new NotifyIcon();

                    ni.Icon = new System.Drawing.Icon(Application.GetResourceStream(new Uri("pack://application:,,,/img/101.ico")).Stream);
                    ni.Visible = true;
                    ni.ShowBalloonTip(2000, "Программа запуска", "Программа запуска продолжает работать в фоновом режиме. Чтобы развернуть ее, используйте двойной щелчек левой кнопки мыши", ToolTipIcon.Info);
                    this.Hide();
                    ni.DoubleClick +=
                        delegate(object sender, EventArgs args)
                        {
                            Show();
                            ni.Visible = false;
                        };

                    string accountName = Properties.Settings.Default.username;
                    Thread.Sleep(600);

                    new Thread(() =>
                    {
                        try
                        {
                            if (process != null)
                            {
                                Thread.CurrentThread.IsBackground = true;

                                while (!process.WaitForInputIdle())
                                {
                                }

                                Thread.Sleep(2000);

                                foreach (char accNameLetter in accountName)
                                {
                                    SendMessage(process.MainWindowHandle, WmChar, new IntPtr(accNameLetter), IntPtr.Zero);
                                    Thread.Sleep(30);
                                }

                                if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.password))
                                {
                                    SendMessage(process.MainWindowHandle, WmKeyup, new IntPtr(VkTab), IntPtr.Zero);
                                    SendMessage(process.MainWindowHandle, WmKeydown, new IntPtr(VkTab), IntPtr.Zero);

                                    foreach (char accPassLetter in Properties.Settings.Default.password)
                                    {
                                        SendMessage(process.MainWindowHandle, WmChar, new IntPtr(accPassLetter), IntPtr.Zero);
                                        Thread.Sleep(30);
                                    }

                                    SendMessage(process.MainWindowHandle, WmKeyup, new IntPtr(VkReturn), IntPtr.Zero);
                                    SendMessage(process.MainWindowHandle, WmKeydown, new IntPtr(VkReturn), IntPtr.Zero);
                                }
                                Thread.CurrentThread.Abort();
                            }
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
        /// <param name="window">Modal window to show</param>
        private void ShowModalWithEffect (Window window)
        {
            var blur = new BlurEffect();
            var current = Background;
            blur.Radius = 15;
            Effect = blur;

            if (window is Settings && anyDownloads)
            {
                (window as Settings).btn_del.IsEnabled = false;
                (window as Settings).resetPath.IsEnabled = false;
                (window as Settings).btn_del.ToolTip = "Не возможно выполнить\nво время процесса обновления";
            }

            window.Owner = this;
            window.ShowDialog();
            Effect = null;
            Background = current;
        }

        private void version_MouseDown(object sender, MouseButtonEventArgs e)
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
            SetProgressType(Properties.Settings.Default.progressBarType);
        }
                
        private void link_main_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Process.Start("http://your-link.domain");
        }

        private void link_cabinet_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Process.Start("http://your-link.domain");
        }

        private void link_registration_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Process.Start("http://your-link.domain");
        }

        private void link_social_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Process.Start("http://your-link.domain");
        }
    }
}
