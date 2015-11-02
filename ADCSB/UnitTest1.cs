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

        [TestMethod]
        public void DemoVanAnonymousTypes()
        {
            var p = new
            {
                Name = "Pete",
                Age = 31,
                Address = new
                {
                    Street = "Smalle Zijde",
                    ZipCode = "3904TG"
                }
            };

            Console.WriteLine(p.Name);

            object o = GeefEenAnonymousTypeTerug();
            Console.WriteLine(o.GetType().GetProperty("Age").GetValue(o));
            Console.WriteLine(o.GetType().FullName);
        }

        private object GeefEenAnonymousTypeTerug()
        {
            return new { Age = 45 };
        }

        [TestMethod]
        public void GenererenVanAnonymousTypes()
        {
            var p1 = new { Age = 23, Name = "Pete" };
            var p2 = new { Age = 31, Name = "John" };
            var p3 = new { Name = "Paul", Age = 12 };
            var p4 = new { Age = 31m, Name = "John" };
            var p5 = new { Age = 23, Name = "Pete" };

            Assert.AreEqual(p1.GetType(), p2.GetType());
            Assert.AreNotEqual(p1.GetType(), p3.GetType());
            Assert.AreNotEqual(p1.GetType(), p4.GetType());

            Assert.AreEqual(p1, p5);

            var p6 = new Person { Age = 31, Name = "John" };
            var p7 = new Person { Age = 31, Name = "John" };
            Person p8 = new Person { Age = 31, Name = "John" };
            Assert.AreNotEqual(p6, p7);
        }

        struct S
        {
            public string Name { get; set; }

            public S(string name) 
                // Blijkbaar is dit gefixt in C# 6 want vroeger moest je zelf de call naar de default constructor doen
                //: this() 
            {
                Name = name;
            }

        }

        [TestMethod]
        public void DefiniteAssignmentOnStructs()
        {
            S s1 = new S();
            s1.Name = "John";
            Console.WriteLine(s1.Name);
            
        }
    }
}
