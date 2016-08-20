using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using Launcher.Properties;

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

            var request = (HttpWebRequest)WebRequest.Create(new Uri(Properties.Settings.Default.launcherUpdates));
            var response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            NewsBox.AppendText(sr.ReadToEnd());
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Hyperlink_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            var hyperlink = (Hyperlink)sender;
            Process.Start(hyperlink.NavigateUri.ToString());
        }

        private void rectangle1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Cursor = Cursors.SizeAll;
                DragMove();
                Cursor = Cursors.Arrow;
            }
        }
    }
}
