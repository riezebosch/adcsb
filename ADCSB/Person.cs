using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADCSB
{
    partial class Person
    {
        public Person()
        {
        }

        public Person(int age)
        {
            Age = age;
        }

        public Address Address { get; internal set; }
        public int Age { set; get; }
        public string Name { get; internal set; }


        private Gender gender;

        public Gender Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        public int ReadOnlyProperty { get; } = 12;
        
        //public int WriteOnlyProperty { set; }

        partial void SomethingYouCanImplementInTheOtherPartial(int sum)
        {
            Console.WriteLine($"Nu ik de partial method een implementatie heb gegeven wordt hij ook daadwerkelijk uitgevoerd. Het bewijs, sum = {sum}.");
        }
    }

    enum Gender
    {
        Unknown,
        Male,
        Female
    }
}
