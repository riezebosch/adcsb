using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Dynamic;
using System.Collections.Generic;
using Shouldly;
using Microsoft.CSharp.RuntimeBinder;

namespace ADCSB
{
    [TestClass]
    public class UnitTest6
    {
        class MyBag : DynamicObject
        {
            IDictionary<string, object> dict = new Dictionary<string, object>();
            public override bool TrySetMember(SetMemberBinder binder, object value)
            {
                dict[binder.Name] = value;
                return true;
            }

            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                return dict.TryGetValue(binder.Name, out result);
            }
        }

        [TestMethod]
        public void DynamicDemo()
        {
            dynamic d = new MyBag();
            d.Leeftijd = 21;

            Console.WriteLine(d.Leeftijd);
        }

        class C
        {
            public string Foo(decimal x) => "decimal";
        }

        class D : C 
        {
            public string Foo(int x) => "integer";
        }

        class E : C
        {
            public string Foo(string x) => "string";
        }

        [TestMethod]
        public void DynamicMetOverloading()
        {
            C c = new D();
            dynamic d = 10;

            string result = c.Foo(d);
            result.ShouldBe("decimal");
        }

        [TestMethod]
        public void DynamicMetOverloading2()
        {
            C c = new E();
            dynamic d = "tien";

            // Ondanks dat hij runtime weet dat het type
            // wel degelijk een matchende method bevat
            // gooit dit nog steeds een runtime binder exception
            // wat hetzelfde gedrag simuleert als de CLR.
            Should.Throw<RuntimeBinderException>(() => c.Foo(d));
        }
    }
}
