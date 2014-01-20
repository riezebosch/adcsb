using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterVariableDemo
{
    delegate void Initialize(out int i);
    class Program
    {
        class Person
        {
            public string Name { get; set; }
        }

        class ExtraWrapper
        {
            public string extra;
        }
        static void Main(string[] args)
        {
            var wrapper = new ExtraWrapper();
            wrapper.extra = "ditwordterachteraangeplakt";

            var people = new List<Person>
            {
                new Person
                {
                    Name = "Manuel"
                }, new Person
                {
                    Name = "Paul"
                }
            };

            Print(people, p => { wrapper.extra += "_"; return p.Name + wrapper.extra; });

            Console.WriteLine(wrapper.extra);
        }

        static string Selector(Person p)
        {
            return p.Name;// +extra;
        }

        static void Print<T>(IEnumerable<T> items, Func<T, object> selector)
        {
            foreach (var item in items)
            {
                Console.WriteLine(selector(item));
            }
        }
    }
}
