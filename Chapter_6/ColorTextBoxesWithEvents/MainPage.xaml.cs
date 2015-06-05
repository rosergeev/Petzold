using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ColorTextBoxesWithEvents
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        RgbViewModel rgbViewModel;
        Brush textBoxTextBrush;
        Brush textBoxErrorBrush = new SolidColorBrush(Colors.Red);

        public MainPage()
        {
            this.InitializeComponent();

            textBoxTextBrush = this.Resources["TextBoxForegroundThemeBrush"] as SolidColorBrush;

            rgbViewModel = new RgbViewModel();
            rgbViewModel.PropertyChanged += OnRgbViewModelPropertyChanged;
            this.DataContext = rgbViewModel;

            rgbViewModel.Color = new UISettings().UIElementColor(UIElementType.Highlight);
        }

        private void OnRgbViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case "Red":
                    redTextBox.Text = rgbViewModel.Red.ToString("F0");
                    break;
                case "Green":
                    greenTextBox.Text = rgbViewModel.Green.ToString("F0");
                    break;
                case "Blue":
                    blueTextBox.Text = rgbViewModel.Blue.ToString("F0");
                    break;
                default:
                    break;
            }
        }

        private void OnTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            byte value;

            if (sender == redTextBox && Validate(redTextBox, out value))
                rgbViewModel.Red = value;

            if (sender == greenTextBox && Validate(greenTextBox, out value))
                rgbViewModel.Green = value;

            if (sender == blueTextBox && Validate(blueTextBox, out value))
                rgbViewModel.Blue = value;
        }

        private bool Validate(TextBox textBox, out byte value)
        {
            bool valid = byte.TryParse(textBox.Text, out value);
            textBox.Foreground = valid ? textBoxTextBrush : textBoxErrorBrush;
            return valid;
        }
    }
}
