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

        private string _prefix;
        public string Prefix
        {
            get
            {
                return _prefix;
            }
            set
            {
                if (value != _prefix)
                {
                    _prefix = value;
                    RaisePropertyChanged(() => Prefix);
                }
            }
        }

        private bool _usePrefis = false;
        public bool UsePrefix
        {
            get
            {
                return _usePrefis;
            }

            set
            {
                if (_usePrefis != value)
                {
                    _usePrefis = value;
                    RaisePropertyChanged(() => UsePrefix);
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
            string prefix = Prefix;//copy

            if (!string.IsNullOrWhiteSpace(inputText))
            {
                using (StreamWriter sw = File.AppendText(_filePath))
                {
                    sw.WriteLine("{0}: {1} {2}", DateTime.Now, UsePrefix ? Prefix : string.Empty, inputText);
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
