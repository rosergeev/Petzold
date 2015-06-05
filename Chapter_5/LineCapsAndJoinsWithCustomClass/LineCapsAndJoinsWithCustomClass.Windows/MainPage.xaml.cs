using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LineCapsAndJoinsWithCustomClass
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Loaded += (s, e) =>
            {
                foreach (UIElement child in startLineCapPanel.Children)
                {
                    (child as LineCapRadioButton).IsChecked = (child as LineCapRadioButton).LineCapTag == polyline.StrokeStartLineCap;
                }
                foreach (UIElement child in endLineCapPanel.Children)
                {
                    (child as LineCapRadioButton).IsChecked = (child as LineCapRadioButton).LineCapTag == polyline.StrokeEndLineCap;
                }
                foreach (UIElement child in lineJoinPanel.Children)
                {
                    (child as LineJoinRadioButton).IsChecked = (child as LineJoinRadioButton).LineJoinTag == polyline.StrokeLineJoin;
                }
            };
        }

            private void OnStartLineCapRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            polyline.StrokeStartLineCap = (sender as LineCapRadioButton).LineCapTag;
        }

        private void OnEndLineCapRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            polyline.StrokeEndLineCap = (sender as LineCapRadioButton).LineCapTag;
        }

        private void OnLineJoinRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            polyline.StrokeLineJoin = (sender as LineJoinRadioButton).LineJoinTag;
        }
    }
}
