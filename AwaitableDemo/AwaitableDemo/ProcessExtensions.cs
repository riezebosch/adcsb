using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AwaitableDemo
{
    static class ProcessExtensions
    {
        public static TaskAwaiter<int> GetAwaiter(this Process p)
        {
            var tcs = new TaskCompletionSource<int>();
            
            p.EnableRaisingEvents = true;
            p.Exited += (o, i) => tcs.TrySetResult(p.ExitCode);

            if (p.HasExited)
            {
                tcs.TrySetResult(p.ExitCode);
            }

            return tcs.Task.GetAwaiter();
        }
    }
}
