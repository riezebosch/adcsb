using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace adcsb
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var p = new Persoon();
            Assert.AreEqual(12, p.Leeftijd);
        }
    }
}
