using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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

            PrintPersoon(new Persoon
            {
                Naam = "Manuel Riezebosch",
                Leeftijd = 32
            });

            using (var myDisposable = new MyDisposable { Leeftijd = 3 })
            {

            }
        }

        private void PrintPersoon(Persoon persoon)
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void CollectionInitializer()
        {
            var personen = new List<Persoon>
            {
                new Persoon { Naam = "Manuel Riezebosch", Leeftijd = 32 },
                new Persoon { Naam = "Ezra Riezebosch", Leeftijd = 4}
            };

            var dict = new Dictionary<string, Persoon>
            {
                { "Manuel", new Persoon { Naam = "Manuel Riezebosch" } }
            };

            int[] items1 = { 1, 2, 4, 5, 6, 7, 8, 9 };
            var items2 = new int[]{ 1, 2, 4, 5, 6, 7, 8, 9 };

            var x = new ClassMetEenAddMethode
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9
            };
        }
    }
}
