using System;
using System.Windows.Input;

namespace Commands
{
    public abstract class ACommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        protected virtual bool CanExecute()
        {
            return true;
        }

        protected abstract void Execute();

        protected virtual void OnCanExecuteChanged(EventArgs e)
        {
            CanExecuteChanged?.Invoke(this, e);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        void ICommand.Execute(object parameter)
        {
            Execute();
        }
    }
}