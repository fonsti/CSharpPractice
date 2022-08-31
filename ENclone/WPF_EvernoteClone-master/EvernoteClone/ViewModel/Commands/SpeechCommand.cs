using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class SpeechCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public NotesVM VM { get; set; }

        public SpeechCommand(NotesVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            return false;
        }

        public void Execute(object parameter)
        {
            // TODO: implement
        }
    }
}
