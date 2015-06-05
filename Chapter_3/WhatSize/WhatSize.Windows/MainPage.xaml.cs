using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WhatSize
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

        private void OnPageSizeChanged(object sender, SizeChangedEventArgs args)
        {
            widthText.Text = args.NewSize.Width.ToString();
            heightText.Text = args.NewSize.Height.ToString();
        }
    }
}
