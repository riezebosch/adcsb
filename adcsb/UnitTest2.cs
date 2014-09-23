using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Linq;

namespace adcsb
{
    [TestClass]
    public class UnitTest2
    {
        class MyContext : DbContext
        {
            public DbSet<Persoon> Personen { get; set; }
        }

        [TestMethod]
        public void TestMethod1()
        {
            IetsMetLambdas(p => p.Naam == "Manuel");
            IetsMetExpressions(p => p.Naam == "Manuel");
        }

        private static void IetsMetLambdas(Func<Persoon, bool> action)
        {
            bool result = action(new Persoon { Naam = "Manuel" });
            Console.WriteLine(result);
        }

        private static void IetsMetExpressions(Expression<Func<Persoon, bool>> expression)
        {
            Console.WriteLine(expression);
            
            bool result = expression.Compile()(new Persoon { Naam = "Manuel" });
            Console.WriteLine(result);
        }

        [TestMethod]
        [Ignore]
        public void EntityFrameworkMetExpressions()
        {
            using (var context = new MyContext())
            {
                foreach (var persoon in context.Personen.Where(p => p.Naam == "Manuel"))
                {
                    Console.WriteLine(persoon);
                }
            }
        }
    }
}
