using System;
using System.IO;
using System.Windows;
using System.Text.RegularExpressions;

namespace Launcher
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {


        public Settings()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.autologin = (autologin.IsChecked == true) ? true : false;
            Properties.Settings.Default.username = user.Text;
            Properties.Settings.Default.password = pass.Password;
            Properties.Settings.Default.progressBarType = progressType.SelectedIndex;
            Properties.Settings.Default.gameFolder = path.Text;
            switch (speedType.SelectedIndex)
            {
                case 2:
                    Properties.Settings.Default.downloadSpeedLimit = Convert.ToInt64(speedValue.Text) * 1024 * 1024 * 1024;
                    break;
                case 1:
                    Properties.Settings.Default.downloadSpeedLimit = Convert.ToInt64(speedValue.Text) * 1024 * 1024;
                    break;
                case 0:
                    Properties.Settings.Default.downloadSpeedLimit = Convert.ToInt64(speedValue.Text) * 1024;
                    break;
            }
            Properties.Settings.Default.Save();

            MainWindow m = Owner as MainWindow;
            if (m.anyDownloads)
            {
                m.currentBytes2 = 0;
                m.currentFileBytes2 = 0;

                m.sw.Stop();
                m.sw.Reset();
                m.sw.Start();
            }
            Close();
        }

        private string detectSize(long value)
        {
            try
            {
                if (int.Parse(value.ToString()) >= 1073741824)
                {
                    speedType.SelectedIndex = 2;
                    return string.Format("{0:0}", double.Parse(value.ToString()) / 1024 / 1024 / 1024);
                    
                }
                else if (int.Parse(value.ToString()) >= 1048576)
                {
                    speedType.SelectedIndex = 1;
                    return string.Format("{0:0}", double.Parse(value.ToString()) / 1024 / 1024); 
                }
                else
                {
                    speedType.SelectedIndex = 0;
                    return string.Format("{0:0}", double.Parse(value.ToString()) / 1024);
                }
            }
            catch
            {
                return "∞ Б";
            }
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            user.Text = Properties.Settings.Default.username;
            pass.Password = Properties.Settings.Default.password;
            autologin.IsChecked = Properties.Settings.Default.autologin;
            progressType.SelectedIndex = Properties.Settings.Default.progressBarType;
            path.Text = Properties.Settings.Default.gameFolder;
            speedValue.Text = detectSize(Properties.Settings.Default.downloadSpeedLimit);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folder = new System.Windows.Forms.FolderBrowserDialog();
            folder.Description = "Выберите папку с клиентом игры";
            folder.RootFolder = Environment.SpecialFolder.MyComputer;
            folder.ShowNewFolderButton = false;
            System.Windows.Forms.DialogResult result = folder.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                if (File.Exists(folder.SelectedPath.ToString() + "\\Wow.exe"))
                {
                    string folderPath = folder.SelectedPath.ToString();
                    path.Text = folderPath;
                }
                else
                { MessageBox.Show("Файл \"Wow.exe\" не найден!\nПожалуйста выберите корректную папку с игрой!", "Ошибка выбора папки", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }

        private void resetPath_Click(object sender, RoutedEventArgs e)
        {
            path.Text = "Не задано";
        }

        private void btn_del_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить все загруженные файлы сервера?", "Подтверждение удаления", MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                MainWindow m = Owner as MainWindow;
                m.DPatches();
            }

        }

        private void speedValue_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9]+");
            return !regex.IsMatch(text);
        }
    }
}
