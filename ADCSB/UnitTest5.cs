using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.Collections.Generic;

namespace ADCSB
{
    [TestClass]
    public class UnitTest5
    {
        interface IMyClass<out T>
        {
        }

        class MyClass<T> : IMyClass<T>
        {
        }

        [TestMethod]
        public void TestMethod1()
        {
            var m1 = new MyClass<int>();
            var m2 = new MyClass<Person>();

            m1.GetType().ShouldNotBe(m2.GetType());
        }

      


        [TestMethod]
        public void MyTestMethod()
        {
            var m1 = new MyClass<Persoon>();
            var m2 = new MyClass<Student>();

            m1.GetType().ShouldNotBe(m2.GetType());

            DoeIetsMetMyClass(m1);
            // Dit mag dus niet
            //DoeIetsMetMyClass(m2);

            // Maar op een interface met contravariance mag het opeens wel!
            DoeIetsMetIMyClass(m2);

            var students = new List<Student>();
            // List<Persoon> people = students

            // Hierom mag dat dus niet:
            //people.Add(new Persoon());

            // Maar bij een IEnumerable kan je niet add'en,
            // en daarom is T contravariant (= <out T>)
            IEnumerable<Persoon> people = students;
        }

        private void DoeIetsMetMyClass(MyClass<Persoon> m1)
        {
            
        }

        private void DoeIetsMetIMyClass(IMyClass<Persoon> m1)
        {

        }

        private class Student : Persoon
        {
        }
    }
}
