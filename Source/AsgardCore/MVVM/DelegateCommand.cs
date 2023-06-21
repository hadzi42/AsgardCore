using System;

namespace AsgardCore.MVVM
{
    public class DelegateCommand : CommandBase
    {
        internal readonly Func<object, bool> _CanExecuteAction;
        internal readonly Action<object> _ExecuteAction;

        public DelegateCommand(Action<object> executeAction)
        {
            _ExecuteAction = executeAction;
        }

        public DelegateCommand(Action<object> executeAction, Func<object, bool> canExecuteAction)
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
            _ExecuteAction(parameter);
        }
    }
}
