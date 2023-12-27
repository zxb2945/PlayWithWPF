using ControlzEx.Theming;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace PlayWithMahApps_Metro.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public List<AppThemeMenuData> AppThemes { get; set; }

        public List<AccentColorMenuData> AccentColors { get; set; }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            // create metro theme color menu items for the demo
            AppThemes = ThemeManager.Current.Themes
                                         .GroupBy(x => x.BaseColorScheme)
                                         .Select(x => x.First())
                                         .Select(a => new AppThemeMenuData { Name = a.BaseColorScheme, BorderColorBrush = a.Resources["MahApps.Brushes.ThemeForeground"] as Brush, ColorBrush = a.Resources["MahApps.Brushes.ThemeBackground"] as Brush })
                                         .ToList();

            // create accent color menu items for the demo
            AccentColors = ThemeManager.Current.Themes
                                            .GroupBy(x => x.ColorScheme)
                                            .OrderBy(a => a.Key)
                                            .Select(a => new AccentColorMenuData { Name = a.Key, ColorBrush = a.First().ShowcaseBrush })
                                            .ToList();


            _regionManager = regionManager;

            DRCommand = new DelegateCommand(DRNavigation);
            NRCommand = new DelegateCommand(NRNavigation);
            TemplateCommand =  new DelegateCommand(TemplateNavigation);

            //Initialize the default region view
            //Refer to https://stackoverflow.com/questions/54330435/navigate-to-a-default-view-when-application-loaded-using-prism-7-in-wpf
            regionManager.RegisterViewWithRegion("ContentRegion", "DRWindows");

        }


        private DelegateCommand _fieldName;
        public DelegateCommand CommandName =>
            _fieldName ?? (_fieldName = new DelegateCommand(ExecuteCommandName));

        void ExecuteCommandName()
        {
            return;
        }


        #region Region Navigation

        //XAML中定义了RegionManager，ViewModel里可以接收这个RegionManager
        //RegionManager顾名思义，管理Region
        private readonly IRegionManager _regionManager;

        public DelegateCommand DRCommand { get; private set; }
        public DelegateCommand NRCommand { get; private set; }
        public DelegateCommand TemplateCommand { get; private set; }

        void DRNavigation()
        {
            if (_regionManager != null)
            {
                //注意，方法与Region注册不同，是Navigate关联的方法
                _regionManager.RequestNavigate("ContentRegion", "DRWindows");
            }
        }

        void NRNavigation()
        {
            if (_regionManager != null)
            {
                _regionManager.RequestNavigate("ContentRegion", "NRWindows");
            }
        }

        void TemplateNavigation()
        {
            if (_regionManager != null)
            {
                _regionManager.RequestNavigate("ContentRegion", "Template");
            }
        }

        #endregion



    }

    public class AppThemeMenuData : AccentColorMenuData
    {
        protected override void DoChangeTheme(string name)
        {
            if (name is not null)
            {
                ThemeManager.Current.ChangeThemeBaseColor(Application.Current, name);
            }
        }
    }

    public class AccentColorMenuData
    {
        public string Name { get; set; }

        public Brush BorderColorBrush { get; set; }

        public Brush ColorBrush { get; set; }

        public AccentColorMenuData()
        {
            //Update SimpleCommand with DelegateCommand 
            // this.ChangeAccentCommand = new SimpleCommand<string?>(o => true, this.DoChangeTheme);
            ChangeAccentCommand = new DelegateCommand<string>(ExecuteChangeACommand, CanExecuteChangeACommand);
        }

        void ExecuteChangeACommand(string name)
        {
            DoChangeTheme(name);
        }

        bool CanExecuteChangeACommand(string name)
        {
            return true;
        }


        public ICommand ChangeAccentCommand { get; }

        protected virtual void DoChangeTheme(string name)
        {
            if (name is not null)
            {
                ThemeManager.Current.ChangeThemeColorScheme(Application.Current, name);
            }
        }


    }
}
