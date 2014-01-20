using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatlesDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> beatles = new List<Person>
            { 
                new Person
                {
                    Name="Paul", 
                    Address="Liverpool", 
                    Instruments =new List<string>{"Bass", "Guitar", "Vocals"}
                }, 
                new Person
                {
                    Name="John", 
                    Address="New York", 
                    Instruments =new List<string>{"Guitar", "Piano", "Vocals"}
                }, 
                new Person
                {
                    Name="George", 
                    Address="Liverpool", 
                    Instruments =new List<string>{"Guitar", "Vocals"}
                }, 
                new Person
                {
                    Name="Ringo", 
                    Address="Los Angeles", 
                    Instruments = new List<string>{"Drums", "Vocals" }
                }
            };
            var singersAndGuitarPlayers =
                (from person in beatles
                from instrument in person.Instruments
                where instrument == "Guitar" || instrument == "Vocals"
                select person.Name).Distinct();

            foreach (var item in singersAndGuitarPlayers)
            {
                Console.WriteLine("{0}", item);
            }

            int[] getallen = { 1, 2, 3, 4, 5 };
            char[] characters = { 'a','b','c','d','e'};

            var query = from i in getallen
                        from c in characters
                        select new { Getal = i, Character = c };

            foreach (var item in query)
            {
                Console.WriteLine("  {0} {1}", item.Getal, item.Character);
            }
        }
    }
}
