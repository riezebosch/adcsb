using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace ADCSB.Fib.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Fib0()
        {
            int n = 0;
            int fib = FibHelpers.Fib(n);
            fib.ShouldBe(0);
        }

        [TestMethod]
        public void Fib1()
        {
            int n = 1;
            int fib = FibHelpers.Fib(n);
            fib.ShouldBe(1);
        }

        [TestMethod]
        public void Fib2()
        {
            int n = 2;
            int fib = FibHelpers.Fib(n: n);
            fib.ShouldBe(1);
        }
    }
}