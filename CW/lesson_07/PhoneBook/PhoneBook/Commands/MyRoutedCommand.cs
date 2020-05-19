using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PhoneBook.ViewModels;

namespace PhoneBook.Commands
{
    public abstract class MyRoutedCommand : ICommand
    {
        protected ApplicationViewModel avm;

        public MyRoutedCommand(ApplicationViewModel _avm)
        {
            this.avm = _avm;
        }

        public event EventHandler CanExecuteChanged;
        public abstract bool CanExecute(object parameter);
        public abstract void Execute(object parameter);
    }
}
