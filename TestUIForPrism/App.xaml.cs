using Prism.Ioc;
using System.Diagnostics.Metrics;
using System.Windows;
using TestUIForPrism.Views;

namespace TestUIForPrism
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
            containerRegistry.RegisterForNavigation<Analyzer>();
            containerRegistry.RegisterForNavigation<Maker>();
        }
    }
}
