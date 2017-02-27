using System;
using System.Collections;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace ADCSB
{
    public class EnumerableTests
    {
        private readonly ITestOutputHelper output;

        public EnumerableTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ItemsNotMatchingPredicateAreNotInResult()
        {
            int[] items = { 1, 2, 3, 4, 5, 6, 7, 9, 8 };

            // Enumerable class zelf maken
            // Where methode zelf make
            // delegate om lambda mee te geven zelf maken
            IEnumerable<int> result = Enumerable.Where(items, i => i >= 2);

            Assert.DoesNotContain(1, result);
        }

        [Fact]
        public void SelectProjectsInputOnOutputUsingDelegate()
        {
            int[] items = { 1, 2, 3 };

            IEnumerable<string> result = Enumerable.Select(items, i => i.ToString());
            Assert.Equal(new string[] { "1", "2", "3" }, result);
        }

        delegate bool Where<T>(T input);
        delegate TResult Select<T, TResult>(T input);

        private class Enumerable
        {
            public static IEnumerable<T> Where<T>(IEnumerable<T> items, Where<T> where)
            {
                // hint: yield return :)
                foreach (var item in items)
                {
                    if (where(item))
                    {
                        yield return item;
                    }
                }
            }

            public static IEnumerable<TResult> Select<T, TResult>(IEnumerable<T> items, Select<T, TResult> select)
            {
                foreach (var item in items)
                {
                    yield return select(item);
                }
            }
        }

        [Fact]
        public void YieldReturn()
        {
            int[] items = { 1, 2, 3, 4, 5, 6, 7, 9, 8 };

            // Enumerable class zelf maken
            // Where methode zelf make
            // delegate om lambda mee te geven zelf maken
            IEnumerable<int> result = EnumerableZonderYield.Where(items, i => i >= 2);

            foreach (var item in result)
            {
                output.WriteLine(item.ToString());
            }
        }

        private class EnumerableZonderYield
        {
            public static IEnumerable<T> Where<T>(IEnumerable<T> items, Where<T> where)
            {
                return new MyEnumerable<T>(items, where);
            }

            private class MyEnumerable<T> : IEnumerable<T>
            {
                private IEnumerator<T> items;
                private Where<T> where;

                public MyEnumerable(IEnumerable<T> items, Where<T> where)
                {
                    this.items = items.GetEnumerator();
                    this.where = where;
                }

                public IEnumerator<T> GetEnumerator()
                {
                    return new MyEmerator<T>(items, where);
                }

                IEnumerator IEnumerable.GetEnumerator()
                {
                    return GetEnumerator();
                }
            }
            private class MyEmerator<T> : IEnumerator<T>
            {
                private IEnumerator<T> items;
                private Where<T> where;

                public MyEmerator(IEnumerator<T> items, Where<T> where)
                {
                    this.items = items;
                    this.where = where;
                }

                public T Current => items.Current;

                object IEnumerator.Current => Current;

                public void Dispose()
                {
                    items.Dispose();
                }

                public bool MoveNext()
                {
                    bool hasNext = items.MoveNext();
                    while (hasNext && !where(items.Current))
                    {
                        hasNext = items.MoveNext();
                    }

                    return hasNext;
                }

                public void Reset()
                {
                    throw new NotImplementedException();
                }
            }

        }
    }
}
