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

        private void button1_Click(object sender, EventArgs e)
        {
            label2.Text = "Starting calculation";

            Task.Run(() =>
            {
                int n = int.Parse(textBox1.Text);
                return Fib(n);
            }).ContinueWith(t =>
            {
                UpdateLabel(t.Result.ToString());
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void UpdateLabel(string t)
        {
            label2.Text = "Finished";
            label1.Text = t;
        }

        private int Fib(int n)
        {
            if (n <= 1)
                return n;

            return Fib(n - 1) + Fib(n - 2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Test");
        }
    }
}
