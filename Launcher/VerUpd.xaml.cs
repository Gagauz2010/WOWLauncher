using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace Launcher
{
    /// <summary>
    /// Логика взаимодействия для VerUpd.xaml
    /// </summary>
    public partial class VerUpd
    {
        public VerUpd()
        {
            InitializeComponent();

            var request = (HttpWebRequest)WebRequest.Create(new Uri(Properties.Settings.Default.LauncherUpdates));
            var response = (HttpWebResponse)request.GetResponse();
            var sr = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException($@"Ошибка получения ответа от {Properties.Settings.Default.LauncherUpdates}"));
            NewsBox.AppendText(sr.ReadToEnd());
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Hyperlink_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            var hyperlink = (Hyperlink)sender;
            Process.Start(hyperlink.NavigateUri.ToString());
        }
    }
}
