using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace ADCSB
{
    public class DemoContext : DbContext
    {
        public IDbSet<Person> People { get; set; }
    }
}