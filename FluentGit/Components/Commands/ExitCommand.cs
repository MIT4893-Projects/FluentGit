using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FluentGit.Components.Commands
{
    public class ExitCommand : ICommand
    {
        public ExitCommand() { }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            Application.Current.Exit();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
