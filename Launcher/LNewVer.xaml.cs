using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Diagnostics;

namespace Launcher
{
    /// <summary>
    /// Логика взаимодействия для LNewVer.xaml
    /// </summary>
    public partial class LNewVer
    {
        public LNewVer()
        {
            InitializeComponent();
        }

        private void Hyperlink_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            var hyperlink = (Hyperlink)sender;
            Process.Start(hyperlink.NavigateUri.ToString());
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
