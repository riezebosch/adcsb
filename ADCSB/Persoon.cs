﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADCSB
{
    partial class Persoon
    {
        public Persoon()
        {
            OnGeboorteDatumChangedMetEenEvent += (sender, args) => Console.WriteLine(args);

            
        }

        public int BerekenLeeftijd()
        {
            return 34;
        }

        partial void OnGeboorteDatumChangedMetPartialMethod(DateTime dt)
        {
            Console.WriteLine($"Hey, deze partial heeft ook een body. De waarde die was meegegeven is: {dt}");
        }


        public IList<Aankoop> Aankopen { get; } = new List<Aankoop>();
        
    }
}