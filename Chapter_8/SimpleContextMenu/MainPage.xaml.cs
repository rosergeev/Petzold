using System;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SimpleContextMenu
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

        async private void OnTextBlockRightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            PopupMenu popupMenu = new PopupMenu();
            popupMenu.Commands.Add(new UICommand("Larger font", OnFontSizeChanged, 1.2));
            popupMenu.Commands.Add(new UICommand("Smaller font", OnFontSizeChanged, 1/1.2));
            popupMenu.Commands.Add(new UICommandSeparator());
            popupMenu.Commands.Add(new UICommand("Red", OnColorChanged, Colors.Red));
            popupMenu.Commands.Add(new UICommand("Green", OnColorChanged, Colors.Green));
            popupMenu.Commands.Add(new UICommand("Blue", OnColorChanged, Colors.Blue));

            await popupMenu.ShowAsync(e.GetPosition(this));
        }

        private void OnColorChanged(IUICommand command)
        {
            textBlock.Foreground = new SolidColorBrush((Color)command.Id);
        }

        private void OnFontSizeChanged(IUICommand command)
        {
            textBlock.FontSize *= (double)command.Id;
        }
    }
}
