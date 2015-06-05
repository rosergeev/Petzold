using Windows.Foundation;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HowToAsync1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Color clr;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void OnButtonClick(object sender, RoutedEventArgs args)
        {
            MessageDialog msgdlg = new MessageDialog("Choose a color", "How To Async #1");
            msgdlg.Commands.Add(new UICommand("Red", null, Colors.Red));
            msgdlg.Commands.Add(new UICommand("Green", null, Colors.Green));
            msgdlg.Commands.Add(new UICommand("Blue", null, Colors.Blue));

            IAsyncOperation<IUICommand> asyncOp = msgdlg.ShowAsync();
            asyncOp.Completed = OnMessageDialogShowAsyncCompleted;
        }

        private void OnMessageDialogShowAsyncCompleted(IAsyncOperation<IUICommand> asyncInfo, AsyncStatus asyncStatus)
        {
            IUICommand command = asyncInfo.GetResults();
            clr = (Color)command.Id;

            IAsyncAction asyncAction = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, OnDispatcherRunAsyncCallback);
        }

        private void OnDispatcherRunAsyncCallback()
        {
            contentGrid.Background = new SolidColorBrush(clr);
        }
    }
}
