using System.Windows.Input;

namespace AsgardCore.MVVM
{
    /// <summary>
    /// Interface for MVVM commands.
    /// </summary>
    public interface ICommandBase : ICommand
    {
        /// <summary>
        /// Raises <see cref="ICommand.CanExecuteChanged"/> event.
        /// </summary>
        void RaiseCanExecuteChanged();
    }
}
