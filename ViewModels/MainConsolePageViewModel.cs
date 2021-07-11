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

        }

        public ICommand ResetButton { get; }

        public bool CanResetExecute(object parameter) => true;
        public void ResetExecute(object parameter)
        {

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
