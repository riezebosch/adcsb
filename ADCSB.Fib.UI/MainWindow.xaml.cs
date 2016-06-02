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

namespace ADCSB.Fib.UI
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

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            int input = int.Parse(textBox.Text);

            var source = new CancellationTokenSource();
            var fib = CalculateFib(input, source.Token);
            var timeout = Task
                .Delay(TimeSpan.FromSeconds(10))
                .ContinueWith(t => source.Cancel());

            await Task.WhenAny(fib, timeout);

            try
            {
                label.Content = fib.Result;
            }
            catch (AggregateException ex)
            {
                label.Content = string.Join(", ", ex.InnerExceptions.Select(x => x.Message));
            }
        }

        private Task<int> CalculateFib(int n, CancellationToken token)
        {
            return Task.Run(() => FibHelpers.Fib(n, token));
        }
    }
}
