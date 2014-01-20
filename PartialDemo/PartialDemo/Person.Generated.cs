using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartialDemo
{
    partial class Person
    {
        event Action<string> OnInitialize;
        public string Name { get; set; }

        public Person()
        {
            Initialize("Manuel");

            if (OnInitialize != null)
            {
                OnInitialize("Manuel");
            }
        }

        partial void Initialize(string name);
        partial void Remove();

    }
}
