using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace ADCSB
{
    public class GenericsDemo
    {
        private readonly ITestOutputHelper output;

        public GenericsDemo(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ArraysZijnVervelend()
        {
            int[] items = { 1, 2, 3 };
            Assert.Throws<IndexOutOfRangeException>(() => items[3] = 4);
        }

        [Fact]
        public void AbstractieOverArrayDieAutomatischMeegroeit()
        {
            int[] items = { 1, 2, 3 };
            var mylist = new MyList<int>(items);
            mylist.Add(4);

            Assert.Equal(4, mylist[3]);
        }

        [Fact]
        public void BuitenDeRangeVanToegevoegdeItemsOpzoekenGooitEenIndexOutOfRangeException()
        {
            var mylist = new MyList<int>();
            Assert.Throws<IndexOutOfRangeException>(() => mylist[3]);
        }

        [Fact]
        public void MyListImplementeertIEnumerable()
        {
            var mylist = new MyList<int> { 1 };

            int aantalkeeraangeroepen = 0;
            foreach (var item in mylist)
            {
                output.WriteLine(item.ToString());
                aantalkeeraangeroepen++;
            }

            Assert.Equal(1, aantalkeeraangeroepen);
        }

        [Fact]
        public void MyListImplementeertIEnumerableMaarGaatNietBuidenZijnEigenBound()
        {
            var mylist = new MyList<int>();
            foreach (var item in mylist)
            {
                throw new InvalidOperationException("dit zou niet mogen worden uitgevoerd.");
            }
        }

        [Fact]
        public void MyListVoorWillekeurigeAndereTypes()
        {
            var mylist = new MyList<string>();
            mylist.Add("eerste");

            string result = mylist[0];
        }

        private class MyList<T> : IEnumerable<T>
        {
            private T[] items;
            private int count;

            public MyList()
            {
                this.items = new T[10];
            }

            public MyList(T[] items)
            {
                this.items = items;
                count = items.Length;
            }

            internal void Add(T item)
            {
                EnsureCapacity();
                items[count++] = item;
            }

            private void EnsureCapacity()
            {
                if (items.Length == count)
                {
                    var temp = new T[count * 2];
                    Array.Copy(items, temp, count);

                    items = temp;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
               return GetEnumerator(); 
            }

            public IEnumerator<T> GetEnumerator()
            {
                for (int i = 0; i < count; i++)
                {
                    yield return items[i];
                }
            }

            public T this[int index]
            {
                get
                {
                    if (index >= count)
                    {
                        throw new IndexOutOfRangeException();
                    }
                    return items[index];
                }
                set { items[index] = value; }
            }
        }

        [Fact]
        public void InheritanceMetGenerics()
        {
            // Dit kan:
            Person p = new Student();

            List<Student> students = new List<Student>();
            students.Add(new Student { Name = "Piet" });

            List<Person> people = new List<Person>();
            people.Add(new Student { Name = "Piet" });

            // Dit mag natuurlijk niet:
            //students.Add(new Person());

            // Daarom kan dit ook niet:
            //List<Person> people = students;
            //people.Add(new Person());
            //people.Add(new Student());

            // Omgekeerd ook niet
            //students = people;

            // Omdat dit dan niet gegarandeerd is
            //Student s = students[0];

            // Dit kan allebei vanwege de co-variance van IEnumberale
            PrintAllPeople(people);
            PrintAllPeople(students);
        }


        private void PrintAllPeople(IEnumerable<Person> people)
        {
            foreach (var p in people)
            {
                output.WriteLine(p.Name);
            }
        }

        interface IGenericCoVariance<out T>
        {
            T Method();

            // Dit mag niet omdat "out T" alleen als return type gebruikt mag worden
            //void Method(T input);
        }

        interface IGenericContraVariance<in T>
        {
            // Dit mag niet omdat "in T" alleen als argument gebruikt mag worden
            //T Method();

            void Method(T input);
        }

        class Person
        {
            public string Name { get; internal set; }
        }

        class Student : Person
        {
        }

        [Fact]
        public void GegenereerdeTypesMetGenerics()
        {
            var a = new Generic<int>();
            var b = new Generic<int>();
            var c = new Generic<char>();

            Assert.Equal(2, a.Counter);
            Assert.Equal(2, b.Counter);
            Assert.Equal(1, c.Counter);
        }

        private class Generic<T>
        {
            // Demo mbv een static omdat die
            // waarde over classes van hetzelfde type
            // heen gedeeld wordt.
            public static int counter;
            public int Counter => counter;

            public Generic()
            {
                counter++;
            }
        }
    }
}

