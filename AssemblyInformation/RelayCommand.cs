using System;
using System.Windows.Input;

namespace AssemblyInformation
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action execute;
        private Func<bool> canExecute;

        public RelayCommand(Action execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            this.execute = execute;

            if (canExecute != null)
                this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (canExecute == null)
                return true;
            else
                return canExecute();
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter) && execute != null)
                execute();
        }
    }
}