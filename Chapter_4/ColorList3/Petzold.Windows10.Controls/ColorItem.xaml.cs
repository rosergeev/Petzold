using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Petzold.Windows10.Controls
{
    public sealed partial class ColorItem : UserControl
    {
        public ColorItem(string name, Color clr)
        {
            this.InitializeComponent();

            rectangle.Fill = new SolidColorBrush(clr);
            txtblkName.Text = name;
            txtblkRgb.Text = $"{clr.A}-{clr.R}-{clr.G}-{clr.B}";
        }
    }
}
