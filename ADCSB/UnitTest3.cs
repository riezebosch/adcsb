using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Shouldly;
using System.Collections.Generic;
using System.Collections;

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
            fib.Reset();

            for (int i = 0; i < 10; i++)
            {
                fib.MoveNext();
                Console.WriteLine(fib.Current);
            }
        }


        class Fibonacci : IEnumerable<int>
        {
            class FibonacciEnumerator : IEnumerator<int>
            {
                private int next;

                public int Current
                {
                    get;
                    set;
                }

                object IEnumerator.Current
                {
                    get
                    {
                        return Current;
                    }
                }

                public void Dispose()
                {
                }

                public bool MoveNext()
                {
                    int temp = Current;
                    Current = next;
                    next = next + temp;

                    return true;
                }

                public void Reset()
                {

                    Current = 0;
                    next = 1;
                }
            }

            public IEnumerator<int> GetEnumerator()
            {
                return new FibonacciEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private IEnumerable<int> Fib()
        {
            return new Fibonacci();
        }
    }
}
