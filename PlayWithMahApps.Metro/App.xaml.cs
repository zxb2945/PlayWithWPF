using PlayWithMahApps.Metro.Views;
using Prism.Ioc;
using System.Windows;

namespace PlayWithMahApps.Metro
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
