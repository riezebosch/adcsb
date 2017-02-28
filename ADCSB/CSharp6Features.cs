using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ADCSB
{
    public class CSharp6Features
    {
        [Fact]
        public void StringInterpolation()
        {
            var input = 2.00m;
            string expected = "asdf: 2.00";

            string oud = string.Format("asdf: {0,2}", input);
            Assert.Equal(expected, oud);

            string nieuw = $"asdf: {input,2}";
            Assert.Equal(expected, nieuw);
        }
    }
}
