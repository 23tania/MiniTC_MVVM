using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;


namespace MiniTC.ViewModel.BaseClass
{
    class RelayCommand : ICommand
    {
        // Metoda nic nie zwracająca o 1 argumencie typu object
        readonly Action<object> _execute;

        // Metodę zwracająca zmienną typu bool o argumencie object
        readonly Predicate<object> _canExecute;

        // Konstruktor
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            else
            {
                _execute = execute;
                _canExecute = canExecute;
            }
        }

        // Metoda określająca czy polecenie może zostać wykonane
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        // Zdarzenie informujące o możliwości wykonania polecenia
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                    CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (_canExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }

        // Metoda wykonywana przez polecenie
        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
