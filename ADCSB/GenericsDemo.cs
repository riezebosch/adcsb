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
                int index = 0;
                foreach (var item in items)
                {
                    if (index++ == count)
                    {
                        yield break;
                    }
                    yield return item;
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
    }
}

