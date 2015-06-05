using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace OrientableColorScroll
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

        private void OnGridSizeChanged(object sender, SizeChangedEventArgs args)
        {
            if (args.NewSize.Width > args.NewSize.Height)
            {
                secondColDef.Width = new GridLength(1, GridUnitType.Star);
                secondRowDef.Height = new GridLength(0);

                Grid.SetColumn(rectangleResult, 1);
                Grid.SetRow(rectangleResult, 0);
            }
            else
            {
                secondColDef.Width = new GridLength(0);
                secondRowDef.Height = new GridLength(1, GridUnitType.Star);

                Grid.SetColumn(rectangleResult, 0);
                Grid.SetRow(rectangleResult, 1);
            }
        }

        private void OnSliderValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            byte r = (byte)redSlider.Value;
            byte g = (byte)greenSlider.Value;
            byte b = (byte)blueSlider.Value;

            redValue.Text = r.ToString("X2");
            greenValue.Text = g.ToString("X2");
            blueValue.Text = b.ToString("X2");

            brushResult.Color = Color.FromArgb(255, r, g, b);
        }
    }
}
