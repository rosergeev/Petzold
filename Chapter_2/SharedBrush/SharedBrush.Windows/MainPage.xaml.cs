using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SharedBrush
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            Resources.Add("fontFamily", new FontFamily("Times New Roman"));
            this.InitializeComponent();

            TextBlock txtblk = (Content as Grid).Children[1] as TextBlock; ;
            LinearGradientBrush brush = txtblk.Foreground as LinearGradientBrush;
            brush.StartPoint = new Point(0, 1);
            brush.EndPoint = new Point(0, 0);
        }
    }
}
