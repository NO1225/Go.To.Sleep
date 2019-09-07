using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Go.To.Sleep
{
    /// <summary>
    /// Implementation of the <see cref="ICommand"/> interface>
    /// </summary>
    public class ReleyCommand : ICommand
    {
        /// <summary>
        /// The event thats fired when the <see cref="CanExecute(object)"/> value has changed
        /// </summary>
        public event EventHandler CanExecuteChanged = delegate { };

        /// <summary>
        /// The action to be executed when the command is called
        /// </summary>
        private Action _action = null;

        /// <summary>
        /// Determine wiether this cammand can be executed
        /// </summary>
        private Func<bool> _canExecute;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="action">The action to be executed when the command is called</param>
        /// <param name="canExecute">Determine wiether this cammand can be executed</param>
        public ReleyCommand(Action action, Func<bool> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Check if we can execute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute();
        }
        
        /// <summary>
        /// Execute the binded action
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            _action();
        }
    }
}
