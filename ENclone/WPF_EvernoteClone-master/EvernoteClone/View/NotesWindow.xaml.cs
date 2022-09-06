using EvernoteClone.ViewModel;
using EvernoteClone.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EvernoteClone.View
{
    /// <summary>
    /// Interaction logic for NotesWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window
    {
        public NotesVM VM { get; set; }

        public NotesWindow()
        {
            InitializeComponent();

            VM = Resources["viewModel"] as NotesVM;
            VM.SelectedNoteChanged += ViewModel_SelectedNoteChanged;

            var fontFamilies = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            FontFamilyComboBox.ItemsSource = fontFamilies;

            List<double> fontSizes = new List<double>()
            {
                8, 9, 10, 11, 12, 14, 16, 28, 48, 72
            };
            FontSizeComboBox.ItemsSource = fontSizes;
        }

        private void ViewModel_SelectedNoteChanged(object sender, EventArgs e)
        {
            ContentRichtTextbox.Document.Blocks.Clear();
            if (VM.SelectedNote != null)
            {
                if (!string.IsNullOrEmpty(VM.SelectedNote.FileLocation))
                {
                    using (FileStream fileStream = new FileStream(VM.SelectedNote.FileLocation, FileMode.Open))
                    {
                        var content = new TextRange(ContentRichtTextbox.Document.ContentStart, ContentRichtTextbox.Document.ContentEnd);
                        content.Load(fileStream, DataFormats.Rtf);
                    }
                }
            }
        }

        private void ContentRichtTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int amountCharacters = (new TextRange(ContentRichtTextbox.Document.ContentStart, ContentRichtTextbox.Document.ContentEnd)).Text.Length;

            StatusTextBlock.Text = $"Document length: {amountCharacters} characters.";
        }

        private void BoldButton_Click(object sender, RoutedEventArgs e)
        {
            bool isButtonChecked = (sender as ToggleButton).IsChecked ?? false;

            if (isButtonChecked)
            {
                ContentRichtTextbox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
            }
            else
            {
                ContentRichtTextbox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Normal);
            }
        }

        private void ItalicButton_Click(object sender, RoutedEventArgs e)
        {
            bool isButtonChecked = (sender as ToggleButton).IsChecked ?? false;

            if (isButtonChecked)
            {
                ContentRichtTextbox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Italic);
            }
            else
            {
                ContentRichtTextbox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Normal);
            }
        }

        private void UnderlineButton_Click(object sender, RoutedEventArgs e)
        {
            bool isButtonChecked = (sender as ToggleButton).IsChecked ?? false;

            if (isButtonChecked)
            {
                ContentRichtTextbox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            }
            else
            {
                TextDecorationCollection textDecorations;
                (ContentRichtTextbox.Selection.GetPropertyValue(Inline.TextDecorationsProperty) as TextDecorationCollection).TryRemove(TextDecorations.Underline, out textDecorations);
                ContentRichtTextbox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, textDecorations);
            }
        }

        private void ContentRichtTextbox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var selectedWeight = ContentRichtTextbox.Selection.GetPropertyValue(Inline.FontWeightProperty);
            BoldButton.IsChecked = (selectedWeight != DependencyProperty.UnsetValue) && selectedWeight.Equals(FontWeights.Bold);

            var selectedStyle = ContentRichtTextbox.Selection.GetPropertyValue(Inline.FontStyleProperty);
            ItalicButton.IsChecked = (selectedStyle != DependencyProperty.UnsetValue) && selectedStyle.Equals(FontStyles.Italic);

            var selectedDecoration = ContentRichtTextbox.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            UnderlineButton.IsChecked = (selectedDecoration != DependencyProperty.UnsetValue) && selectedDecoration.Equals(TextDecorations.Underline);

            FontFamilyComboBox.SelectedItem = ContentRichtTextbox.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            FontSizeComboBox.Text = (ContentRichtTextbox.Selection.GetPropertyValue(Inline.FontSizeProperty)).ToString();
        }

        private void FontFamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontFamilyComboBox.SelectedItem != null)
            {
                ContentRichtTextbox.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, FontFamilyComboBox.SelectedItem);
            }
        }

        private void FontSizeComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ContentRichtTextbox.Selection.ApplyPropertyValue(Inline.FontSizeProperty, FontSizeComboBox.Text);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string rtfFile = System.IO.Path.Combine(Environment.CurrentDirectory, $"{VM.SelectedNote.Id}.rtf");
            VM.SelectedNote.FileLocation = rtfFile;
            DataBaseHelper.Update(VM.SelectedNote);

            using (FileStream fileStream = new FileStream(rtfFile, FileMode.Create))
            {
                var content = new TextRange(ContentRichtTextbox.Document.ContentStart, ContentRichtTextbox.Document.ContentEnd);
                content.Save(fileStream, DataFormats.Rtf);

            }
            
        }
    }
}
