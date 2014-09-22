using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
    }
}
