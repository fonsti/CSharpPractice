using EvernoteClone.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EvernoteClone.View.UserControls
{
    /// <summary>
    /// Interaction logic for DisplayNotebook.xaml
    /// </summary>
    public partial class DisplayNotebook : UserControl
    {
        public Notebook MyNotebook
        {
            get { return (Notebook)GetValue(MyNotebookProperty); }
            set { SetValue(MyNotebookProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyNotebook.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyNotebookProperty =
            DependencyProperty.Register("MyNotebook", typeof(Notebook), typeof(DisplayNotebook), new PropertyMetadata(null, SetValues));

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DisplayNotebook notebookUserControl = d as DisplayNotebook;

            if (notebookUserControl != null)
            {
                notebookUserControl.DataContext = notebookUserControl.MyNotebook;
            }
        }

        public DisplayNotebook()
        {
            InitializeComponent();
        }
    }
}
