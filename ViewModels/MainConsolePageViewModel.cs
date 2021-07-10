using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConsoleWpf.ViewModels
{
    internal class MainConsolePageViewModel : Base_ViewModel.ViewModel
    {
        internal MainConsolePageViewModel()
        {

        }

        private string entry = "console line > ";

        public string Entry
        {
            get
            {
                if (entry.EndsWith("\n"))
                {
                    return entry + f();
                }else
                {
                    return entry;
                }
            }
            set => Set(ref entry, value);
        }

        public string f() 
        {
                return "Hello\nconsole line > ";
        } 
    }
}
