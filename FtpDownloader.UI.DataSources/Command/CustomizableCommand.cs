using System.Windows.Input;

namespace FtpDownloader.UI.DataSources.Command
{
    public class CustomizableCommand : ICommand
    {
        private readonly Action<object> _action;
        private readonly Func<object, bool> _predicate;

        public CustomizableCommand(Action<object> action, Func<object, bool> predicate)
        {
            _action = action;
            _predicate = predicate;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }



        public bool CanExecute(object parameter) => _predicate(parameter);
        public void Execute(object parameter) => _action(parameter);
        public static void RaiseCanExecuteChanged() => CommandManager.InvalidateRequerySuggested();
    }
}
