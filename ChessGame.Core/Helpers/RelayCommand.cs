using System;
using System.Windows.Input;

namespace ChessGame
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _Action;
        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<object> action)
        {
            _Action = action;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _Action(parameter);
        }
    }
}
