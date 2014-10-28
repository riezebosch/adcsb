using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace ADCSB
{
    delegate void NaamVanDeDelegate(int parameter);
    //delegate void NaamVanDeDelegate(int parameter1, int parameter2);
    //delegate bool NaamVanDeDelegate(int parameter1, int parameter2);

    delegate void NaamVanDeDelegateMetGenerics<T>(T parameter);
    delegate void NaamVanDeDelegateMetGenerics<T1, T2>(T1 p1, T2 p2);

    [TestClass]
    public class LambdaDemo
    {
        [TestMethod]
        public void EenDelegateIsEeBeschrijvingVanEenMethode()
        {
            var m1 = new NaamVanDeDelegate(Print);
            NaamVanDeDelegate m2 = Print;
        }

        private void Print(int parameter)
        {

        }

        [TestMethod]
        public void EenDelegateIsStandaardMulticast()
        {
            var m1 = new NaamVanDeDelegate(Print);
            NaamVanDeDelegate m2 = Print;

            m2 += Console.WriteLine;

            m2(3);
        }

        [TestMethod]
        public void WatIsHetVerschilTussenEventsEnDelegates()
        {
            var methods = typeof(ClassMetEventsEnDelegatesErop).GetMethods();
            foreach (var method in methods)
            {
                Console.WriteLine(method.Name);
            }

            var c = new ClassMetEventsEnDelegatesErop();
            c.Load1 += Console.WriteLine;
            c.Load2 += Console.WriteLine;

            // Nu komt het grote verschil:
            c.Load1(12);

            // Dit mag dus niet
            //c.Load2(12);

            c.Load1 = null;

            // Dit ook niet.
            //c.Load2 = null;
        }

        class ClassMetEventsEnDelegatesErop
        {
            public NaamVanDeDelegate Load1;
            public event NaamVanDeDelegate Load2;
        }

        [TestMethod]
        public void MyTestMethod()
        {
        }

        private static void DoetIetsMetDelegates(NaamVanDeDelegate methode, int input)
        {
            methode(input);
        }

        private static void DoetIetsMetDelegates<T>
            (NaamVanDeDelegateMetGenerics<T> methode, T input)
        {
            methode(input);
        }

        [TestMethod]
        public void BouwEenEigenWhereMethode()
        {
            int[] items = { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34 };
            foreach (var item in Where(items, IsEven))
            {
                Console.WriteLine(item);
            }

            foreach (var item in Where(items, IsGroterDanDrie))
            {
                Console.WriteLine(item);
            }

            // Met behulp van anonymous methods
            foreach (var item in Where(items, delegate(int item) { return item <= 21; }))
            {
                Console.WriteLine(item);
            }

            // Met behulp van Lambdas
            foreach (var item in Where(items, item => item <= 21))
            {
                Console.WriteLine(item);
            }

            // Op al deze regels staat precies hetzelfde!
            Func<int, bool> f = IsEven;
            f += delegate(int item) { return item % 2 == 0; };
            f += (int item) => { return item % 2 == 0; };
            f += item => { return item % 2 == 0; };
            f += item => item % 2 == 0;
        }

        private bool IsGroterDanDrie(int arg)
        {
            return arg > 3;
        }

        private static bool IsEven(int item)
        {
            return item % 2 == 0;
        }

        private static IEnumerable<int> Where(int[] items, Func<int, bool> predicate)
        {
            foreach (var item in items)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }

            yield return 3;
        }

        [TestMethod]
        public void IetsMetCapturedVariables()
        {
            int getal = 0;
            Action a = () => Console.WriteLine(getal);

            getal = 3;
            KanIkBijEenCapturedVariable(a);
        }

        private static void KanIkBijEenCapturedVariable(Action a)
        {
            a();
        }

        [TestMethod]
        public void FuncMetParameters()
        {
            int[] items = { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34 };
            var query = items.Select((i, index) => string.Format("{0} [{1}]", i, index));

            Console.WriteLine(string.Join(Environment.NewLine, query));
        }

        [TestMethod]
        public void DeferredExecution()
        {
            List<Person> band = new List<Person> {
                new Person{ Name="John"},
                new Person{ Name="Paul"},
                new Person{ Name="George"},
                new Person{ Name="Ringo"}
            };

            string filter = "e";

            var selection = band.Where(person => person.Name.Contains(filter));

            filter = "o";

            selection =
                selection.Where(person => person.Name.Contains(filter));

            Console.WriteLine("Found {0} persons.", selection.Count());
        }

        class A
        {
            public string Y { get; set; }
            public List<B> B { get; set; }
        }

        class B
        {
            public string X { get; set; }
        }

        [TestMethod]
        public void FromInEenFrom()
        {
            var items = new List<A>
            {
                new A
                {
                    B = new List<B>
                    {
                        new B { X = "a"},
                        new B { X = "b"}
                    }, Y = "A1"
                    },
                    new A{
                        B = new List<B>
                        {
                            new B { X = "c"},
                            new B { X = "d"}
                    }, Y = " A2"
                }
            };

            var query = from a in items
                        from b in a.B
                        select a.Y + b.X;

            // inderdaad, hier komt a, b, c, d
            foreach (var item in query)
            {
                Console.WriteLine(item);
            }
        }

        [TestMethod]
        public void FromInFromZonderRelatie()
        {
            int[] getallen = { 1, 2, 3, 4, 5 };
            char[] karakters = { 'a', 'b', 'c', 'd' };

            var query = from g in getallen
                        from k in karakters
                        select g + k.ToString();

            foreach (var item in query)
            {
                Console.WriteLine(item);
            }
        }

        [TestMethod]
        public void OrderByEnNogEenKeer()
        {
            List<Person> band = new List<Person> {
                new Person{ Name="John"},
                new Person{ Name="Paul"},
                new Person{ Name="George"},
                new Person{ Name="Ringo"}
            };

            var query = band.OrderBy(p => p.Name).ThenBy(p => p.Name.Length);
        }
    }
}
