using System;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SimpleContextDialog
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

        async private void OnTextBlockRightTapped(object sender, RightTappedRoutedEventArgs args)
        {
            StackPanel stackPanel = new StackPanel();

            Button btn1 = new Button
            {
                Content = "Larger font",
                Tag = 1.2,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(12)
            };
            btn1.Click += OnButtonClick;
            stackPanel.Children.Add(btn1);

            Button btn2 = new Button
            {
                Content = "Smaller font",
                Tag = 1 / 1.2,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(12)
            };
            btn2.Click += OnButtonClick;
            stackPanel.Children.Add(btn2);

            string[] names = { "Red", "Green", "Blue" };
            Color[] colors = { Colors.Red, Colors.Green, Colors.Blue };

            for (int i = 0; i < names.Length; i++)
            {
                RadioButton radioButton = new RadioButton
                {
                    Content = names[i],
                    Foreground = new SolidColorBrush(colors[i]),
                    IsChecked = (textBlock.Foreground as SolidColorBrush).Color == colors[i],
                    Margin = new Thickness(12)
                };
                radioButton.Checked += OnRadioButtonChecked;
                stackPanel.Children.Add(radioButton);
            }

            Border border = new Border
            {
                Child = stackPanel,
                Background = this.Resources["ApplicationPageBackgroundThemeBrush"] as SolidColorBrush,
                BorderBrush = this.Resources["ApplicationForegroundThemeBrush"] as SolidColorBrush,
                BorderThickness = new Thickness(1),
                Padding = new Thickness(24)
            };

            Popup popup = new Popup
            {
                Child = border,
                IsLightDismissEnabled = true
            };

            border.Loaded += (s, e) =>
            {
                Point point = args.GetPosition(this);
                point.X -= border.ActualWidth / 2;
                point.Y -= border.ActualHeight;

                popup.HorizontalOffset = Math.Min(this.ActualWidth - border.ActualWidth - 24, Math.Max(24, point.X));
                popup.VerticalOffset = Math.Min(this.ActualHeight - border.ActualHeight - 24, Math.Max(24, point.Y));

                btn1.Focus(FocusState.Programmatic);
            };

            popup.IsOpen = true;
        }
        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            textBlock.FontSize *= (double)(sender as Button).Tag;
        }

        private void OnRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            textBlock.Foreground = (sender as RadioButton).Foreground;
        }

    }
}
