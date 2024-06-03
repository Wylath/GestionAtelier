using System;
using System.Windows.Input;

namespace GestionAtelier.ToolBox
{
    class RelayCommand : ICommand
    {
        private readonly Action _Execute;
        private readonly Action<object> _ExecuteWithObject;
        private readonly Func<bool> _CanExecute;

        public RelayCommand(Action<object> Execute, Func<bool> CanExecute)
        {
            if (Execute == null)
                throw new Exception();

            _ExecuteWithObject = Execute;
            _CanExecute = CanExecute;

        }

        public RelayCommand(Action Execute, Func<bool> CanExecute)
        {
            if (Execute == null)
                throw new Exception();

            _Execute = Execute;
            _CanExecute = CanExecute;

        }
        public bool CanExecute(object parameter)
        {
            return (_CanExecute == null) ? true : _CanExecute();
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_CanExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }
            remove
            {
                if (_CanExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        public void Execute(object parameter)
        {
            if (_ExecuteWithObject != null)
                _ExecuteWithObject(parameter);
            else
                _Execute();
        }
    }
}
