using System;
using System.Windows.Input;

namespace Pomodoro.Utils
{
    public class DelegateCommand: ICommand
    {
        public Action canExcute;
        public Action<object> canExcuteObj;
        public DelegateCommand(Action execute) {
            canExcute = execute;
        }

        public DelegateCommand(Action<object> execute) {
            canExcuteObj = execute;    
        }

        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object parameter) {
            return true;
        }

        public virtual void Execute(object parameter) {
            if(parameter == null)
            {
                canExcute();
            }
            else
            {
                canExcuteObj(parameter);
            }
            
        }
    }
}