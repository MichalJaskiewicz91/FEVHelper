using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEVHelper.Commands
{
    public class CommandEvent
    {
        #region Private Members
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;
        #endregion

        #region Constructors
        public CommandEvent(Predicate<object> canExecute, Action<object> execute)
        {
            // Assign methods to the delegates
            _canExecute = canExecute;
            _execute = execute;
        }
        #endregion

        public event EventHandler CanExecuteChanged = (sender, e) => { };

        public bool CanExecute(object parameter)
        {
            // Invoke the delegate
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            // Invoke the delegate
            _execute(parameter);
        }
    }
}
