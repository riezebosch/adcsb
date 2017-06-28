using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ADCSB
{
    public class LinqOefening
    {
        IEnumerable<string> plaatsnamen = new List<string>
        {
            "Amsterdam", "Arnhem", "Amersfoort",
            "Assen", "Amstelveen", "Alphen"
        };

        /// <summary>
        ///  Opdracht 1;
        ///   Schrijf één LINQ-query(gebruikmakend van comprehension syntax / query syntax)
        ///   die alle korte plaatsnamen(minder dan 8 letters), in volgorde van lengte, en
        ///  bij gelijke lengte alfabetisch, oplevert.
        /// </summary>
        [Fact]
        public void Opdracht1()
        {
            var query = plaatsnamen;

            Assert.Equal(new[] { "Assen", "Alphen", "Arnhem" }, query);
        }

        /// <summary>
        /// Schrijf één LINQ-query(gebruikmakend van extension methods / fluent syntax)
        /// die de som bepaalt van de lengtes van alle plaatsnamen die eindigen 
        /// op een ‘m’. 
        /// (Met één LINQ-query wordt hier een aaneengesloten reeks van extension methods / query operators bedoeld.)
        /// </summary>
        [Fact]
        public void Opdracht2()
        {
            var query = plaatsnamen;
            Assert.Equal(15, query);
        }

        /// <summary>
        /// Bepaal met behulp van LINQ de meest voorkomende eindletter
        /// van de plaatsnamen.Het antwoord is een lijstje dat bestaat uit één element
        /// als er precies één eindletter het vaakst voorkomt, en bestaat uit meerdere
        /// letters als er meerdere eindletters een eerste plaats delen.
        /// Je oplossing mag bestaan uit meerdere queries en / of LINQ - expressies.
        /// </summary>
        [Fact]
        public void Opdracht3()
        {
            var query = from p in plaatsnamen
                        select p.Last();


            var expected = new[] { 'n' };
            Assert.Equal(expected, query);
        }
    }
}
