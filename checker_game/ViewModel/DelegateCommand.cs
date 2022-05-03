using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace checker_game.ViewModel
{
    public class DelegateCommand : ICommand
    {
        private readonly Action<Object> _execute;
        private readonly Func<Object, Boolean> _canExecute;

        /// <summary>
        /// Creating order.
        /// </summary>
        /// <param name="execute">Order to be implemented.</param>
        public DelegateCommand(Action<Object> execute) : this(null, execute) { }

        /// <summary>
        /// Creating order.
        /// </summary>
        /// <param name="canExecute">Conditon of execution.</param>
        /// <param name="execute">Order to be implemented.</param>
        public DelegateCommand(Func<Object, Boolean> canExecute, Action<Object> execute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Eventhandler of the execution
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Checking implementation.
        /// </summary>
        /// <param name="parameter">Parameter of the order.</param>
        /// <returns>True if executable.</returns>
        public Boolean CanExecute(Object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        /// <summary>
        /// Execution of the order.
        /// </summary>
        /// <param name="parameter">Parameter of the order.</param>
        public void Execute(Object parameter)
        {
            if (!CanExecute(parameter))
            {
                throw new InvalidOperationException("Command execution is disabled.");
            }
            _execute(parameter);
        }

        /// <summary>
        /// Event of the execution.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }
    }
}
