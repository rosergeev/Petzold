using System;
using System.Collections.Generic;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PrimitivePad
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ApplicationDataContainer appData = ApplicationData.Current.LocalSettings;

        public MainPage()
        {
            this.InitializeComponent();
            Loaded += (s, e) =>
            {
                if (appData.Values.ContainsKey("TextWrapping"))
                    txtbox.TextWrapping = (TextWrapping)appData.Values["TextWrapping"];
                wrapButton.IsChecked = txtbox.TextWrapping == TextWrapping.Wrap;
                wrapButton.Content = (bool)wrapButton.IsChecked ? "Wrap" : "No wrap";

                txtbox.Focus(FocusState.Programmatic);
            };
        }

        async private void OnFileOpenButtonClick(object sender, RoutedEventArgs e)
        {
            FileOpenPicker picker = new FileOpenPicker();
            picker.FileTypeFilter.Add(".txt");
            StorageFile storageFile = await picker.PickSingleFileAsync();

            if (storageFile == null)
                return;

            using (IRandomAccessStream stream = await storageFile.OpenReadAsync())
            {
                using (DataReader dataReader = new DataReader(stream))
                {
                    uint length = (uint)stream.Size;
                    await dataReader.LoadAsync(length);
                    txtbox.Text = dataReader.ReadString(length);
                }
            }
        }

        async private void OnFileSaveAsButtonClick(object sender, RoutedEventArgs e)
        {
            FileSavePicker picker = new FileSavePicker();
            picker.DefaultFileExtension = ".txt";
            picker.FileTypeChoices.Add("Text", new List<string> { ".txt" });

            StorageFile storageFile = await picker.PickSaveFileAsync();

            if (storageFile == null)
                return;

            using (IRandomAccessStream stream = await storageFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                using (DataWriter dataWriter = new DataWriter(stream))
                {
                    dataWriter.WriteString(txtbox.Text);
                    await dataWriter.StoreAsync();
                }
            }
        }

        private void OnWrapButtonChecked(object sender, RoutedEventArgs e)
        {
            txtbox.TextWrapping = (bool)wrapButton.IsChecked ? TextWrapping.Wrap : TextWrapping.NoWrap;
            wrapButton.Content = (bool)wrapButton.IsChecked ? "Wrap" : "No wrap";
            appData.Values["TextWrapping"] = (int)txtbox.TextWrapping;
        }
    }
}
