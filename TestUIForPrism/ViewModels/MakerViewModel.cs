using Microsoft.WindowsAPICodePack.Dialogs;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing.IndexedProperties;
using System.Text;
using System.Threading.Tasks;

namespace TestUIForPrism.ViewModels
{
    public class MakerViewModel : BindableBase
    {

        /// <summary>
        /// For showing log in TextBox
        /// </summary>
        private string log = @"";
        public string Log
        {
            get { return log; }
            set
            {
                this.SetProperty(ref this.log, value + Environment.NewLine);
            }
        }

        /// <summary>
        /// For showing path in TextBoxs
        /// </summary>
        private string pathconfig;
        public string PathConfig
        {
            get { return pathconfig; }
            set { SetProperty(ref pathconfig, value); }
        }

        private string pathinput;
        public string PathInput
        {
            get { return pathinput; }
            set
            {
                SetProperty(ref pathinput, value);
                CommandStart.RaiseCanExecuteChanged();
            }
        }

        private string pathoutput;
        public string PathOutput
        {
            get { return pathoutput; }
            set
            {
                SetProperty(ref pathoutput, value);
                CommandStart.RaiseCanExecuteChanged();
            }
        }


        /// <summary>
        /// For Start Button 
        /// </summary>
        private DelegateCommand _fieldStart;
        public DelegateCommand CommandStart =>
            _fieldStart ?? (_fieldStart = new DelegateCommand(ExecuteCommandStart, CanExecuteCommandStart));

        void ExecuteCommandStart()
        {
            // Test
            string TextExample = "Test log function ~";
            for (int i = 0; i < 100; i++)
            {
                this.Log += TextExample + " ==>  " + i;
            }
        }

        bool CanExecuteCommandStart()
        {
            if (string.IsNullOrEmpty(this.PathInput) || string.IsNullOrEmpty(this.PathOutput)) return false;
            return true;
        }

        /// <summary>
        /// For Reference Button
        /// </summary>
        private DelegateCommand<Object> _fieldReference;
        public DelegateCommand<Object> CommandReference =>
            _fieldReference ?? (_fieldReference = new DelegateCommand<Object>(ExecuteCommandReference, CanExecuteCommandReference));

        void ExecuteCommandReference(Object parameter)
        {
            string type = (string)parameter;

            if (type == "config")
            {
                this.PathConfig = @"..\..\TestUIForPrism\Config\Analizer.conf";
                System.Windows.MessageBox.Show($"{this.PathConfig} will been set automatically");
            }
            else
            {
                //Please install WindowsAPICodePack-Shell in Nuget
                using (var cofd = new CommonOpenFileDialog()
                {
                    Title = "Please Select  File",
                    InitialDirectory = @"",
                    IsFolderPicker = true,
                })
                {
                    if (cofd.ShowDialog() != CommonFileDialogResult.Ok)
                    {
                        return;
                    }
                    if (type == "input") this.PathInput = cofd.FileName;
                    else this.PathOutput = cofd.FileName;

                    System.Windows.MessageBox.Show($"{cofd.FileName} is Selected");
                }
            }

        }

        bool CanExecuteCommandReference(Object parameter)
        {
            return true;
        }
    }
}

