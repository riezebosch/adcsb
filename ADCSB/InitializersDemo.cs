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

            public override bool Equals(object obj)
            {
                if (!(obj is Persoon))
                {
                    return false;
                }

                var p2 = (Persoon)obj;

                return this.Naam == p2.Naam && this.Leeftijd == p2.Leeftijd;
            }

            public override int GetHashCode()
            {
                return this.Naam.GetHashCode() ^ this.Leeftijd.GetHashCode();
            }
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

            var p2 = new { Naam = "Piet", Leeftijd = (short)25 };
            Assert.AreEqual(p, p2);

            // Door het omdraaien van de properties worden verschillende
            // types gegenereeerd waardoor de equals nooit true kan opleveren.
            var p3 = new { Leeftijd = (short)25, Naam = "Piet" };
            Assert.AreNotEqual(p, p3);
            

            // Object is heel wat anders dan var
            object p4 = new { Naam = "Piet", Leeftijd = (short)25 };

            // Voorbeeldje van impliciete name
            var p5 = new Persoon { Naam = "Manuel" };
            var p6 = new { p5.Naam };
            
            // De naam van de property is impliciet overgenomen van het 
            // Persoon object wat gebruikt is in de initializer.
            Console.WriteLine(p6.Naam);

            // Waarom ik fan ben van var
            var items = new Dictionary<Tuple<string, int>, List<Persoon>>();
            Dictionary<Tuple<string, int>, List<Persoon>> items2 = new Dictionary<Tuple<string, int>, List<Persoon>>();
        }

        [TestMethod]
        public void VergelijkTweeObjecten()
        {
            var p3 = new Persoon { Naam = "Piet" };
            var p4 = new Persoon { Naam = "Piet" };

            Assert.AreEqual(p3, p4);
        }

        private static object Create()
        {
            return new { Aantal = 3};
        }
    }
}
