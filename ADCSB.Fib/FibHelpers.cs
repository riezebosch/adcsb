using System;
using System.Threading;

namespace ADCSB.Fib
{
    public class FibHelpers
    {
        public static int Fib(int n, CancellationToken? token = null)
        {
            if (n < 0)
            {
                throw new ArgumentException($"{nameof(n)} mag niet minder zijn dan 0 maar is {n}", nameof(n));
            }
            token?.ThrowIfCancellationRequested();

            if (n <= 1)
                return n;
            return Fib(n - 1, token) + Fib(n - 2, token);
        }
    }
}