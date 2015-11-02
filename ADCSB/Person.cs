using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADCSB
{
    partial class Person
    {
        public Person(int age)
        {
            Age = age;
        }

        public Address Address { get; internal set; }
        public int Age { get; set; }
        public string Name { get; internal set; }

        public void Print()
        {

        }

        partial void SomethingYouCanImplementInTheOtherPartial(int sum)
        {
            Console.WriteLine($"Nu ik de partial method een implementatie heb gegeven wordt hij ook daadwerkelijk uitgevoerd. Het bewijs, sum = {sum}.");
        }
    }
}
