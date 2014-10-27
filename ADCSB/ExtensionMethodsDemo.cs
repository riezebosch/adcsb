using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Linq;

namespace ADCSB
{
    static class StringHelpers
    {
        public static string RemoveAllVowels(this string input)
        {
            var sb = new StringBuilder();
            foreach (var c in input)
            {
                if (!IsVowel(c))
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        private static bool IsVowel(char c)
        {
            return c == 'a' ||
                c == 'e' ||
                c == 'i' ||
                c == 'o' ||
                c == 'u';
        }
    }

    [TestClass]
    public class ExtensionMethodsDemo
    {


        [TestMethod]
        public void TestMethod1()
        {
            var input = "Pietje Puk";
            string result = StringHelpers.RemoveAllVowels(input);

            Assert.AreEqual("Ptj Pk", result);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var input = "Pietje Puk";
            string result = input.RemoveAllVowels();

            Assert.AreEqual("Ptj Pk", result);
        }


        [TestMethod]
        public void HetVerschilTussenMetEnZonderExtensionMethodsMetLinq()
        {
            int[] items = { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34 };
            
            var query1 = from i in items
                        where i % 2 == 0
                        select i * 3;

            var query2 = items
                .Where(i => i % 2 == 0)
                .Select(i => i * 3);

            var query3 = Enumerable.Select(
                Enumerable.Where(items, i => i % 2 == 0),
                i => i * 3);
        }

        [TestMethod]
        public void WatIsDeferredExecution()
        {
            int[] items = { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34 };

            var query1 = from i in items
                         where i % 2 == 0
                         select i * 3;

            Console.WriteLine(string.Join(", ", query1));

            items[1] = 2;
            Console.WriteLine(string.Join(", ", query1));
        }

        [TestMethod]
        public void WatIsEenEnumerator()
        {
            int[] items = { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34 };

            foreach (var item in items)
            {
                Console.WriteLine(item);
            }

            var enumerator = items.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }
        }
    }
}
