using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ADCSB
{
    public class GenericsDemo
    {
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
            var mylist = new MyList(items);
            mylist.Add(4);

            Assert.Equal(4, mylist[3]);
        }

        [Fact]
        public void BuitenDeRangeVanToegevoegdeItemsOpzoekenGooitEenIndexOutOfRangeException()
        {
            var mylist = new MyList();
            Assert.Throws<IndexOutOfRangeException>(() => mylist[3]);
        }

        private class MyList
        {
            private int[] items;
            private int count;

            public MyList()
            {
                this.items = new int[10];
            }

            public MyList(int[] items)
            {
                this.items = items;
                count = items.Length;
            }

            internal void Add(int item)
            {
                EnsureCapacity();
                items[count++] = item;
            }

            private void EnsureCapacity()
            {
                if (items.Length == count)
                {
                    var temp = new int[count * 2];
                    Array.Copy(items, temp, count);

                    items = temp;
                }
            }

            public int this[int index]
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

