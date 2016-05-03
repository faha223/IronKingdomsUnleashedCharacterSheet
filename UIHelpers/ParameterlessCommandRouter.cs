using System;
using System.Windows.Input;

namespace IronKingdomsUnleashedCharacterSheet.UIHelpers
{
    public class ParameterlessCommandRouter : ICommand
    {
        #region Private Members

        private readonly Func<bool> _canExecute;
        private readonly Action _execute;

        #endregion Private Members

        #region Constructors

        /// <summary>
        /// Constructor for the ParameterlessCommandRouter that does not take in a boolean function, so it will always allow the command to execute.
        /// Calls the overloaded constructor, passing null.
        /// </summary>
        /// <param name="Execute">he method to execute.</param>
        public ParameterlessCommandRouter(Action Execute)
            : this(Execute, null)
        {
        }

        /// <summary>
        /// Constructor for the ParameterlessCommandRouter
        /// </summary>
        /// <param name="Execute">The method to execute.</param>
        /// <param name="CanExecute">Whether the Execute method can run.</param>
        public ParameterlessCommandRouter(Action Execute, Func<bool> CanExecute)
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
        /// Allows for a command argument to be passed, but this class does not allow command parameters.
        /// </summary>
        /// <param name="parameter">The command parameter. Unused in this class.</param>
        /// <returns>A boolean indicating whether the requested operation can run.</returns>
        public bool CanExecute(object parameter)
        {
            //if the 
            if (_canExecute == null) return true;
            else return _canExecute();
        }

        /// <summary>
        /// A Method from the ICommand Interface. 
        /// Executes the stored Command
        /// Allows for a command argument to be passed, but this class does not allow command parameters.
        /// </summary>
        /// <param name="parameter">The command parameter. Unused in this class.</param>
        public void Execute(object parameter)
        {
            _execute();
        }

        #endregion ICommand Interface Members

    }
}
