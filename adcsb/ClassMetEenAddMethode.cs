using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adcsb
{
    class ClassMetEenAddMethode : IEnumerable
    {
        public void Add(int item)
        {
            Console.WriteLine(item);
        }

        public IEnumerator GetEnumerator()
        {
            return null;
        }
    }
}
