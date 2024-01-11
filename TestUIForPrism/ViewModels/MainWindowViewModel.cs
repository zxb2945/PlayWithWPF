using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using TestUIForPrism.Views;

namespace TestUIForPrism.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Please Name Tool";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        /// <summary>
        /// Region Navigation Controller in Prism Framework
        /// </summary>
        private readonly IRegionManager _regionManager;

        public DelegateCommand AnalyzerCommand { get; private set; }
        public DelegateCommand MakerCommand { get; private set; }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            this._regionManager = regionManager;

            AnalyzerCommand = new DelegateCommand(Analyzer);
            MakerCommand = new DelegateCommand(Maker);
        }


        void Analyzer()
        {
            if (_regionManager != null)
            {
                _regionManager.RequestNavigate("ContentRegion", "Analyzer");
            }
        }

        void Maker()
        {
            if (_regionManager != null)
            {
                _regionManager.RequestNavigate("ContentRegion", "Maker");
            }
        }
    }
}

