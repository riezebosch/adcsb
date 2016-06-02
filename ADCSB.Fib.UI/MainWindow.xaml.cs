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

            var fib = CalculateFib(input);
            await Task.WhenAny(fib, Task.Delay(TimeSpan.FromSeconds(30)));

            label.Content = fib.IsCompleted ? fib.Result.ToString() : "timeout";
        }

        private Task<int> CalculateFib(int n)
        {
            return Task.Run(() => FibHelpers.Fib(n));
        }
    }
}
