using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Shouldly;
using System.Collections.Generic;
using System.Collections;
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

        [TestMethod]
        public void DefferedExecution()
        {
            int multiplier = 3;
            int[] items = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var query = (from i in items
                         where i % 2 == 0
                         select i * multiplier).ToList();

            multiplier = 2;

            foreach (var item in query)
            {
                Console.WriteLine(item);
            }

            multiplier = 1;

            foreach (var item in query)
            {
                Console.WriteLine(item);
            }
        }

        [TestMethod]
        public void DefferedExecutionMetEnumeratorEnFibonacciDemo()
        {
            var fib = Fib().GetEnumerator();

            for (int i = 0; i < 10; i++)
            {
                fib.MoveNext();
                Console.WriteLine(fib.Current);
            }
        }

        [TestMethod]
        public void YieldReturnInCombinatieMetLinq()
        {
            var query = from i in Fib()
                        where i % 2 == 0
                        select i * 3;

            foreach (var item in query.Take(10))
            {
                Console.WriteLine(item);
            }
        
        }

        private IEnumerable<int> Fib()
        {
            int current = 0;
            int next = 1;

            while (true)
            {
                int temp = current;
                current = next;
                next = next + temp;

                yield return current;
            }
        }

        [TestMethod]
        public void YieldReturnDemoOnzeEigenWhereImplementatie()
        {
            int[] items = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var query = items.OnzeEigenWhere(i => i > 5);

            query.ShouldBe(new int[] { 6, 7, 8, 9 });
        }
    }

    static class OnzeEigenLinqExtensions
    {
        public static IEnumerable<T> OnzeEigenWhere<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            foreach (var item in items)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }
    }
}
