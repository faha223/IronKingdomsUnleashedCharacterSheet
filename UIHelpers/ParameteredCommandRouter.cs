using System;
using System.Windows.Input;

namespace IronKingdomsUnleashedCharacterSheet.UIHelpers
{
    class ParameteredCommandRouter<T> : ICommand
    {
        #region Private Members

        private readonly Predicate<T> _canExecute;
        private readonly Action<T> _execute;

        #endregion Private Members

        #region Constructors

        /// <summary>
        /// Constructor for the ParameteredCommandRouter that does not take in a boolean function, so it will always allow the command to execute.
        /// Calls the overloaded constructor, passing null.
        /// </summary>
        /// <param name="Execute">he method to execute.</param>
        public ParameteredCommandRouter(Action<T> Execute)
            : this(Execute, null)
        {
        }

        /// <summary>
        /// Constructor for the ParameteredCommandRouter
        /// </summary>
        /// <param name="Execute">The method to execute.</param>
        /// <param name="CanExecute">Whether the Execute method can run.</param>
        public ParameteredCommandRouter(Action<T> Execute, Predicate<T> CanExecute)
        {
            //check argument
            if (Execute == null) throw new ArgumentNullException("Executing action cannot be null.", "Execute");
            //assign values
            _execute = Execute;
            _canExecute = CanExecute;
        }

        #endregion Constructors

        #region ICommand Interface Members

        /// <summary>
        /// An event from the ICommand Interface. Event fires when the result of CanExecute changes.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                //only allow subscription if the can execute method is not null
                if (_canExecute != null) CommandManager.RequerySuggested += value;
            }
            remove
            {
                //only allow unsubscription if the can execute method is not null
                if (_canExecute != null) CommandManager.RequerySuggested -= value;
            }
        }

        /// <summary>
        /// A Method from the ICommand Interface. 
        /// Runs the canExecute predicate, passing the command parameter, to determine if the command can execute.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        /// <returns>A boolean indicating whether the requested operation can run.</returns>
        public Boolean CanExecute(object parameter)
        {
            if (parameter == System.Windows.Data.BindingOperations.DisconnectedSource)
                return false;
            if (_canExecute == null) return true;
            return _canExecute((T)parameter);
        }

        /// <summary>
        /// A Method from the ICommand Interface. 
        /// Executes the stored Command with the passed parameter.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        #endregion ICommand Interface Members

    }
}
