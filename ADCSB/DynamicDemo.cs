using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Dynamic;
using System.Collections.Generic;
using Microsoft.CSharp.RuntimeBinder;

namespace ADCSB
{
    [TestClass]
    public class DynamicDemo
    {
        class MyDynamicObject : DynamicObject
        {
            public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
            {
                result = 45;
                return true;
                //return base.TryInvokeMember(binder, args, out result);
            }
        }

        

        [TestMethod]
        public void TestMethod1()
        {
            dynamic input = "Pietje Puk";
            Console.WriteLine(input.Length);

            //Console.WriteLine(input.HaalAllMedeklinkersVanDezeStringAf());

        }

        [TestMethod]
        public void DemoMetDynamicObject()
        {
            dynamic d = new MyDynamicObject();
            int result = d.Reken(14);

            Console.WriteLine(result);
        }


        class JavaScriptAchtigeClass : DynamicObject
        {
            Dictionary<string, object> _items = new Dictionary<string, object>();

            public override bool TrySetMember(SetMemberBinder binder, object value)
            {
                _items[binder.Name] = value;
                return true;
            }

            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                return _items.TryGetValue(binder.Name, out result);
            }
        }

        [TestMethod]
        public void GebruikOnzeJavaScriptClass()
        {
            dynamic c = new JavaScriptAchtigeClass();
            c.Leeftijd = 23;

            Assert.AreEqual(23, c.Leeftijd);
            string getal = c.Leeftijd.ToString();

            try
            {
                int dummy = c.BestaatNogNiet;
                Assert.Fail();
            }
            catch (RuntimeBinderException)
            {
            }
        }

        [TestMethod]
        public void TestIetsMetEenExplicitImplemtedInterface()
        {
            var d = new MyDisposable();
            ((IDisposable)d).Dispose();

            ((IDisposable)new Person()).Dispose();
        }

        class MyDisposable : IDisposable
        {

            void IDisposable.Dispose()
            {
            }
        }
    }
}
