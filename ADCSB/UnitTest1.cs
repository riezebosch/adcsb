﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections;
using Shouldly;

namespace ADCSB
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void PartialClassEnPartialMethodDemo()
        {
            var p = new Persoon();
            p.GeboorteDatum = new DateTime(1982, 4, 5);

            Assert.AreEqual(34, p.BerekenLeeftijd());
        }

        [TestMethod]
        public void ObjectInitializersDemo()
        {
            var p1 = new Persoon();
            p1.GeboorteDatum = new DateTime(1982, 4, 5);

            var p2 = new Persoon { GeboorteDatum = new DateTime(1982, 4, 5) };
        }

        [TestMethod]
        public void ArrayInitializersDemo()
        {
            var people1 = new List<Persoon>();
            people1.Add(new Persoon { GeboorteDatum = new DateTime(1982, 4, 5) });

            var people2 = new List<Persoon>
            {
                new Persoon { GeboorteDatum = new DateTime(1982, 4, 5) }
            };

            var people3 = new Dictionary<string, Persoon>
            {
                { "PIET", new Persoon { GeboorteDatum = new DateTime(1982, 4, 5) } }
            };

            int[] getallen1 = { 1, 2, 3, 4, 5 };
            var getallen2 = new[] { 1, 2, 3, 4, 5 };
            var getallen3 = new object[] { 1, 2, 3, 4, 5, "getal" };
        }

        [TestMethod]
        public void ObjectInitializerMetCollectionDemo()
        {
            var p = new Persoon
            {
                GeboorteDatum = new DateTime(1982, 4, 5),
                //Aankopen = new List<Aankoop>
                //{
                //    new Aankoop(),
                //    new Aankoop()
                //}
            };

            foreach (var a in p.Aankopen)
            {
                Console.WriteLine(a.Omschrijving);
            }
        }

        [TestMethod]
        public void CollectionInitializerOpEigenType()
        {
            var q = new DemoType { 0, 1, 2, 3, 4, 5, 6 };
        }

        private class DemoType : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                return null;
            }

            public void Add(int item)
            {
            }
        }

        [TestMethod]
        public void AnonymousTypesDemo()
        {
            var a = new { GeboorteDatum = new DateTime(1982, 4, 5) };
            Console.WriteLine(a.GeboorteDatum);

            var p = IetsMetEenAnymousType();
            Console.WriteLine(p.ToString());
        }

        public static object IetsMetEenAnymousType()
        {
            var p = new Persoon { GeboorteDatum = new DateTime(1982, 4, 5) };
            return new { Leeftijd = 34, p.GeboorteDatum };
        }

        [TestMethod]
        public void AnonymousTypeGeneratedDemo()
        {
            var p1 = new { Leeftijd = 34, Naam = "Piet" };
            var p2 = new { Leeftijd = 23, Naam = "Klaas" };

            p1.GetType().ShouldBeSameAs(p2.GetType());

            var p3 = new { Naam = "Piet", Leeftijd = 34 };
            p1.GetType().ShouldNotBeSameAs(p3.GetType());

            var p4 = new { Leeftijd = 34, Naam = "Piet" };
            p1.ShouldNotBe(p2);

            //  Kan niet, omdat p1 en p3 voor de compiler
            //  al twee verschillende types zijn!!
            //p1.ShouldNotBe(p3);

            // Deze is bijzonder, blijkbaar wordt bij anonymous types
            // de Equals (en ook de GetHashCode) door de compiler overriden.
            p1.ShouldBe(p4);

            // Want dat gedrag is helemaal niet normaal bij classes!
            var p5 = new Persoon { GeboorteDatum = new DateTime(1982, 4, 5) };
            var p6 = new Persoon { GeboorteDatum = new DateTime(1982, 4, 5) };
            p6.ShouldNotBe(p5);
        }

        [TestMethod]
        public void AutoImplementsProperties()
        {
            var ap = new AutoImplmemtedPropertiesDemo();

            ap.setZoDoetJavaHet(13);
            Console.WriteLine(ap.getZoDoetJavaHet());

            ap.ZoKwamDotNetErmee = 13;
            Console.WriteLine(ap.ZoKwamDotNetErmee);

            ap.ReadOnly3(16);
            ap.ReadOnly4(16);
        }

        class AutoImplmemtedPropertiesDemo
        {
            private int _zoDoetJavaHet;
            public void setZoDoetJavaHet(int iets)
            {
                _zoDoetJavaHet = iets;
            }

            public int getZoDoetJavaHet()
            {
                return _zoDoetJavaHet;
            }


            private int _zoKwamDotNetErmee;

            public int ZoKwamDotNetErmee
            {
                get { return _zoKwamDotNetErmee; }
                set { _zoKwamDotNetErmee = value; }
            }

            public int DitWasEenVerbeteringOmdatHetGewoonTypewerkScheelt { get; set; }

            public int BeetjeReadOnlyInElkGevalVanBuitenaf { get; private set; }

            public AutoImplmemtedPropertiesDemo()
            {
                // Read-only properties zetten van binnenuit, bijvoorbeeld
                // in de constructor
                BeetjeReadOnlyInElkGevalVanBuitenaf = 13;
            }

            public int DeCSharp6ReadOnlyOplossing { get; } = 13;

            public void OverschrijfHetReadonlyPropertyVanCSharp6(int waarde)
            {
                // Deze kan ik van binnenuit wel overschrijven,
                // aangezien de setter slechts 'private' is.
                BeetjeReadOnlyInElkGevalVanBuitenaf = waarde;

                // Deze mag ik niet overschrijven, de compiler
                // herkent dat het propertie read-only is!
                //DeCSharp6ReadOnlyOplossing = waarde;
            }

            public int ReadOnly1 { get { return 16; } }


            public int ReadOnly2 => 16;

            public int ReadOnly3(int input)
            {
                return 16 * input;
            }

            public int ReadOnly4(int input) => 16 * input;
        }

        [TestMethod]
        public void AutoImplementedPropertiesMetStructs()
        {
            MyStruct s1 = new MyStruct();
            s1.X = 5;
            s1.MyProperty = 13;

            Console.WriteLine(s1.X);
            Console.WriteLine(s1.Y);

            MyStruct s2 = s1;
            s2.X = 13;

            Console.WriteLine(s1.X);
            Console.WriteLine(s2.X);

           
        }

        struct MyStruct
        {
            public int X;

            public int Y;

            public int MyProperty { get; set; }
        }
    }
}
