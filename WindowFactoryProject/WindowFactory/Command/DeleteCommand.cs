using System;
using System.Windows.Input;

namespace WindowFactory.Command
{
    class DelegateCommand : ICommand
    {
        private readonly Action _command;
        private readonly Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action command, Func<bool> canExecute = null)
        {
            _canExecute = canExecute;
            _command = command;
        }

        public void Execute(object parameter) => _command?.Invoke();
        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}