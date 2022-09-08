using EvernoteClone.Model;
using EvernoteClone.ViewModel.Commands;
using EvernoteClone.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace EvernoteClone.ViewModel
{
    public class NotesVM : INotifyPropertyChanged
    {
        public ObservableCollection<Notebook> Notebooks { get; set; }

        private Notebook selectedNotebook;

        public event PropertyChangedEventHandler PropertyChanged;

        public Notebook SelectedNotebook
        {
            get { return selectedNotebook; }
            set { 
                selectedNotebook = value;
                OnPropertyChanged("SelectedNotebook");
                GetNotes();
            }
        }

        private Note selectedNote;

        public Note SelectedNote
        {
            get { return selectedNote; }
            set { 
                selectedNote = value; 
                OnPropertyChanged("SelectedNote"); 
                SelectedNoteChanged?.Invoke(this, new EventArgs()); 
            }
        }


        private Visibility editNameIsVisible;

        public Visibility EditNameIsVisible
        {
            get { return editNameIsVisible; }
            set { 
                editNameIsVisible = value;
                OnPropertyChanged("EditNameIsVisible");
            }
        }


        public ObservableCollection<Note> Notes { get; set; }

        public NewNotebookCommand MyNewNotebookCommand { get; set; }

        public NewNoteCommand MyNewNoteCommand { get; set; }

        public ExitCommand MyExitCommand { get; set; }

        public SpeechCommand MySpeechCommand { get; set; }

        public EditCommand MyEditCommand { get; set; }

        public EndEditinigcommand MyEndEditingCommand { get; set; }

        public event EventHandler SelectedNoteChanged;

        public NotesVM()
        {
            MyNewNotebookCommand = new NewNotebookCommand(this);
            MyNewNoteCommand = new NewNoteCommand(this);
            MyExitCommand = new ExitCommand(this);
            MySpeechCommand = new SpeechCommand(this);
            MyEditCommand = new EditCommand(this);
            MyEndEditingCommand = new EndEditinigcommand(this);

            Notebooks = new ObservableCollection<Notebook>();
            Notes = new ObservableCollection<Note>();

            EditNameIsVisible = Visibility.Collapsed;

            GetNotebooks();
        }

        public void CreateNotebook()
        {
            Notebook newNotebook = new Notebook()
            {
                Name = "New Notebook"
            };

            DataBaseHelper.Insert(newNotebook);

            GetNotebooks();
        }

        public void CreateNote(int notebookId)
        {
            Note newNote = new Note()
            {
                NotebookId = notebookId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Title = $"New Note {DateTime.Now.ToString()}"
            };

            DataBaseHelper.Insert(newNote);

            GetNotes();
        }

        public void GetNotebooks()
        {
            var notebooks = DataBaseHelper.Read<Notebook>();

            Notebooks.Clear();
            foreach (var notebook in notebooks)
            {
                Notebooks.Add(notebook);
            }
        }

        private void GetNotes()
        {
            if (SelectedNotebook != null)
            {
                var notes = DataBaseHelper.Read<Note>().Where(n => n.NotebookId == SelectedNotebook.Id).ToList();

                Notes.Clear();
                foreach (var note in notes)
                {
                    Notes.Add(note);
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void StartEditing()
        {
            EditNameIsVisible = Visibility.Visible;
        }

        public void StopEditing(Notebook notebook)
        {
            EditNameIsVisible = Visibility.Collapsed;
            DataBaseHelper.Update(notebook);
            GetNotebooks();
        }
    }
}
