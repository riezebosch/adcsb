using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ADCSB
{
    public class ExtensionMethodsDemo
    {
        [Fact]
        public void WaaropKanIkExtensionMethodsPlakken()
        {
            int i = 13;
            string s = i.ToReadableFormat();
                       //IntHelperMethods.ToReadableFormat(i);

            Assert.Equal("thirteen", s);

            int i2 = 13.Square();
                     //IntHelperMethods.Square(13);

            Assert.Equal(169, i2);

            bool even = 13.IsEven();
                        //IntHelperMethods.IsEven(13);
            Assert.False(even);
        }
    }
    static class IntHelperMethods
    {
        public static bool IsEven(this int v)
        {
            return v % 2 == 0;
        }

        public static string ToReadableFormat(this int i)
        {
            switch (i)
            {
                case 13:
                    return "thirteen";
                default:
                    return "I don't know";
            }
        }

        public static int Square(this int n)
        {
            return n * n;
        }
    }
}
