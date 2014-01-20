using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqDemo
{
    delegate string SelectForSort(Person p);
    delegate TOut SelectForSort<TIn, TOut>(TIn p);

    class Person
    {
        public string Name { get; set; }
    }

    class Program
    {

        static void Main(string[] args)
        {
            var people = new List<Person>
            {
                new Person { Name = "Manuel"},
                new Person { Name = "Bastiaan"},
                new Person { Name = "Titus"}
            };
            
            var sorted1 = people.OrderBy(new Func<Person, string>(SelecteerNaam));
            var sorted2 = people.OrderBy(SelecteerNaam);
            var sorted3 = people.OrderBy(delegate(Person p)
            {
                return p.Name;
            });

            var sorted4 = people.OrderBy(p => p.Name);
            var sorted5 = people.OrderBy((Person p) => p.Name);
            var sorted6 = people.OrderBy((Person p) => { return p.Name; });

            var sorted7 = MySlowSort(people, SelecteerNaam);
        }



        public static string SelecteerNaam(Person p)
        {
            return p.Name;
        }

        public static IEnumerable<T> MySlowSort<T, TOut>(IEnumerable<T> items, SelectForSort<T, TOut> selector)
            where TOut: IComparable
        {
            foreach (var item in items)
            {
                //selector(item).CompareTo()
            }
        }
    }
}
