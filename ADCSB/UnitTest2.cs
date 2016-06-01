using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ADCSB
{
    delegate bool MyFirstDelegate(int input);

    delegate bool MyFirstDelegate2(int input1, int input2);

    delegate bool MyFirstDelegate3(int input1, int input2, int input3);

    // IPV hiervan kunnen we generics gebruiken,
    // stiekem is dit precies wat Func<..> doet!
    delegate TResult MyFirstDelegate<T, TResult>(T input);
    delegate TResult MyFirstDelegate<T1, T2, TResult>(T1 input1, T2 input2);

    [TestClass]
    public class LambdaDemos
    {
        [TestMethod]
        public void TestMethod1()
        {
            MyFirstDelegate f1 = new MyFirstDelegate(VoorbeeldFunctie);
            bool result1 = f1.Invoke(12);

            // Hier staat hetzelfde
            MyFirstDelegate f2 = VoorbeeldFunctie;
            var result2 = f2(12);

            MyFirstDelegate<int, bool> f3 = VoorbeeldFunctie;

            // Hier gebruiken we de standaard in het framework aanwezige
            // generic delegate
            Func<int, bool> f4 = VoorbeeldFunctie;

            DezeMethodeDoetIetsMetMijnDelegates(f1);
            DezeMethodeDoetIetsMetMijnDelegates(f2);
            DezeMethodeDoetIetsMetMijnDelegates(VoorbeeldFunctie);
            //DezeMethodeDoetIetsMetMijnDelegates(f3);
        }

        private void DezeMethodeDoetIetsMetMijnDelegates(MyFirstDelegate f)
        {
            f(12);
        }

        private bool VoorbeeldFunctie(int input)
        {
            return true;
        }

        [TestMethod]
        public void AnymousMethodsDemo()
        {
            DezeMethodeDoetIetsMetMijnDelegates(new MyFirstDelegate(VoorbeeldFunctie));
            DezeMethodeDoetIetsMetMijnDelegates(VoorbeeldFunctie);

            // Ouderwetse anonymous method
            DezeMethodeDoetIetsMetMijnDelegates(delegate (int item) { return true; });

            // Dat moet korter kunnen
            DezeMethodeDoetIetsMetMijnDelegates((int item) => { return true; });

            // Maar nog korter
            DezeMethodeDoetIetsMetMijnDelegates(item => { return true; });

            // En als je body toch maar 1 statement is:
            DezeMethodeDoetIetsMetMijnDelegates(item => true);
        }

        [TestMethod]
        public void CapturedOuterVariablesDemo()
        {
            int multiplier = 13;
            DezeMethodeDoetIetsMetMijnDelegates(item =>
            {
                // Hoe kan het dat deze lambda toegang heeft
                // tot de lokaal gedefinieerde variables van
                // de enclosing method?!?!
                Console.WriteLine(multiplier * item);
                return true;
            });


            // Nou, zo:
            var wrapper = new CapturedOuterVariablesWrapper();
            wrapper.multiplier = 13;
            DezeMethodeDoetIetsMetMijnDelegates(wrapper.VoorbeeldFunctie2);
        }

        class CapturedOuterVariablesWrapper
        {
            public int multiplier;
            public bool VoorbeeldFunctie2(int item)
            {
                Console.WriteLine(multiplier * item);
                return true;
            }
        }

        [TestMethod]
        public void CapturedOuterVariablesTrap()
        {
            var actions = new List<Action>();
            for (var i = 0; i < 10; i++)
            {
                var j = i;
                actions.Add(() => Console.Write("{0} ", j));
            }

            foreach (var action in actions)
            {
                action();
            }
        }

        [TestMethod]
        public void CapturedOuterVariablesTrapForEach()
        {
            var actions = new List<Action>();
            foreach (var i in new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 })
            {
                // Dit is opgelost sinds VS2012 ongeveer: https://blogs.msdn.microsoft.com/ericlippert/2009/11/12/closing-over-the-loop-variable-considered-harmful/
                actions.Add(() => Console.Write("{0} ", i));
            }

            foreach (var action in actions)
            {
                action();
            }
        }
    }
}
