using System;
using System.Windows.Input;

namespace EspejoAnalysis.Helper
{
    [Serializable]
    public class CommandBase : ICommand
    {
        private Action<object> execAction;
        private Func<object, bool> canExec;

        public CommandBase(Action<object> execAction)
        {
            this.execAction = execAction;
            this.canExec = null;
        }

        public CommandBase(Action<object> execAction, Func<object, bool> canExec)
        {
            this.execAction = execAction;
            this.canExec = canExec;
        }

        public bool CanExecute(object parameter)
        {
            if (this.canExec == null)
            {
                return true;
            }
            else
            {
                return this.canExec.Invoke(parameter);
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            if (execAction != null)
            {
                execAction.Invoke(parameter);
            }
        }
    }
}
