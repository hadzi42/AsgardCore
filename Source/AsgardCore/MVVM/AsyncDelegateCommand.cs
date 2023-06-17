using System;
using System.Threading.Tasks;

namespace AsgardCore.MVVM
{
    public sealed class AsyncDelegateCommand : CommandBase
    {
        public Task CurrentTask { get; private set; }
        
        internal readonly Func<object, bool> _CanExecuteAction;
        internal readonly Func<object, Task> _ExecuteAction;

        public AsyncDelegateCommand(Func<object, Task> executeAction)
        {
            _ExecuteAction = executeAction;
        }

        public AsyncDelegateCommand(Func<object, Task> executeAction, Func<object, bool> canExecuteAction)
            : this(executeAction)
        {
            _CanExecuteAction = canExecuteAction;
        }

        public override bool CanExecute(object parameter)
        {
            if (_CanExecuteAction != null)
                return _CanExecuteAction(parameter);
            return true;
        }

        public override void Execute(object parameter)
        {
            CurrentTask = _ExecuteAction(parameter);
        }
    }
}
