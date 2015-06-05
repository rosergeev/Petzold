using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SliderSketch
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

        private void OnSliderValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            polyline.Points.Add(new Point(xSlider.Value, ySlider.Value));
        }

        private void OnBorderSizeChanged(object sender, SizeChangedEventArgs args)
        {
            Border border = sender as Border;
            xSlider.Maximum = args.NewSize.Width - border.Padding.Left
                                                 - border.Padding.Right
                                                 - polyline.StrokeThickness;
            ySlider.Maximum = args.NewSize.Height - border.Padding.Top
                                                  - border.Padding.Bottom
                                                  - polyline.StrokeThickness;

        }
    }
}
