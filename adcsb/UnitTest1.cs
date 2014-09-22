using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace adcsb
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var p = new Persoon();
            Assert.AreEqual(12, p.Leeftijd);
        }

        [TestMethod]
        public void ObjectInitializers()
        {
            var p = new Persoon();
            p.Naam = "Manuel Riezebosch";
            p.Leeftijd = 32;
            PrintPersoon(p);

            PrintPersoon(new Persoon
            {
                Naam = "Manuel Riezebosch",
                Leeftijd = 32
            });

            using (var myDisposable = new MyDisposable { Leeftijd = 3 })
            {

            }
        }

        private void PrintPersoon(Persoon persoon)
        {
        }

        [TestMethod]
        public void CollectionInitializer()
        {
            var personen = new List<Persoon>
            {
                new Persoon { Naam = "Manuel Riezebosch", Leeftijd = 32 },
                new Persoon { Naam = "Ezra Riezebosch", Leeftijd = 4}
            };

            var dict = new Dictionary<string, Persoon>
            {
                { "Manuel", new Persoon { Naam = "Manuel Riezebosch" } }
            };

            int[] items1 = { 1, 2, 4, 5, 6, 7, 8, 9 };
            var items2 = new int[]{ 1, 2, 4, 5, 6, 7, 8, 9 };

            var x = new ClassMetEenAddMethode
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9
            };
        }

        [TestMethod]
        public void AnonymousTypes()
        {
            object test = new object();
            test = "Manuel";

            try
            {
                int i = (int)test;
                Assert.Fail();
            }
            catch (InvalidCastException)
            {
            }

            Console.WriteLine(test);

            var anonymous = new 
            { 
                Naam = "Manuel", 
                Leeftijd = 32 
            };

            var p1 = new Persoon { Leeftijd = 32, Naam = "Manuel Riezebosch" };
            var p2 = new { p1.Leeftijd, p1.Naam };

            var x = CanAnAnonymousTypeLeaveTheMethodScope();

            var a1 = new { X = 13, Date = new DateTime(2014, 09, 22) };
            var a2 = new { Date = new DateTime(2014, 09, 22), X = 13 };
            Assert.AreNotEqual(a1, a2);
        }



        private static object CanAnAnonymousTypeLeaveTheMethodScope()
        {
            return new { X = 13 };
        }

        [TestMethod]
        public void AutoImplementedPropertiesAndStructs()
        {
            DemoStruct s = new DemoStruct(12);
            s.MyProperty = 12;
            Console.WriteLine(s.MyProperty);
        }

        struct DemoStruct
        {
            public int MyProperty { get; set; }

            public DemoStruct(int data) : this()
            {
                
            }
            
        }

        [TestMethod]
        public void ExtensionMethods()
        {
            var input = "Manuel Riezebosch";
            
            // Als helper method
            var result1 = StringHelper.RemoveVowels(input);

            // Als extension method
            var result2 = input.RemoveVowels();

            Assert.AreEqual("Mnl Rzbsch", result1);
            Assert.AreEqual("Mnl Rzbsch", result2);
        }

        [TestMethod]
        public void LinqExtensions()
        {
            var items = new List<int>
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9
            };

            var collection = items
                .Where(i => i != 5)
                .Where(i => i > 0)
                .Select(i => i % 2 == 0);

            if (DateTime.Today.DayOfWeek == DayOfWeek.Monday)
            {
                collection = collection.Where(p => p);
            }

            foreach (var item in collection)
            {
                Console.WriteLine(item);   
            }

            items.Add(20);

            Console.WriteLine(" ---" );

            foreach (var item in collection)
            {
                Console.WriteLine(item);
            }

            Enumerable.Select(items, i => i % 2 == 0);
        }

        [TestMethod]
        public void TestClonable()
        {
            string input = "Manuel";
            var result = input.DeepClone();

            Assert.AreEqual("Manuel", result);
        }
    }
}
