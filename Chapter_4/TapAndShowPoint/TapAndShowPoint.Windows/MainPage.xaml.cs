using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TapAndShowPoint
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnTapped(TappedRoutedEventArgs args)
        {
            Point pt = args.GetPosition(this);

            Ellipse ellipse = new Ellipse
            {
                Width = 3,
                Height = 3,
                Fill = this.Foreground
            };

            Canvas.SetLeft(ellipse, pt.X);
            Canvas.SetTop(ellipse, pt.Y);

            //ellipse.SetValue(Canvas.LeftProperty, pt.X);
            //ellipse.SetValue(Canvas.TopProperty, pt.Y);

            canvas.Children.Add(ellipse);

            TextBlock txtblk = new TextBlock
            {
                Text = $"({pt})",
                FontSize = 24
            };

            Canvas.SetLeft(txtblk, pt.X);
            Canvas.SetTop(txtblk, pt.Y);
            canvas.Children.Add(txtblk);

            args.Handled = true;

            base.OnTapped(args);
        }
    }
}
