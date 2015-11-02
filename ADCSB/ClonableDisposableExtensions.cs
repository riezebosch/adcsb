using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADCSB
{
    static class ClonableDisposableExtensions
    {
        public static void Iets<T>(this T item)
            where T : ICloneable, IDisposable
        {
            item.Clone();
            item.Dispose();
        }
    }
}
