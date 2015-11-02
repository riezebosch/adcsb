using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ADCSB
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var p = new Person();
            p.GeneratedMethod();
        }

        [TestMethod]
        public void DemoVanInitializers()
        {
            var p1 = new Person();
            p1.Name = "Pete";
            p1.Age = 23;

            var p2 = new Person(23) { Name = "Pete" };

            var p3 = new Person { Age = 23, Name = "Pete" };

            Print(p1);
            Print(new Person { Age = 36 });

            var p4 = new Person
            {
                Address = new Address
                {
                    Street = "Smalle Zijde"
                }
            };

            var p5 = new Person() { Name = "Pete" };
        }

        private void Print(Person p)
        {
            Console.WriteLine($"Person age = {p.Age}.");
        }
    }
}
