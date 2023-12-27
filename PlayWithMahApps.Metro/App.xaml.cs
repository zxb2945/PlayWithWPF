using PlayWithMahApps_Metro.Views;
using Prism.Ioc;
using System.Windows;

namespace PlayWithMahApps_Metro
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
            containerRegistry.RegisterForNavigation<DRWindows>();
            containerRegistry.RegisterForNavigation<NRWindows>();
            containerRegistry.RegisterForNavigation<Template>();
        }
    }
}
