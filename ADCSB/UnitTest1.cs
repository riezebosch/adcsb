using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections;

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

        [TestMethod]
        public void ObjectInitializersDemo()
        {
            var p1 = new Persoon();
            p1.GeboorteDatum = new DateTime(1982, 4, 5);

            var p2 = new Persoon { GeboorteDatum = new DateTime(1982, 4, 5) };
        }

        [TestMethod]
        public void ArrayInitializersDemo()
        {
            var people1 = new List<Persoon>();
            people1.Add(new Persoon { GeboorteDatum = new DateTime(1982, 4, 5) });

            var people2 = new List<Persoon>
            {
                new Persoon { GeboorteDatum = new DateTime(1982, 4, 5) }
            };

            var people3 = new Dictionary<string, Persoon>
            {
                { "PIET", new Persoon { GeboorteDatum = new DateTime(1982, 4, 5) } }
            };

            int[] getallen1 = { 1, 2, 3, 4, 5 };
            var getallen2 = new []{ 1, 2, 3, 4, 5 };
            var getallen3 = new object[] { 1, 2, 3, 4, 5, "getal" };
        }

        [TestMethod]
        public void ObjectInitializerMetCollectionDemo()
        {
            var p = new Persoon
            {
                GeboorteDatum = new DateTime(1982, 4, 5),
                //Aankopen = new List<Aankoop>
                //{
                //    new Aankoop(),
                //    new Aankoop()
                //}
            };

            foreach (var a in p.Aankopen)
            {
                Console.WriteLine(a.Omschrijving);
            }
        }

        [TestMethod]
        public void CollectionInitializerOpEigenType()
        {
            var q = new DemoType { 0, 1, 2, 3, 4, 5, 6 };
        }

        private class DemoType : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                return null;
            }

            public void Add(int item)
            {
            }
        }
    }
}
