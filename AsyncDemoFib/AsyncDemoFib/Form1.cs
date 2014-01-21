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
            try
            {
                var result = await Run(n);
                await Task.WhenAny(Task.Delay(5000), Task.Delay(2000));
                label1.Text = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Task<string> Run(int n)
        {
            return Task.Run(() => Fib(n).ToString());
        }

        private int Fib(int n)
        {
            if (n == 0)
            {
                return 0;
            }

            if (n == 1)
            {
                return 1;
            }

            //throw new ArgumentException();

            return Fib(n-2) + Fib(n-1);
        }
    }
}
