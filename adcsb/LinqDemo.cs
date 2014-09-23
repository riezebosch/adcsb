using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace adcsb
{
    [TestClass]
    public class LinqDemo
    {
        [TestMethod]
        public void TestMethod1()
        {
            var personen = new List<Persoon>
            {

            };

            var query1 = personen.OrderBy(p => p.Naam).ThenBy(p => p.Leeftijd);
            var query2 = from p in personen
                         orderby p.Naam, p.Leeftijd descending
                         select p;

        }

        [TestMethod]
        public void GroupClause()
        {
            string[] plaatsen = { "Veenendaal", "Utrecht", "Gouda", "Raalte", "Weesp" };
            var query = from plaats in plaatsen
                        group plaats by plaats.Length into p
                        orderby p.Key
                        select p;
                        

            foreach (var item in query)
            {
                Console.WriteLine(item.Key);

                foreach (var plaats in item)
                {
                    Console.WriteLine("  {0}", plaats);
                }

            }

            var gouda = plaatsen.First(p => p.Length == 5);
            Assert.IsNotNull(gouda);
        }

        [TestMethod]
        public void CartesischProduct()
        {
            char[] items1 = { 'a', 'b', 'c', 'd' };
            int[] items2 = { 1, 2, 3, 4, 5 };

            var query = from a in items1
                        from b in items2
                        select string.Format("{0}{1}", a, b);

            foreach (var item in query)
            {
                Console.WriteLine(item);
            }
        }


    }
}
