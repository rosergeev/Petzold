using System.Collections.Generic;
using System.Reflection;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ColorList1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            IEnumerable<PropertyInfo> properties = typeof(Colors).GetTypeInfo().DeclaredProperties;

            foreach (var property in properties)
            {
                Color clr = (Color)property.GetValue(null);

                StackPanel vertStackPanel = new StackPanel
                {
                    VerticalAlignment = VerticalAlignment.Center
                };
                TextBlock txtblkName = new TextBlock
                {
                    Text = property.Name,
                    FontSize = 24
                };
                vertStackPanel.Children.Add(txtblkName);

                TextBlock txtblkRgb = new TextBlock
                {
                    Text = string.Format("{0:X2}-{1:X2}-{2:X2}-{3:X2}", clr.A, clr.R, clr.G, clr.B),
                    FontSize = 18
                };
                vertStackPanel.Children.Add(txtblkRgb);

                StackPanel horzStackPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal
                };

                Rectangle rectangle = new Rectangle
                {
                    Width = 72,
                    Height = 72,
                    Fill = new SolidColorBrush(clr),
                    Margin = new Thickness(6)
                };
                horzStackPanel.Children.Add(rectangle);
                horzStackPanel.Children.Add(vertStackPanel);

                stackPanel.Children.Add(horzStackPanel);
            }
        }
    }
}
