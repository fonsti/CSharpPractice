using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class ExitCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public NotesVM VM { get; set; }

        public ExitCommand(NotesVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Application.Current.Shutdown();
        }
    }
}
