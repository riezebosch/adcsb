using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ADCSB
{
    public class CallerInfoDemo
    {
        [Fact]
        public void Ik()
        {
            var result = WieRoeptMijAan();
            Assert.Equal("Ik", result);
        }

        private string WieRoeptMijAan([CallerMemberName] string name = null, [CallerLineNumber] int line = 0)
        {
            return name;
        }
    }
}
