using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicDemo
{
    class MyBag : DynamicObject
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

    interface IMyInterface
    {
        void Foo();
    }

    class ImplicitImpl : IMyInterface
    {
        public void Foo()
        {
        }
    }

    class ExplicitImpl : IMyInterface
    {

        public void IMyInterface.Foo()
        {
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var a = new ImplicitImpl();
            a.Foo();

            var b = new ExplicitImpl();
            b.Foo();

            dynamic bag = new MyBag();
            bag.MyPropertyASDASDASD = 4;

            string temp = bag.MyPropertyASDASDASD;

            Console.WriteLine(bag.MyPropertyASDASDASD);
        }
    }
}

