using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WordFreq
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Uri uri = new Uri("http://www.gutenberg.org/cache/epub/2701/pg2701.txt");
        CancellationTokenSource cts;

        public MainPage()
        {
            this.InitializeComponent();
        }

        Task<IOrderedEnumerable<KeyValuePair<string,int>>> GetWordFrequenciesAsync(Stream stream,
                                                                                   CancellationToken cancellationToken,
                                                                                   IProgress<double> progress)
        {
            return Task.Run(async () =>
            {
                Dictionary<string, int> dictionary = new Dictionary<string, int>();
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    string line = await streamReader.ReadLineAsync();
                    while (line != null)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        progress.Report(100.0 * stream.Position / stream.Length);

                        string[] words = line.Split(' ', ',', '.', ';', ':');

                        foreach (string word in words)
                        {
                            string charWord = word.ToLower();

                            while (charWord.Length > 0 && !char.IsLetter(charWord[0]))
                                charWord = charWord.Substring(1);

                            while (charWord.Length > 0 && !char.IsLetter(charWord[charWord.Length - 1]))
                                charWord = charWord.Substring(0, charWord.Length - 1);

                            if (charWord.Length == 0)
                                continue;
                            if (dictionary.ContainsKey(charWord))
                                dictionary[charWord] += 1;
                            else
                                dictionary.Add(charWord, 1);
                        }
                        line = await streamReader.ReadLineAsync();
                    }
                }
                return dictionary.OrderByDescending(i => i.Value);
            }, cancellationToken);
        }

        async private void OnStartButtonClick(object sender, RoutedEventArgs e)
        {
            stackPanel.Children.Clear();
            progressBar.Value = 0;
            errorText.Text = "";
            startButton.IsEnabled = false;
            IOrderedEnumerable<KeyValuePair<string, int>> wordList = null;

            try
            {
                RandomAccessStreamReference streamRef = RandomAccessStreamReference.CreateFromUri(uri);
                using (IRandomAccessStream raStream = await streamRef.OpenReadAsync())
                {
                    using (Stream stream = raStream.AsStream())
                    {
                        cancelButton.IsEnabled = true;
                        cts = new CancellationTokenSource();

                        wordList = await GetWordFrequenciesAsync(stream, cts.Token, new Progress<double>(ProgressCallback));
                        cancelButton.IsEnabled = false;
                    }
                }
            }
            catch (OperationCanceledException)
            {
                progressBar.Value = 0;
                cancelButton.IsEnabled = false;
                startButton.IsEnabled = true;
                return;
            }
            catch(Exception exc)
            {
                progressBar.Value = 0;
                cancelButton.IsEnabled = false;
                startButton.IsEnabled = true;
                errorText.Text = "Error: " + exc.Message;
                return;
            }

            foreach (KeyValuePair<string, int> word in wordList)
            {
                if (word.Value > 1)
                {
                    TextBlock txtblk = new TextBlock
                    {
                        FontSize = 24,
                        Text = word.Key + "\x2014" + word.Value.ToString()
                    };
                    stackPanel.Children.Add(txtblk);
                }
                await Task.Yield();
            }
            startButton.IsEnabled = true;
        }

        private void OnCancelButtonClick(object sender, RoutedEventArgs e)
        {
            cts.Cancel();
        }

        private void ProgressCallback(double progress)
        {
            progressBar.Value = progress;
        }
    }
}
