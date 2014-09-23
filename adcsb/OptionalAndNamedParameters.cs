using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace adcsb
{
    [TestClass]
    public class OptionalAndNamedParameters
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(35, Add(b: 12));
        }

        private static int Add(int a = 23, int b = 12)
        {
            return a + b;
        }
    }
}
