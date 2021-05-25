using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace notePad1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// https://docs.microsoft.com/en-us/uwp/api/Windows.UI.Xaml.Controls.RichEditBox?view=winrt-19041
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            
            // App Customization
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            
            
            ApplicationView appView = ApplicationView.GetForCurrentView();
            appView.Title = "Grant's Notepad";
        
            // Set Active Window Colors
            titleBar.BackgroundColor = Windows.UI.Colors.DarkBlue;
            titleBar.ForegroundColor = Windows.UI.Colors.White;
            txtBox1.Background = new SolidColorBrush(Windows.UI.Colors.Black);
            txtBox1.Foreground = new SolidColorBrush(Windows.UI.Colors.White);

        }

        // Exiting the Application
        private void exitFile_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        // Saving a File
        private async void saveFile_Click(object sender, RoutedEventArgs e)
        {

            Windows.Storage.Pickers.FileSavePicker savePicker = new Windows.Storage.Pickers.FileSavePicker();
            savePicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;

            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add("Rich Text", new List<string>() { ".rtf" });

            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = "New Document";

            Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                // Prevent updates to the remote version of the file until we 
                // finish making changes and call CompleteUpdatesAsync.
                Windows.Storage.CachedFileManager.DeferUpdates(file);
                // write to file
                Windows.Storage.Streams.IRandomAccessStream randAccStream =
                    await file.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite);

                txtBox1.Document.SaveToStream(Windows.UI.Text.TextGetOptions.FormatRtf, randAccStream);
                


                // Let Windows know that we're finished changing the file so the 
                // other app can update the remote version of the file.
                Windows.Storage.Provider.FileUpdateStatus status = await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);
                if (status != Windows.Storage.Provider.FileUpdateStatus.Complete)
                {
                    Windows.UI.Popups.MessageDialog errorBox =
                        new Windows.UI.Popups.MessageDialog("File " + file.Name + " couldn't be saved.");
                    await errorBox.ShowAsync();
                }
            }
        }

        // Opening a File
        private async void openFile_Click(object sender, RoutedEventArgs e)
        {
            // Open a text file.
            Windows.Storage.Pickers.FileOpenPicker open =
                new Windows.Storage.Pickers.FileOpenPicker();
            open.SuggestedStartLocation =
                Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            open.FileTypeFilter.Add(".rtf");

            Windows.Storage.StorageFile file = await open.PickSingleFileAsync();
            
            

            if (file != null)
            {
                try
                {
                    Windows.Storage.Streams.IRandomAccessStream randAccStream =
                await file.OpenAsync(Windows.Storage.FileAccessMode.Read);

                    // Load the file into the Document property of the RichEditBox.
                    txtBox1.Document.LoadFromStream(Windows.UI.Text.TextSetOptions.FormatRtf, randAccStream);
                    
                }
                catch (Exception)
                {
                    ContentDialog errorDialog = new ContentDialog()
                    {
                        Title = "File open error",
                        Content = "Sorry, I couldn't open the file.",
                        PrimaryButtonText = "Ok"
                    };

                    await errorDialog.ShowAsync();
                }
            }
        }

        private void underline_Click(object sender, RoutedEventArgs e)
        {
         
        
            Windows.UI.Text.ITextSelection selectedText = txtBox1.Document.Selection;
            if (selectedText != null)
            {
                Windows.UI.Text.ITextCharacterFormat charFormatting = selectedText.CharacterFormat;
                if (charFormatting.Underline == Windows.UI.Text.UnderlineType.None)
                {
                    charFormatting.Underline = Windows.UI.Text.UnderlineType.Single;
                }
                else
                {
                    charFormatting.Underline = Windows.UI.Text.UnderlineType.None;
                }
                selectedText.CharacterFormat = charFormatting;
            }
        }

        private void italic_Click(object sender, RoutedEventArgs e)
        {
            Windows.UI.Text.ITextSelection selectedText = txtBox1.Document.Selection;
            if (selectedText != null)
            {
                Windows.UI.Text.ITextCharacterFormat charFormatting = selectedText.CharacterFormat;
                charFormatting.Italic = Windows.UI.Text.FormatEffect.Toggle;
                selectedText.CharacterFormat = charFormatting;
            }
        }

        private void bold_Click(object sender, RoutedEventArgs e)
        {
            Windows.UI.Text.ITextSelection selectedText = txtBox1.Document.Selection;
            if (selectedText != null)
            {
                Windows.UI.Text.ITextCharacterFormat charFormatting = selectedText.CharacterFormat;
                charFormatting.Bold = Windows.UI.Text.FormatEffect.Toggle;
                selectedText.CharacterFormat = charFormatting;
            }
        }

        private void leftAlign_Click(object sender, RoutedEventArgs e)
        {
            
            txtBox1.TextAlignment = TextAlignment.Left;
        }

        private void centerAlign_Click(object sender, RoutedEventArgs e)
        {
            txtBox1.TextAlignment = TextAlignment.Center;
        }

        private void rightAlign_Click(object sender, RoutedEventArgs e)
        {
            txtBox1.TextAlignment = TextAlignment.Right;
        }

        private void paste_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void readAloud_Click(object sender, RoutedEventArgs e)
        {

        }

        private void textSpeech_Click(object sender, RoutedEventArgs e)
        {

        }

        private void saveFile_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            saveFile.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(0, 0, 0, 0));
        }
    }
}
