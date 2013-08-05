using System;
using System.Collections.Generic;
using System.Linq;
//http://relaycommandrt.codeplex.com/SourceControl/latest#RelayCommandRt/RelayCommand.cs

using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Go.Common
{
    public class RelayCommand<T> : ICommand
    {
        #region Fields

        readonly Action<T> _execute;
        readonly Predicate<T> _canExecute;

        #endregion // Fields

        #region Constructors

        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }
        #endregion // Constructors

        #region ICommand Members

        public bool CanExecute(T parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(T parameter)
        {
            _execute(parameter);
        }
        
        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute((T)parameter);
        }

        void ICommand.Execute(object parameter)
        {
            Execute((T)parameter);
        }

        #endregion // ICommand Members
    }

}