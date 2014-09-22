using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace adcsb
{
    class Student : Persoon
    {
        public int StudentId { get; set; }
    }

    interface IBuilder<out T>
    {
        T Build();
    }

    interface IPrint<in T>
    {
        void Print(T input);
    }

    class PersoonBuilder : IBuilder<Persoon>, IPrint<Persoon>
    {
        public Persoon Build()
        {
            return new Persoon
            {
                Naam = "Manuel Riezebosch",
                Leeftijd = 32
            };
        }

        public void Print(Persoon input)
        {
            Console.WriteLine("{0}: {1}", input.Naam, input.Leeftijd);
        }
    }

    class StudentBuilder : IBuilder<Student>, IPrint<Student>
    {
        public Student Build()
        {
            return new Student
            {
                Naam = "Manuel Riezebosch",
                Leeftijd = 32,
                StudentId = 202060
            };
        }

        public void Print(Student input)
        {
            Console.WriteLine("{0}: {1} ({2})", input.Naam, input.Leeftijd, input.StudentId);
        }
    }

    [TestClass]
    public class ContraAndCoVariance
    {
        [TestMethod]
        public void TestMethod1()
        {
            IBuilder<Persoon> builder = new StudentBuilder();
            Persoon p = builder.Build();

            IPrint<Student> printer = new PersoonBuilder();
            printer.Print((Student)p);


            var items = new List<Student>
            {
                new Student { Naam = "Manuel Riezebosch", Leeftijd = 32, StudentId = 202060 } 
            };

            Print(items);
        }

        private static void Print(IEnumerable<Persoon> personen)
        {
            foreach (var persoon in personen)
            {
                Console.WriteLine("{0}: {1}", persoon.Naam, persoon.Leeftijd);
            }
        }

        [TestMethod]
        public void ContraAndCovarianceWithDelegates()
        {
            Func<Persoon, Student> ConvertPersoonToStudent = 
                p => new Student { Leeftijd = p.Leeftijd, Naam = p.Naam, StudentId = -1 };
            Persoon s = ConvertPersoonToStudent(new Student { Naam = "Manuel Riezebosch", Leeftijd = 32 });
        }

    }
}
