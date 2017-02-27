using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ADCSB
{
    public class EnumerableTests
    {
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
            public static IEnumerable<T> Where<T>(IEnumerable<T> items,  Where<T> where)
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
    }
}
