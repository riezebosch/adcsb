using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using adcsb_asdfasdfsadf;
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
            IEnumerable<int> items = new int[]{ 1, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            items = items.Where(IsEven);

            //foreach (var item in items)
            //{
            //    Console.WriteLine(item);
            //}

            items = items.Where(GreaterThanFive);
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }

        delegate bool Predicate(int item);
        delegate bool Predicate<T>(T item);

        private bool IsEven(int item)
        {
            return item % 2 == 0;
        }

        private bool GreaterThanFive(int item)
        {
            return item > 5;
        }

        [TestMethod]
        public void WhatAreAnonymousMethods()
        {
            int[] items = { 0, 1, 1, 2, 3, 5, 8, 13 };
            
            // Aanroep mbv methode in delegate
            items.Where(IsEven);

            // Anonymous Methods
            items.Where(delegate(int i) { return i % 2 == 0; });

            // Lamdba's, allemaal equivalent
            items.Where((int i) => { return i % 2 == 0; });
            items.Where(i => { return i % 2 == 0; });
            items.Where(i => i % 2 == 0);
        }

        [TestMethod]
        public void WhatAreCapturedOuterVariables()
        {
            string input = "test";
            input += DateTime.Today;

            ExecuteActionWithOuterVariable(() => Console.WriteLine(input));
        }

        private static void ExecuteActionWithOuterVariable(Action action)
        {
            action();
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
