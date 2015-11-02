using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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

        [TestMethod]
        public void DemoVanCollectionInitializers()
        {
            var list = new List<Person>
            {
                new Person { Name = "Pete" },
                new Person { Name = "John" }
            };

            var dict = new Dictionary<string, Person>
            {
                { "asdf", new Person { Name = "Pete" } },
                { "zasd", new Person { Name = "John" } }
            };

            int[] numbers1 = { 1, 2, 3, 4, 5 };
            var numbers2 = new[] { 1, 2, 3, 4, 5 };

            int[] numbers3 = new int[5];
            numbers3[0] = 1;
            numbers3[1] = 2;
            numbers3[2] = 3;
            numbers3[3] = 4;
            numbers3[4] = 5;

            //var numbers4 = { 1, 2, 3, 4, 5 };
        }
    }
}
