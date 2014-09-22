using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adcsb
{
    partial class Persoon
    {
        public Persoon()
        {
            CustomInitialize(12);
        }

        partial void CustomInitialize(int p);



        public string Naam { get; set; }
    }
}
