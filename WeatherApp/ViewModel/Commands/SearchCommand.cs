using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WeatherApp.ViewModel.Commands
{
    // ThIS class should only handle the command execution (whether or not it can execute etc) and then call the method from the VM 
    public class SearchCommand : ICommand
    {
        public WeatherViewModel VM { get; set; }

        public event EventHandler? CanExecuteChanged;

        public SearchCommand(WeatherViewModel vm)
        {
            VM = vm;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            VM.MakeQuery();
        }
    }
}