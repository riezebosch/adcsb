using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adcsb_asdfasdfsadf
{
    internal static class LinqExtensions
    {
        public static IEnumerable<T> Where<T>(this IEnumerable<T> items, Func<T, bool> p)
        {
            var result = new List<T>();

            foreach (var item in items)
            {
                if (p(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }
    }
}
