using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ADCSB
{
    public class DynamicDemo
    {
       

        [Fact]
        public void IetMetDynamics()
        {
            int result = TelOp(2, 4);
            Assert.Equal(6, result);
        }

        [Fact]
        public void ZoekHetMaarLekkerRuntimeUit()
        {
            int result = TelOp("twee", "vier");
            Assert.Equal(0, result);
        }

        private int TelOp(dynamic v1, dynamic v2)
        {
            if (v1 is int && v2 is int)
            {
                return v1 + v2;
            }

            return 0;
        }

        [Fact]
        public void PropertiesOpDeBag()
        {
            dynamic bag = new Bag();
            bag.Leeftijd = 68;

            Assert.Equal(68, bag.Leeftijd);
        }

        class Bag : DynamicObject
        {
            IDictionary<string, object> items = new Dictionary<string, object>();

            public override bool TrySetMember(SetMemberBinder binder, object value)
            {
                items[binder.Name] = value;
                return true;
            }

            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                return items.TryGetValue(binder.Name, out result);
            }
        }
    }
}
