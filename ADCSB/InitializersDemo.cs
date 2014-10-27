using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace ADCSB
{
    [TestClass]
    public class InitializersDemo
    {
        class Persoon
        {
            public string Naam { get; set; }

            public int Leeftijd { get; set; }

            public IList<decimal> Declaraties { get; set; }
        }

        [TestMethod]
        public void TestMethod1()
        {
            var p1 = new Persoon();
            p1.Naam = "Piet";

            var p2 = new Persoon 
            { 
                Naam = "Piet", 
                Declaraties = new List<decimal>() 
                { 
                    12m, 
                    14m, 
                    1m 
                } 
            };

            int[] getallen = { 1, 2, 3, 4, 5, 6, 7, 8 };
            
            // In de query objecten aamaken én initialiseren
            var query = from getal in getallen
                        select new Persoon 
                        { 
                            Naam = "Piet", 
                            Leeftijd = getal 
                        };

            var query2 = getallen.Select(getal => new { LottoNummer = getal });
            foreach (var item in query2)
            {
                Console.WriteLine(item.LottoNummer);
            }
        }

        class FakeCollection : IEnumerable
        {
            public void Add(int i)
            {
                Console.WriteLine(i);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }

        [TestMethod]
        public void CollectionInitializerDemo()
        {
            var items = new FakeCollection { 1, 2, 3, 4, 5, 6 };
        }

        [TestMethod]
        public void AnonymousTypesDemo()
        {
            var p = new { Naam = "Piet", Leeftijd = (short)25 };
            Console.WriteLine(p.Naam);

            // Dit mag dus niet, blijkbaar is de gegenereerde property
            // spontaan read-only geworden :S
            //p.Naam = "Puk";
        }
    }
}
