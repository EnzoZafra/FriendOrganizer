using Autofac;
using FriendOrganizer.UI.Startup;
using System;
using System.Windows;

namespace FriendOrganizer.UI
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Manually injecting dependencies
            //var mainWindow = new MainWindow(
            //    new ViewModel.MainViewModel(
            //        new FriendDataService()));

            // autoFac
            var bootstrapper = new Bootstrapper();
            var container = bootstrapper.Bootstrap();
            // Resolve will see MainWindow needs MainViewModel which needs IFriendDataService
            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }


        // Processing unhandled exceptions
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Unexpected error occured. Please contact your admin." +
                Environment.NewLine + e.Exception.Message, "Unexpected Error");

            e.Handled = true;
        }
    }
}
