using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using System.Linq;

namespace ADCSB
{
    [TestClass]
    public class EntityFrameworkDemo
    {
        class DemoContext : DbContext
        {
            public DbSet<Person> People { get; set; }
        }

        [TestMethod]
        public void TestMethod1()
        {
            using (var context = new DemoContext())
            {
                context.Database.Log = Console.WriteLine;
                var query = from p in context.People
                            join q in context.People on p.Id equals q.Id
                            where p.FirstName == "Piet"
                            select p;

                query.ToList();
            }
        }
    }

    class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Name { get; set; }
    }
}
