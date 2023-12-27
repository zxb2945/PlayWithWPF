using MahApps.Metro.Controls;
using Prism.Regions;
using System.Windows;

namespace PlayWithMahApps_Metro.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            /* HamburgerMenu.Content 上に配置したコントロールは遅延作成されるらしく、
             * Prism の RegionManager は遅延作成された Region を認識できないため RequestNavigate が認識されない
             * これを回避するには Region を手動で RegionManager に登録する必要があるようなので、MainWindow のコードビハインドに 上記の処理を追加します。*/
            RegionManager.SetRegionName(this.ContentRegion, "ContentRegion");

            var regionMan = (IRegionManager)Prism.Ioc.ContainerLocator.Container.Resolve(typeof(IRegionManager));
            RegionManager.SetRegionManager(ContentRegion, regionMan);


        }
    }
}
