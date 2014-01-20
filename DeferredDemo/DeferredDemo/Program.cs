using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeferredDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            int divisor = 2;
            IEnumerable<int> items = new [] { 0, 1, 1, 2, 3, 5, 8, 13, 21 };

            //var iter = items.GetEnumerator();
            //while(iter.MoveNext())
            //{
            //    Console.WriteLine(iter.Current);
            //}
            
            
            items = items.Where(i => i % divisor == 0);
            //items = items.Select(i => i * 2);
            items = items.OrderBy(i => i).ToList();

            divisor = 3;

            foreach (var item in items)
            {
                Console.WriteLine(item);
            }

            divisor = 4;

            foreach (var item in items)
            {
                Console.WriteLine(item);
            }

            var people = new List<Persoon>
            {
                new Persoon
                {
                    Naam = "Manuel"
                },
                new Persoon
                {
                    Naam = "Manuel"
                },
                new Persoon
                {
                    Naam = "Piet"
                },
                new  Persoon{
                    Naam = "Kees"
                }
            };

            var grouped = from p in people
                          group p by p.Naam.Length;

            foreach (var item in grouped)
            {
                Console.WriteLine(item.Key);
                foreach (var p in item)
                {
                    Console.WriteLine("  {0}", p.Naam);
                }
            }
        }

        class Persoon
        {
            public string Naam { get; set; }
        }
    }
}
