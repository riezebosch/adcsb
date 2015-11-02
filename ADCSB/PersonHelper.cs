using System;

namespace ADCSB
{
    internal static class PersonHelper
    {
        public static void Print(this Person person)
        {
            Console.WriteLine($"Name: {person.Name}, Age: {person.Age}");
        }
    }
}