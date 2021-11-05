using System;
using System.Windows.Input;

namespace ChessGame
{
    public class RelayCommand : ICommand
    {
        private Action<object> mAction;
        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<object> action)
        {
            mAction = action;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            mAction(parameter);
        }
    }
}
