using System;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace QuickNotes
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Loaded += OnLoaded;
            Application.Current.Suspending += OnAppSuspending;
        }

        async private void OnAppSuspending(object sender, Windows.ApplicationModel.SuspendingEventArgs args)
        {
            SuspendingDeferral deferral = args.SuspendingOperation.GetDeferral();
            await PathIO.WriteTextAsync("ms-appdata:///local/QuickNotes.txt", txtbox.Text);
            deferral.Complete();
        }

        async private void OnLoaded(object sender, RoutedEventArgs args)
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile storageFile = await localFolder.CreateFileAsync("QuickNotes.txt", CreationCollisionOption.OpenIfExists);

            txtbox.Text = await FileIO.ReadTextAsync(storageFile);
            txtbox.SelectionStart = txtbox.Text.Length;
            txtbox.Focus(FocusState.Programmatic);
        }
    }
}
