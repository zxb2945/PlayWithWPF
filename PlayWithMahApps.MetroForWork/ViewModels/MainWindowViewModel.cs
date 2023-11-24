using ControlzEx.Theming;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace PlayWithMahApps.MetroForWork.ViewModels
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

        public MainWindowViewModel()
        {
            // create metro theme color menu items for the demo
            this.AppThemes = ThemeManager.Current.Themes
                                         .GroupBy(x => x.BaseColorScheme)
                                         .Select(x => x.First())
                                         .Select(a => new AppThemeMenuData { Name = a.BaseColorScheme, BorderColorBrush = a.Resources["MahApps.Brushes.ThemeForeground"] as Brush, ColorBrush = a.Resources["MahApps.Brushes.ThemeBackground"] as Brush })
                                         .ToList();

            // create accent color menu items for the demo
            this.AccentColors = ThemeManager.Current.Themes
                                            .GroupBy(x => x.ColorScheme)
                                            .OrderBy(a => a.Key)
                                            .Select(a => new AccentColorMenuData { Name = a.Key, ColorBrush = a.First().ShowcaseBrush })
                                            .ToList();
        }
    }

    public class AppThemeMenuData : AccentColorMenuData
    {
        protected override void DoChangeTheme(string? name)
        {
            if (name is not null)
            {
                ThemeManager.Current.ChangeThemeBaseColor(Application.Current, name);
            }
        }
    }

    public class AccentColorMenuData
    {
        public string? Name { get; set; }

        public Brush? BorderColorBrush { get; set; }

        public Brush? ColorBrush { get; set; }

        public AccentColorMenuData()
        {
            //Update SimpleCommand with DelegateCommand 
            // this.ChangeAccentCommand = new SimpleCommand<string?>(o => true, this.DoChangeTheme);
            this.ChangeAccentCommand = new DelegateCommand<string>(ExecuteChangeACommand, CanExecuteChangeACommand);
        }

        void ExecuteChangeACommand(string name)
        {
            this.DoChangeTheme(name);
        }

        bool CanExecuteChangeACommand(string name)
        {
            return true;
        }


        public ICommand ChangeAccentCommand { get; }

        protected virtual void DoChangeTheme(string? name)
        {
            if (name is not null)
            {
                ThemeManager.Current.ChangeThemeColorScheme(Application.Current, name);
            }
        }
    }
}
