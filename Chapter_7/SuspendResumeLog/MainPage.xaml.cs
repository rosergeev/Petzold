using System;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SuspendResumeLog
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        StorageFile logFile;
        public MainPage()
        {
            this.InitializeComponent();

            Loaded += OnLoaded;

            Application.Current.Suspending += OnAppSuspending;
            Application.Current.Resuming += OnAppResuming;
        }

        async private void OnAppResuming(object sender, object e)
        {
            txtbox.Text += $"Resuming at {DateTime.Now.ToString()}\r\n";
            await FileIO.WriteTextAsync(logFile, txtbox.Text);
        }

        async private void OnAppSuspending(object sender, Windows.ApplicationModel.SuspendingEventArgs args)
        {
            SuspendingDeferral deferral = args.SuspendingOperation.GetDeferral();

            txtbox.Text += $"Suspending at {DateTime.Now.ToString()}\r\n";
            await FileIO.WriteTextAsync(logFile, txtbox.Text);

            deferral.Complete();
        }

        async private void OnLoaded(object sender, RoutedEventArgs e)
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            logFile = await localFolder.CreateFileAsync("logfile.txt", CreationCollisionOption.OpenIfExists);

            txtbox.Text = await FileIO.ReadTextAsync(logFile);

            txtbox.Text += $"Launching at {DateTime.Now.ToString()}\r\n";
            await FileIO.WriteTextAsync(logFile, txtbox.Text);
        }
    }
}
