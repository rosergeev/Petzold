using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace DependencyPropertiesWithBindings
{
    public sealed partial class GradientButton : Button
    {
        public static DependencyProperty Color1Property { get; private set; }
        public static DependencyProperty Color2Property { get; private set; }

        static GradientButton()
        {
            Color1Property = DependencyProperty.Register("Color1", typeof(Color), typeof(GradientButton), new PropertyMetadata(Colors.White));
            Color2Property = DependencyProperty.Register("Color2", typeof(Color), typeof(GradientButton), new PropertyMetadata(Colors.Black));
        }
        
        public GradientButton()
        {
            this.InitializeComponent();
        }

        public Color Color1
        {
            get { return (Color)GetValue(Color1Property); }
            set { SetValue(Color1Property, value); }
        }

        public Color Color2
        {
            get { return (Color)GetValue(Color2Property); }
            set { SetValue(Color2Property, value); }
        }
    }
}
