using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FibCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        CancellationTokenSource _tokenSource;

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var input = int.Parse(Input.Text);

            _tokenSource = new CancellationTokenSource();
            try
            {
                int nMax = 0;
                var progress = new Progress<int>(n => ProgressLabel.Content = n);
                Result.Content = await Task.Run(() => Fib(input, _tokenSource.Token, progress, true));
            }
            catch (OperationCanceledException)
            {
            }
        }

        private static int Fib(int n,
            CancellationToken token,
            IProgress<int> progress, bool report)
        {

            token.ThrowIfCancellationRequested();

            if (n <= 1)
            {
                return n;
            }
            
            checked
            {
                int result = Fib(n - 1, token, progress, report) +
                    Fib(n - 2, token, progress, false);

                if (report)
                {
                    progress.Report(n);
                }

                return result;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (_tokenSource != null)
            {
                _tokenSource.Cancel();
            }
        }
    }
}
