using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace ADCSB
{
    class CustomAwaiterResult : INotifyCompletion
    {
        public void OnCompleted(Action continuation)
        {
        }

        public bool IsCompleted { get; set; }

        public int GetResult()
        {
            return 0;
        }
    }

    static class AwaiterExtension
    {
        public static CustomAwaiterResult GetAwaiter(this int input)
        {
            return null;
        }
    }

    [TestClass]
    public class AsyncAndAwaitTests
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            await 3;
        }

        [TestMethod]
        public void ProcessDemo()
        {
            var p = Process.Start("notepad");
            p.Exited += (s, e) => Console.WriteLine("Klaar!");
        }
    }


}
