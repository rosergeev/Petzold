using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SimpleKeypad
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        string inputString = "";
        char[] specialChars = { '*', '#' };

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void OnDeleteButtonClick(object sender, RoutedEventArgs e)
        {
            inputString = inputString.Substring(0, inputString.Length - 1);
            FormatText();
        }

        private void OnCharButtonClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            inputString += btn.Content as string;
            FormatText();
        }

        private void FormatText()
        {
            bool hasNonNumbers = inputString.IndexOfAny(specialChars) != -1;

            if (hasNonNumbers || inputString.Length < 4 || inputString.Length > 10)
                resultText.Text = inputString;
            else if (inputString.Length < 8)
                resultText.Text = $"{inputString.Substring(0, 3)}-{inputString.Substring(3)}";
            else
                resultText.Text = $"({inputString.Substring(0, 3)}) {inputString.Substring(3, 3)}-{inputString.Substring(6)}";

            deleteButton.IsEnabled = inputString.Length > 0;
        }
    }
}
