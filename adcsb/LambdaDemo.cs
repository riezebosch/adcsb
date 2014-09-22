using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adcsb
{
    public delegate void MyDelegate(string input);

    [TestClass]
    public class LambdaDemo
    {
        [TestMethod]
        public void MyTestMethod()
        {
            MyDelegate m1 = new MyDelegate(Print);
            MyDelegate m2 = Print;
            
            // Dit is precies hetzelfde.
            m1("hallo");
            m1.Invoke("hallo");

            m1 += Console.WriteLine;

            m1("poging 2");

            ExecuteMyDelegate(m1);
        }

        public event MyDelegate Execute;

        private static void Print(string input)
        {
            Console.WriteLine(input + " vanuit mijn print methode");
        }

        private static void ExecuteMyDelegate(MyDelegate m)
        {
            m("input vanuit een methode die een delegate als parameter heeft binnen gekregen.");
        }

        [TestMethod]
        public void TestFilteringAListWithDelegates()
        {
            int[] items = { 1, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            items = Where(items, IsEven);

            //foreach (var item in items)
            //{
            //    Console.WriteLine(item);
            //}

            items = Where(items, GreaterThanFive);
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }

        delegate bool Predicate(int item);

        private bool IsEven(int item)
        {
            return item % 2 == 0;
        }

        private bool GreaterThanFive(int item)
        {
            return item > 5;
        }

        private int[] Where(int[] items, Predicate p)
        {
            var result = new List<int>();

            foreach (var item in items)
            {
                if (p(item))
                {
                    result.Add(item);
                }
            }

            return result.ToArray();
        }
    }

    [TestClass]
    public class EventSubscriptionDemo
    {
        [TestMethod]
        public void MyTestMethod()
        {
            LambdaDemo d = new LambdaDemo();
            d.Execute += Print;
            d.Execute -= Print;

        }

        private void Print(string input)
        {

        }
    }

}
