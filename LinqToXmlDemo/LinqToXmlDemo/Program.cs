using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LinqToXmlDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            XNamespace ns = "urn:www-infosupport-com:adcsb:2013:xml-demo";
            Console.WriteLine(ns + "demo");
            
            var guitarPlayers = from person in XDocument.Load("XMLFile1.xml")
                                    .Element(ns + "people")
                                    .Elements(ns + "person")
                                from instr in person.Elements(ns + "instrument")
                                where (string)instr.Attribute("type") == "Guitar"
                                select (string)person.Attribute("name");

            foreach (var p in guitarPlayers)
            {
                Console.WriteLine(p);
            }
        }
    }
}
