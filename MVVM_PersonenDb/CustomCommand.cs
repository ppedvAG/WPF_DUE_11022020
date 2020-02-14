using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM_PersonenDb
{
    public class CustomCommand : ICommand
    {
        //Delegatedefinition
        public delegate bool CanExecuteDelegate(object parameter);
        public delegate void ExecuteDelegate(object parameter);

        //Eigenschaftendefinition
        public CanExecuteDelegate CanExecuteMethode { get; set; }
        public ExecuteDelegate ExecuteMethode { get; set; }

        //Konstruktor
        public CustomCommand(CanExecuteDelegate can, ExecuteDelegate exe)
        {
            CanExecuteMethode = can;
            ExecuteMethode = exe;
        }

        //Commandanmeldung
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //Methoden
        public bool CanExecute(object parameter)
        {
            return CanExecuteMethode(parameter);
        }

        public void Execute(object parameter)
        {
            ExecuteMethode(parameter);
        }
    }
}
