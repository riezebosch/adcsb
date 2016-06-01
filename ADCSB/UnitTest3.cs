using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Shouldly;

namespace ADCSB
{
    [TestClass]
    public class LinqDemo
    {
        [TestMethod]
        public void DifferentSyntax()
        {
            int[] items = { 1, 2, 3, 4, 5, 6, 7, 8 };

            // Extension method syntax
            var result1 = items
                .Where(i => i < 5)
                .Where(i => i % 2 == 0)
                .OrderBy(i => i).Reverse();

            // Wordt vertaald door de compiler naar dit:
            var result2 = Enumerable.Reverse(
                Enumerable.Where(
                    Enumerable.Where(items, i => i < 5),
                    i => i % 2 == 0));

            // Is hetzelfde als comprehension syntax :)
            var result3 = from i in items
                          where i < 5
                          where i % 2 == 0
                          orderby i descending
                          select i;

            result1.ShouldBe(result2);
            result2.ShouldBe(result3);
        }
    }
}
