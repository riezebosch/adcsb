using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace LinqOefening
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void LinqOefening()
        {
            var plaatsnamen = new List<string>
            { 
                "Amsterdam", "Arnhem", "Amersfoort",
                "Assen", "Amstelveen", "Alphen"
            };

            var query = from p in plaatsnamen
                        where p.Length < 8
                        orderby p.Length, p
                        select p;

            foreach (var item in query)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();
            var query2 = plaatsnamen
                .Where(p => p.Length < 8)
                .OrderBy(p => p.Length)
                .ThenBy(p => p);

            foreach (var item in query2)
            {
                Console.WriteLine(item);
            }


            /* Opdracht 1;
             * Schrijf één LINQ-query (gebruikmakend van comprehension syntax / query syntax) 
             * die alle korte plaatsnamen (minder dan 8 letters), in volgorde van lengte, en 
             * bij gelijke lengte alfabetisch, oplevert.

             * Opdracht 2. 
             * Schrijf één LINQ-query (gebruikmakend van extension methods / fluent syntax) 
             * die de som bepaalt van de lengtes van alle plaatsnamen die eindigen 
             * op een ‘m’. 
             * (Met één LINQ-query wordt hier een aaneengesloten reeks van extension methods / query operators bedoeld.)

            
             * Opdracht 3. 
             * Bepaal met behulp van LINQ de meest voorkomende eindletter 
             * van de plaatsnamen. Het antwoord is een lijstje dat bestaat uit één element 
             * als er precies één eindletter het vaakst voorkomt, en bestaat uit meerdere 
             * letters als er meerdere eindletters een eerste plaats delen. 
             * Je oplossing mag bestaan uit meerdere queries en/of LINQ-expressies.
             */
        }
    }
}
