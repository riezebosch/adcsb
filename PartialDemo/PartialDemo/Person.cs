using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartialDemo
{
    partial class Person
    {
        int _age;
        public int Age { get { return _age; } set { _age = value; } }

        public int ReadonlyAge { get {  return _age; } }

        public int ReadonlyAutoImplementedProperty { get; private set; }

        public Person()
        {
            _age = 21;
            ReadonlyAutoImplementedProperty = 21;
        }

        public void Print()
        {
            Console.WriteLine(Age);
        }
    }

   
}
