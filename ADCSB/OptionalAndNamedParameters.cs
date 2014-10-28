using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ADCSB
{
    [TestClass]
    public class OptionalAndNamedParameters
    {
        [TestMethod]
        public void TestMethod1()
        {
            DoeIets(3, 5);
            DoeIets(a: 3, b: 5);
            DoeIets(b: 3, a: 5);
        }

        private static void DoeIets(int a, double b)
        {
            Console.WriteLine("a: {0}, b: {1}", a, b);
        }

        [TestMethod]
        public void OptionalParametersDemo()
        {
            DoeIetsMetOptionals();
            DoeIetsMetOptionals(3);
            DoeIetsMetOptionals(b: 3);
        }

        private static void DoeIetsMetOptionals(int a = 5, double b = 6)
        {
            Console.WriteLine("a: {0}, b: {1}", a, b);

        }

        
    }
}
