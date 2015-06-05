using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RainbowEight
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private double txtblkBaseSize;
        public MainPage()
        {
            this.InitializeComponent();

            Loaded += OnPageLoaded;
        }

        private void OnPageLoaded(object sender, RoutedEventArgs e)
        {
            txtblkBaseSize = txtblk.ActualHeight;
            CompositionTarget.Rendering += OnCompositionTargetRendering;
        }

        private void OnCompositionTargetRendering(object sender, object args)
        {
            txtblk.FontSize = this.ActualHeight / txtblkBaseSize;

            RenderingEventArgs renderingArgs = args as RenderingEventArgs;
            double t = (0.25 * renderingArgs.RenderingTime.TotalSeconds) % 1;

            for (int index = 0; index < gradientBrush.GradientStops.Count; index++)
                gradientBrush.GradientStops[index].Offset = index / 7.0 - t;
        }
    }
}
