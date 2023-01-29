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
    public class GlobalCommands
    {
        public static ICommand Exit { get; } = new ExitCommand();
    }
}
