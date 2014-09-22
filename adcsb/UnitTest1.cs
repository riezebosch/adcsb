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

        [TestMethod]
        public void ObjectInitializers()
        {
            var p = new Persoon();
            p.Naam = "Manuel Riezebosch";
            p.Leeftijd = 32;
            PrintPersoon(p);

            PrintPersoon(new Persoon { 
                Naam = "Manuel Riezebosch", 
                Leeftijd = 32 
            });

            using (var myDisposable = new MyDisposable { Leeftijd = 3})
            {

            }
        }

        private void PrintPersoon(Persoon persoon)
        {
            throw new NotImplementedException();
        }
    }
}
