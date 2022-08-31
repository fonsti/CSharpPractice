using EvernoteClone.Model;
using EvernoteClone.ViewModel.Commands;
using EvernoteClone.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace EvernoteClone.ViewModel
{
    public class NotesVM
    {
        public ObservableCollection<Notebook> Notebooks { get; set; }

        private Notebook selectedNotebook;

        public Notebook SelectedNotebook
        {
            get { return selectedNotebook; }
            set { 
                selectedNotebook = value; 
                //TODO: get notes
            }
        }

        public ObservableCollection<Note> Notes { get; set; }

        public NewNotebookCommand MyNewNotebookCommand { get; set; }

        public NewNoteCommand MyNewNoteCommand { get; set; }

        public ExitCommand MyExitCommand { get; set; }

        public SpeechCommand MySpeechCommand { get; set; }

        public NotesVM()
        {
            MyNewNotebookCommand = new NewNotebookCommand(this);
            MyNewNoteCommand = new NewNoteCommand(this);
            MyExitCommand = new ExitCommand(this);
            MySpeechCommand = new SpeechCommand(this);
        }

        public void CreateNotebook()
        {
            Notebook newNotebook = new Notebook()
            {
                Name = "New Notebook"
            };

            DataBaseHelper.Insert(newNotebook);
        }

        public void CreateNote(int notebookId)
        {
            Note newNote = new Note()
            {
                NotebookId = notebookId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Title = "New Note"
            };

            DataBaseHelper.Insert(newNote);
         }
    }
}
