using System;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HowToCancelAsync
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        IAsyncOperation<IUICommand> asyncOp;
        public MainPage()
        {
            this.InitializeComponent();
        }

        async private void OnButtonClick(object sender, RoutedEventArgs args)
        {
            MessageDialog msgdlg = new MessageDialog("Choose a color", "How To Async #1");
            msgdlg.Commands.Add(new UICommand("Red", null, Colors.Red));
            msgdlg.Commands.Add(new UICommand("Green", null, Colors.Green));
            msgdlg.Commands.Add(new UICommand("Blue", null, Colors.Blue));

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += OnTimerTick;
            timer.Start();

            asyncOp = msgdlg.ShowAsync();
            IUICommand command = null;

            try
            {
                command = await asyncOp;
            }
            catch (Exception)
            {
            }

            timer.Stop();

            if (command == null)
                return;
            
            Color clr = (Color)command.Id;
            contentGrid.Background = new SolidColorBrush(clr);
        }

        private void OnTimerTick(object sender, object e)
        {
            asyncOp.Cancel();
        }
    }
}
