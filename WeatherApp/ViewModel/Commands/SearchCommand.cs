using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WeatherApp.ViewModel.Commands
{
    // ThiS class should only handle the command execution (e.g. whether or not it can be executed etc) and then call the method from the VM 
    public class SearchCommand : ICommand
    {
        public WeatherViewModel VM { get; set; }

        // CanExecuteChanged is raised when the CanExecute method of an ICommand gets changed.
        public event EventHandler? CanExecuteChanged
        {
            // CommandManager.RequerySuggested event occurs when the CommandManager detects conditions that might change the ability of a command to execute.
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public SearchCommand(WeatherViewModel vm)
        {
            VM = vm;
        }

        // Will be executed every time the query changes.
        public bool CanExecute(object? parameter)
        {
            string query = parameter as string;
            if (string.IsNullOrWhiteSpace(query))
            {
                return false;
            }
            return true;
        }

        public void Execute(object? parameter)
        {
            VM.MakeQuery();
        }
    }
}