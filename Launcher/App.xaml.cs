using System.Threading;
using System.Windows;

namespace Launcher
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App
    {
        static Mutex _instanceMutex;
        protected override void OnStartup(StartupEventArgs e)
        {
            bool createdNew;

            _instanceMutex = new Mutex(true, "F37E84CB-D76A-49B1-A1AC-80870903087B", out createdNew);
            if (!createdNew)
            {
                MessageBox.Show("Копия лаунчера уже запущена на данном компьютере", "Попытка повторного запуска", MessageBoxButton.OK, MessageBoxImage.Warning);
                _instanceMutex = null;
                Current.Shutdown();
                return;
            }
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (_instanceMutex != null)
                _instanceMutex.ReleaseMutex();
            base.OnExit(e);
        }
    }
}
