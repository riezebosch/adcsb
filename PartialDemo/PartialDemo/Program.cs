using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartialDemo
{
    class Program
    {
        static void Main(string[] args)
        {


            var p = new Person();
            p.Age = 13;
            p.Name = "Piet";
            Print(p);

            Print(new Person { Age = 13, Name = "Piet" });

            var p2 = Initialize("Manuel", 31);

            var m = new { Naam = "Manuel", Leeftijd = 31 };

            var personen = new List<Person>
            {
                new Person { Name = "Manuel", Age = 31},
                new Person { Name = "Bastiaan", Age = 31 }
            };

            personen.Select(q => new { q.Name, AgeInTenYears = q.Age + 10 });

            foreach (var item in personen)
            {

            }

            Console.WriteLine(m.GetType().FullName);
            var c = new CollectionInitializerDemo() { "Manuel", "Bastiaan", "Titus", "etc." };

        }

        static void Print(Person p)
        { }

        static Person Initialize(string name, int age )
        {
            return new Person { Name = name, Age = age };
        }
    }
}
