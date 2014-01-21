using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkDemo
{
    class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
    }

    class MyContext : DbContext
    {
        public DbSet<Person> People { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new MyContext())
            {
                //var query = from p in context.People
                //            where p.Name.StartsWith("m")
                //            select p.Name;

                var query = ((IEnumerable<Person>)context.People)
                    .Where(p => p.Name.StartsWith("M"))
                    .Select(p => p.Name);

                foreach (var p in query)
                {
                    Console.WriteLine(p);
                }
            }
        }
    }
}
