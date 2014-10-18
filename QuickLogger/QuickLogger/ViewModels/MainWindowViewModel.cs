using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QuickLogger.Helpers;
using System.Windows.Input;
using System.Diagnostics;
using System.IO;

namespace QuickLogger.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private static readonly string _filePath = "log.txt";

        private string _inputText;
        public string InputText
        {
            get
            {
                return _inputText;
            }

            set
            {
                if (_inputText != value)
                {
                    _inputText = value;
                    RaisePropertyChanged(() => InputText);
                }
            }
        }

        public ICommand LogItCommand { get; set; }

        public ICommand OpenFileCommand { get; set; }

        public MainWindowViewModel()
        {
            LogItCommand = new DelegateCommand(LogItExecute, LogItCanExeute);
            OpenFileCommand = new DelegateCommand(OpenFileExecute);
        }

        private void LogItExecute()
        {
            string inputText = InputText; //copy

            if (!string.IsNullOrWhiteSpace(inputText))
            {
                using (StreamWriter sw = File.AppendText(_filePath))
                {
                    sw.WriteLine("{0}: {1}", DateTime.Now, inputText);
                }

                InputText = string.Empty;
            }
        }

        private bool LogItCanExeute()
        {
            return !string.IsNullOrWhiteSpace(InputText);
        }

        private void OpenFileExecute()
        {
            if (File.Exists(_filePath))
            {
                Process.Start(_filePath);
            }
        }
    }
}
