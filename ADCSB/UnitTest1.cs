using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ADCSB
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void PartialClassEnPartialMethodDemo()
        {
            var p = new Persoon();
            p.GeboorteDatum = new DateTime(1982, 4, 5);

            Assert.AreEqual(34, p.BerekenLeeftijd());
        }
    }
}
