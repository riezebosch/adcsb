using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ADCSB
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int[] items = { 1, 4, 3, 6, 34, 563, 234, 56, 234, 45, 234, 23, 234, 234, 234, 234 };
            var query = from i in items
                        where i > 17
                        select i * 3;

            foreach (var item in query)
            {
                Console.WriteLine(item);
            }
        }

        [TestMethod]
        public void TestMethod2()
        {
            int[] items = { 1, 4, 3, 6, 34, 563, 234, 56, 234, 45, 234, 23, 234, 234, 234, 234 };
            var query = items
                .Where(i => i < 17)
                .Select(i => i * 3);

            foreach (var item in query)
            {
                Console.WriteLine(item);
            }
        }

        [TestMethod]
        public void DemoMetPartialClassesEnPartialMethods()
        {
            var target = new PartialDemo();
        }
    }
}
