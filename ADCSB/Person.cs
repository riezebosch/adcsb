using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADCSB
{
    partial class Person
    {
        public int Age { get; set; }

        public void Print()
        {

        }

        partial void SomethingYouCanImplementInTheOtherPartial(int sum)
        {
            Console.WriteLine($"Nu ik de partial method een implementatie heb gegeven wordt hij ook daadwerkelijk uitgevoerd. Het bewijs, sum = {sum}.");
        }
    }
}
