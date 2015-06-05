using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ManualBrushAnimation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            CompositionTarget.Rendering += OnCompositionTargetRendering;
        }

        private void OnCompositionTargetRendering(object sender, object e)
        {
            RenderingEventArgs renderingArgs = e as RenderingEventArgs;
            double t = (0.25 * renderingArgs.RenderingTime.TotalSeconds) % 1;
            double scale = t < 0.5 ? 2 * t : 2 - 2 * t;

            byte gray = (byte)(255 * t);
            Color clr = Color.FromArgb(255, gray, gray, gray);
            contentGrid.Background = new SolidColorBrush(clr);

            gray = (byte)(255 - gray);
            clr = Color.FromArgb(255, gray, gray, gray);
            txtblk.Foreground = new SolidColorBrush(clr);
        }
    }
}
