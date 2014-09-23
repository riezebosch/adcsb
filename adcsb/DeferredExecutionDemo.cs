using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace adcsb
{
    [TestClass]
    public class DeferredExecutionDemo
    {
        [TestMethod]
        public void TestMethod1()
        {
            int[] items = { 1, 2, 5, 2, 7, 8, 4 };
            IEnumerable<int> query = 
                Enumerable.Where(items, i => i > 5);

            foreach (var item in query)
            {
                Console.WriteLine(item);
            }

            using (IEnumerator<int> enumerator = query.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Console.WriteLine(enumerator.Current);
                }
            }
        }
    }
}
