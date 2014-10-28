using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            int n = int.Parse(textBox1.Text);

            var source = new CancellationTokenSource();
            var progress = new Progress<int>(i => label1.Text = "We zijn bij getal: " + i);
            var fib = Task.Run(() => Fib(n, source.Token, progress));

            await Task.WhenAny(fib, Task.Delay(5000));
            if (fib.IsCompleted)
            {
                label1.Text = fib.Result.ToString();
            }
            else
            {
                source.Cancel();

                MessageBox.Show("Duurt lang");
            }
        }

        private async Task<bool> IsHetAlVrijdag(IProgress<int> progress)
        {
            //Thread.Sleep(20000);


            for (int i = 0; i < 20; i++)
            {
                progress.Report(i);
                await Task.Delay(1000); 
            }

            return DateTime.Today.DayOfWeek == DayOfWeek.Friday;
        }

        private int Fib(int n, CancellationToken token, IProgress<int> progress)
        {
            token.ThrowIfCancellationRequested();

            // Slecht voorbeeld van Progress
            // omdat dit direct weer de UI
            // thread bezet houdt met een
            // overvloed van berichtjes.
            progress.Report(n);
            
            if (n <= 1)
            {
                return n;
            }
            
            return Fib(n - 1, token, progress) + 
                Fib(n - 2, token, progress);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            var progress = new Progress<int>(i => label1.Text = i.ToString());
            MessageBox.Show(string.Format("Vrijdag?: {0}", await IsHetAlVrijdag(progress)));
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {
                await ThrowException();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task ThrowException()
        {
            await Task.Delay(5000);
            throw new NotImplementedException();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo("notepad");
            p.Start();
            
            p.EnableRaisingEvents = true;
            p.Exited += (s, a) => MessageBox.Show("Notepad is weer gesloten");
        }
    }
}
