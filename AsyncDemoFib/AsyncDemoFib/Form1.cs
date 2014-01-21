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

namespace AsyncDemoFib
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            int n = (int)numericUpDown1.Value;
            var result = await Run(n);
            label1.Text = result;
        }

        private Task<string> Run(int n)
        {
            return Task.Run(() => Fib(n).ToString());
        }

        private int Fib(int n)
        {
            return Fib(n, 0, 1);
        }

        private int Fib(int n, int a, int b)
        {
            if (n == 0)
            {
                return 0;
            }

            if (n == 1)
            {
                return b;
            }

            // Met dank aan BZ is deze demo waardeloos omdat
            // het algoritme veel te snel is geworden.
            return Fib(n - 1, b, a + b);
        }
    }
}
