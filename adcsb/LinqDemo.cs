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
    }
}
