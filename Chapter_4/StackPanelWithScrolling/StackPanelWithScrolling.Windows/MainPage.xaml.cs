using System.Collections.Generic;
using System.Reflection;
using Windows.UI;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace StackPanelWithScrolling
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

            foreach (PropertyInfo property in properties)
            {
                Color clr = (Color)property.GetValue(null);
                TextBlock txtblk = new TextBlock();
                txtblk.Text = string.Format("{0} \x2014 {1:X2}-{2:X2}-{3:X2}-{4:X2}", property.Name, clr.A, clr.R, clr.G, clr.B);
                stackPanel.Children.Add(txtblk);
            }
        }
    }
}
