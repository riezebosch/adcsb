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

        private void button1_Click(object sender, EventArgs e)
        {
            var t = new Thread(Run);
            t.Start();
            
        }

        private void Run()
        {
            int n = 0;
            this.Invoke(new Action(() => n = (int)numericUpDown1.Value));
            
            string result = Fib(n).ToString();

            this.Invoke(new Action(() => label1.Text = result));
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

            return Fib(n-2) + Fib(n-1);
        }
    }
}
