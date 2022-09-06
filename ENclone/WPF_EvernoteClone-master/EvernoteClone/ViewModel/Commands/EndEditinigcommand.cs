using EvernoteClone.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class EndEditinigcommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public NotesVM VM { get; set; }

        public EndEditinigcommand(NotesVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Notebook notebook = parameter as Notebook;
            if (notebook != null)
            {
                VM.StopEditing(notebook);
            }
        }
    }
}
