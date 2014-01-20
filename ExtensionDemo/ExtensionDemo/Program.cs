using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionDemo
{
    static class MyStringExtensions
    {
        public static string Reverse(this string input)
        {
            var sb = new StringBuilder();
            for (int i = input.Length - 1; i >= 0; i--)
            {
                sb.Append(input[i]);
            }

            return sb.ToString();
        }
    }

    static class ExtraExtensions
    {
        //public static string Reverse(this string message)
        //{
        //    return "tweede reverse";
        //}

        public static IEnumerable<int> Where(this IEnumerable<int> input, Func<int, bool> pred)
        {
            return null;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var result = MyStringExtensions.Reverse("input");
            Console.WriteLine(result);

            Console.WriteLine("Manuel".Reverse());
            Console.WriteLine(MyStringExtensions.Reverse("Manuel"));


            var items = new List<int> { 1, 3, 5, 6, 7, 8, 9, 34, 234 };
            items.Where(i => i % 2 == 0).
                Reverse().
                Select(i => i * 3);

            var where = Enumerable.Where(items, i => i % 2 == 0);
            var reverse = Enumerable.Reverse(where);
            var select = Enumerable.Select(where, i => i * 3);
        }

    }
}
