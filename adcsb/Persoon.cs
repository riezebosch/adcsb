using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adcsb
{
    partial class Persoon
    {

        public int Leeftijd { get; set; }

        partial void CustomInitialize(int p)
        {
            Leeftijd = p;
        }
    }
}
