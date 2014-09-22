using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace adcsb
{
    static class StringHelper
    {
        public static string RemoveVowels(this string input)
        {
            var sb = new StringBuilder();
            foreach (var c in input)
            {
                if (!c.IsVowel())
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        private static bool IsVowel(this char c)
        {
            return c == 'a' ||
                c == 'e' ||
                c == 'i' ||
                c == 'o' ||
                c == 'u' ||
                c == 'y';
        }

        public static T DeepClone<T>(this T item)
            where T: ICloneable
        {
            using (var s = new MemoryStream())
            {
                var dc = new DataContractSerializer(typeof(T));
                dc.WriteObject(s, item);

                s.Seek(0, 0);
                return (T)dc.ReadObject(s);
            }
        }
    }
}
