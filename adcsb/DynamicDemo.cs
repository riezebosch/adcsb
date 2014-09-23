using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Dynamic;
using System.Collections.Generic;

namespace adcsb
{
    [TestClass]
    public class DynamicDemo
    {
        class Bag : DynamicObject
        {
            public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
            {
                result = 0;
                return true;
            }

            private Dictionary<string, object> _data = new Dictionary<string, object>();

            public override bool TrySetMember(SetMemberBinder binder, object value)
            {
                _data[binder.Name] = value;
                return true;
            }

            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                return _data.TryGetValue(binder.Name, out result);
            }
        }

        [TestMethod]
        public void TestMethod1()
        {
            dynamic b = new Bag();
            b.Execute();
            b.Leeftijd = 32;
   
            Console.WriteLine(b.Leeftijd);
        }
    }
}
