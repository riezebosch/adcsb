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

namespace ProgressAndCancellationDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        CancellationTokenSource token;

        private async void button1_Click(object sender, EventArgs e)
        {
            var progress = new Progress<int>(i => progressBar1.Value = i);
            token = new CancellationTokenSource();

            try
            {
                await Run(progress, token.Token);
                button1.Text = "Finished!";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static Task Run(IProgress<int> progress, CancellationToken token)
        {
            return Task.Run(() =>
                {
                    for (int i = 0; i <= 100; i++)
                    {
                        token.ThrowIfCancellationRequested();

                        progress.Report(i);
                        Thread.Sleep(100);
                    }
                });
        }

        private void button2_Click(object sender, EventArgs e)
        {
                token.Cancel();
            
        }
    }
}
