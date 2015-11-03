using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        CancellationTokenSource source;
        private async void button1_Click(object sender, EventArgs e)
        {
            label2.Text = "Starting calculation";
            source = new CancellationTokenSource();

            Task<int> algo = FibAsync();
            Task delay = Task.Delay(TimeSpan.FromSeconds(25), source.Token);
            
            await Task.WhenAny(algo, delay);
            if (algo.IsCompleted && !algo.IsFaulted)
            {
                UpdateLabel(algo.Result.ToString());
            }
            else
            {
                source.Cancel();
                UpdateLabel("Timeout, algo took more than 5 seconds.");
            }

            source = null;
        }

        private Task<int> FibAsync()
        {
            var progress = new Progress<int>((i) => label2.Text = $"Bezig met nummer: {i}.");
            int n = int.Parse(textBox1.Text);

            return Task.Run(() =>
            {
                return Fib(n, source.Token, progress);
            });
        }

        private void UpdateLabel(string t)
        {
            label2.Text = "Finished";
            label1.Text = t;
        }

        private int Fib(int n, 
            CancellationToken token,
            IProgress<int> progress, bool left = true)
        {
            token.ThrowIfCancellationRequested();

            if (n <= 1)
                return n;

            var result = 
                Fib(n - 1, token, progress, left) + 
                Fib(n - 2, token, progress, false);

            // Only printing the progress from the left
            // recursion because otherwise we are flooding
            // the main thread with progress info.
            if (left)
            {
                progress.Report(n);
            }

            return result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (source != null)
            {
                source.Cancel();
            }
        }
    }
}
