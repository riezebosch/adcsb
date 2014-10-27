using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ADCSB
{
    [TestClass]
    public class AutoImplementedProperties
    {
        // Auto-implemented property
        public int MyProperty { get; private set; }


        private int _myProperty2;
        public int MyProperty2
        {
            get { return _myProperty2; }
           // set { _myProperty2 = value; }
        }

        private int _myProperty3;
        public void setProperty3(int value)
        {
            _myProperty3 = value;
        }

        public int getProperty3()
        {
            return _myProperty3;
        }

        [TestMethod]
        public void TestMethod1()
        {
            foreach (var method in this.GetType().GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public))
            {
                Console.WriteLine(method.Name);
            }
            
        }

        struct MyStruct
        {
            public int MyProperty { get; set; }

            public MyStruct(int value) : this()
            {
                MyProperty = value;
            }
        }

        [TestMethod]
        public void AutoImplementedPropertiesMetStructs()
        {
            var s = new MyStruct
            {
                MyProperty = 23
            };

            Console.WriteLine(s.MyProperty);


            MyStruct s2 = new MyStruct
            {
                MyProperty = 23
            };

            Assert.AreEqual(s, s2);
        }
    }
}
