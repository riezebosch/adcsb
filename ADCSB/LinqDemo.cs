using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace ADCSB
{
    public class LinqDemo
    {
        IEnumerable<Persoon> personen = new List<Persoon>
        {
            new Persoon { Id = 1, Naam = "Pietje", Geboortedatum = new DateTime(1990, 2,14) },
            new Persoon { Id = 2, Naam = "Ruben", Geboortedatum = new DateTime(1982, 10, 9) }
        };
        private readonly ITestOutputHelper output;

        public LinqDemo(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void QuerySyntax_ExpressionSyntax_ComprehensionSyntax()
        {
            var query = personen
                .Where(p => p.Geboortedatum.Year >= 1900)
                .OrderBy(p => p.Naam, StringComparer.OrdinalIgnoreCase)
                .Select(p => p.Naam);

            var query01 = personen;
            var query02 = query01.Where(p => p.Geboortedatum.Year >= 1900);
            var query03 = query02.Select(p => p.Naam);

            var query11 = personen;
            var query12 = Enumerable.Where(query11, p => p.Geboortedatum.Year >= 1900);
            var query13 = Enumerable.Select(query12, p => p.Naam);

            var query21 = from p in personen
                          where p.Geboortedatum.Year >= 1900
                          orderby p.Naam descending, StringComparer.OrdinalIgnoreCase
                          select p.Naam;
        }

        [Fact]
        public void ComparerOndersteuningInQuerySyntax()
        {
            string[] input = { "aa", "b", "Ab" };
            string[] expected = { "Ab", "aa", "b" };

            var result1 = input.OrderBy(a => a, StringComparer.Ordinal);
            var result2 = from a in input
                          orderby a descending, StringComparer.Ordinal
                          select a;

            Assert.Equal(expected, result1);

            // De StringComparer wordt uitgevoerd in een ThenBy 
            // en niet als parameter van de originele OrderBy"
            Assert.NotEqual(expected, result2);
        }

        [Fact]
        public void BewijsVanLazy()
        {
            int year = 0;
            var query = personen
                .Where(p => p.Geboortedatum.Year >= year)
                .Select(p => p.Naam);

            year = 2000;
            Assert.Equal(0, query.Count());
        }

        [Fact]
        public void SomsWilJeNietLazy()
        {
            int year = 0;
            int counter = 0;

            var query = personen
                .Where(p => p.Geboortedatum.Year >= year)
                .Select(p => { counter++; return p.Naam; })
                .ToList();

            Assert.Equal(2, query.Count());
            Assert.Equal(2, query.Count());
            Assert.True(query.Any());

            Assert.Equal(2, counter);

            var query2 = (from p in personen
                         where p.Geboortedatum.Year >= year
                         let c = counter++
                         select p.Naam).ToList();

            query2 = query2.ToList();
        }

        [Fact]
        public void JoinMetProjectionOpAnonymousType()
        {
            var autos = new List<Auto>
            {
                new Auto{ Eigenaar = 1, Merk = "Opel" },
                new Auto{ Eigenaar = 1, Merk = "Renault" }
            };

            var query = from p in personen
                        join a in autos on p.Id equals a.Eigenaar into temp
                        from a in temp.DefaultIfEmpty()
                        group a?.Merk by p.Naam into g
                        select new { Naam = g.Key, Autos = g };

            personen
                .Join(autos, 
                    p => p.Id, 
                    a => a.Eigenaar, 
                    (p, a) => new { p.Naam, a.Merk })
                .GroupBy(g => g.Naam)
                .Select(g => new { Naam = g.Key, Autos = g });

            var result = query.First();
            Assert.Equal("Pietje", result.Naam);
            Assert.Equal(new[] { "Opel", "Renault" }, result.Autos);

            Assert.Equal(2, query.Count());
            var tweede = query.Skip(1).First();
            Assert.Equal("Ruben", tweede.Naam);

            // LELIJK!
            Assert.Equal(new string[] { null }, tweede.Autos);

        }

        [Fact]
        public void LinqExceptions()
        {
            var items = System.Linq.Enumerable.Range(0, 5000);
            int result = items.FirstOrDefault();
            
            Assert.Equal(0, result);

            for (int i = 0; items.Any(); i++)
            {
                output.WriteLine(string.Join(", ", items.Take(20)));
                items = items.Skip(20);
            }
        }

        [Fact]
        public void CastDemo()
        {
            var ouderwets = new System.Collections.ArrayList(new object[] { 1, "twee", 2, 3, 4 });
            ouderwets.OfType<int>().Where(q => q % 2 == 0);

            int i = 3;
            double j = i;

            Assert.Throws<InvalidCastException>(() => (double)(object)i);

            var integers = new List<int> { 1, 2, 3 };
            var doubles = integers.Cast<double>();

            Assert.Throws<InvalidCastException>(() => doubles.Any());

            var students = new List<Student> { new Student { } };
            var people = students.OfType<Persoon>();

            Assert.NotEmpty(people);
        }

        private class Persoon
        {
            public DateTime Geboortedatum { get; internal set; }
            public int Id { get; internal set; }
            public string Naam { get; internal set; }
        }

        private class Auto
        {
            public int Eigenaar { get; internal set; }
            public string Merk { get; internal set; }
        }

        private class Student : Persoon
        {
        }
    }
}
