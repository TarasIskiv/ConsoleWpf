using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ConsoleWpf.ViewModels
{
    internal class MainConsolePageViewModel : Base_ViewModel.ViewModel
    {
        
        #region Entry
        private string entry = "console line > ";

        public string Entry
        {
            get => entry;
            set => Set(ref entry, value);
        }
        #endregion

        #region Buttons

        public ICommand ResultButton { get; }

        public bool CanResultExecute(object parameter) => true;
        public void ResultExecute(object parameter)
        {
            string searched = Entry;
            selector(searched);
        }

        public ICommand ResetButton { get; }

        public bool CanResetExecute(object parameter) => true;
        public void ResetExecute(object parameter)
        {
            Entry = null;
            Entry = "console line > ";
        }

        #endregion


        #region Functions

        private void selector(string line)
        {
            line = line.Substring(14).Trim();
            MessageBox.Show(line);
            if (line.Equals("help"))
            {
                //work
            }else
            {
                if (line.StartsWith("-f"))
                {
                    //work
                }else if (line.StartsWith("-d"))
                {
                    // work
                }else if (line.StartsWith("-u"))
                {
                    // work
                }else
                {
                    //bad input
                }
            }
        }

        private string fileSelected(string line)
        {
            return null;
        }

        private string directorySelected(string line)
        {
            return null;
        }

        private string urlSelected(string line)
        {
            return null;
        }
        #endregion

        #region Result
        private string result;
        public string Result 
        { 
            get => result;
            set => Set(ref result, value);
        }
        #endregion
        internal MainConsolePageViewModel()
        {
            ResultButton = new Infastructure.LambdaCommand(ResultExecute, CanResultExecute);
            ResetButton = new Infastructure.LambdaCommand(ResetExecute, CanResetExecute);
        }
    }
}
