using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ADCSB
{
    [TestClass]
    public class LinqTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var list = new List<Person>
            {
                new Person{ Age = 23, Name = "Pete" },
                new Person{ Age = 31, Name = "John" },
                new Person { Name = "Paul", Age = 12 },
                new Person { Age = 31, Name = "John" },
                new Person { Age = 23, Name = "Pete" }
            };

            int min = 23;
            var result = from p in list
                         where p.Age > min
                         select new { p.Age, p.Name };

            //foreach (var p in result)
            //{
            //    Console.WriteLine(p.Name);
            //}

            var result2 = from p in result
                          where p.Name.StartsWith("J")
                          select p; 

            min = 22;
            foreach (var p in result2)
            {
                Console.WriteLine(p.Name);
            }
        }

        [TestMethod]
        public void MyTestMethod()
        {
            var items = new int[] { 5, 12, 3 };
            IEnumerable<int> lessThanTen = 
                items.Where(n => n < 10)
                        .OrderBy(n => n)
                        .Select(n => n * 10);

            foreach (var item in lessThanTen)
            {
                Console.WriteLine(item);
            }

            var i1 = items.Where(n => n < 10);
            var i2 = i1.OrderBy(n => n);
            var i3 = i2.Select(n => n * 10);
        }

        [TestMethod]
        public void DemoVanGroupInLinq()
        {
            var list = new List<Person>
            {
                new Person{ Age = 23, Name = "Pete" },
                new Person{ Age = 31, Name = "John" },
                new Person { Name = "Paul", Age = 12 },
                new Person { Age = 31, Name = "John" },
                new Person { Age = 23, Name = "Pete" }
            };

            IEnumerable<IGrouping<int, Person>> group = from p in list
                        group p by p.Name.Length;

            foreach (IGrouping<int, Person> g in group)
            {
                Console.WriteLine(g.Key);
                foreach (var person in g)
                {

                }
            }

        }

        [TestMethod]
        public void FromInFrom()
        {
            List<Person> beatles = new List<Person>{
    new Person{Name="Paul",  Instruments =      new List<string>{"Bass", "Guitar", "Vocals"}},
    new Person{Name="John", Instruments =       new List<string>{"Guitar", "Piano", "Vocals"}},
    new Person{Name="George", Instruments =        new List<string>{"Guitar", "Vocals"}},
    new Person{Name="Ringo", Instruments         = new List<string>{"Drums", "Vocals" }}};

            var singersAndGuitarPlayers =
                from person in beatles
                //from instrument in person.Instruments
                where person.Instruments.Contains("Guitar") || person.Instruments.Contains("Vocals")
                select person.Name;
        }

        [TestMethod]
        public void GroupInto()
        {
            var list = new List<Person>
            {
                new Person{ Age = 23, Name = "Peter" },
                new Person{ Age = 31, Name = "Johan" },
                new Person { Name = "Paul", Age = 12 },
                new Person { Age = 31, Name = "John" },
                new Person { Age = 23, Name = "Pete" }
            };

            var result = from p in list
                         group p by p.Age into citizen
                         where citizen.Count() > 1
                         from c in citizen
                         where c.Name.Length > 3
                         orderby c.Name
                         select c;

            foreach (var item in result)
            {
                    Console.WriteLine(item.Name);
            }
        }

        class Student
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        class Training
        {
            public int StudentId { get; set; }
            public string Description { get; set; }
        }

        [TestMethod]
        public void Joins()
        {
            var people = new List<Student>
            {
                new Student { Id = 5, Name = "paul" },
                new Student { Id = 7, Name = "richard" }
            };

            var trainings = new List<Training>
            {
                new Training { StudentId = 123, Description = "Introduction to C#" },
                new Training { StudentId = 751, Description = "Scala for the brave of heart" },
                new Training { StudentId = 5, Description = "Introduction to C#" },
            };

            var result = from p in people
                         join t in trainings on p.Id equals t.StudentId into trainingPerStudent
                         from t in trainingPerStudent.DefaultIfEmpty()
                         select new { p, t };

            foreach (var item in result)
            {
                Console.WriteLine($"{item.p.Name} {item.t?.Description}");
            }
        }

        [TestMethod]
        public void WatIsHetVerschilTussenEenLambdaEnEenExpressie()
        {
            Func<int, int> f1 = i => i * i;
            f1(10);
            Console.WriteLine(f1);

            Expression<Func<int, int>> f2 = i => i * i;
            f2.Compile()(10);
            Console.WriteLine(f2);
        }
    }
}
