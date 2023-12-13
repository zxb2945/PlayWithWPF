using Microsoft.WindowsAPICodePack.Dialogs;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayWithMahApps_Metro.ViewModels
{
    class DRWindowsViewModel : BindableBase
    {

        /// <summary>
        /// Property for log
        /// </summary>
        private string _log = @"";
        public string Log
        {
            get { return _log; }
            set { SetProperty(ref _log, value + Environment.NewLine); }
        }

        /// <summary>
        /// Input file Label
        /// </summary>
        private string _inputTitle = "File Path";
        public string InputTitle
        {
            get { return _inputTitle; }
            set { SetProperty(ref _inputTitle, value); }
        }


        /// <summary>
        /// Input file path
        /// </summary>
        private string _inputPath = "Please input file";
        public string InputPath
        {
            get { return _inputPath; }
            set { SetProperty(ref _inputPath, value); }
        }

        /// <summary>
        /// Select file path and Diagnose
        /// </summary>
        private DelegateCommand _diagnose;
        public DelegateCommand DiagnoseCommand =>
            _diagnose ?? (_diagnose = new DelegateCommand(ExecuteDiagnoseCommand));

        void ExecuteDiagnoseCommand()
        {
            //Please install WindowsAPICodePack-Shell in Nuget
            using (var cofd = new CommonOpenFileDialog()
            {
                Title = "Please Select  File",
                InitialDirectory = @"C:",
                IsFolderPicker = true,
            })
            {
                if (cofd.ShowDialog() != CommonFileDialogResult.Ok)
                {
                    return;
                }

                InputPath = cofd.FileName;

                System.Windows.MessageBox.Show($"{cofd.FileName} is Selected");
            }

            for (int i = 0; i < 2000; i++)
            {
                Log += $"Test: {i}";
            }

        }


    }
}

