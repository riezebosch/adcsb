using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace ADCSB
{
    public class AnonymousMethodDemo
    {
        private readonly ITestOutputHelper output;

        public AnonymousMethodDemo(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void VerschillendeSchrijfwijzenVanEenAnonymousMethod()
        {
            // Methode met een naam
            Predicate<int> a1 = MethodeMetEenNaam;

            // Ouderwetse notatie zoals in C# 2.0
            Predicate<int> a2 = delegate (int v) { return v % 2 == 0; };

            // Nieuwere notatie met een lambda maar nog steeds wat verbose.
            Predicate<int> a3 = (int v) => { return v % 2 == 0; };

            // Impliciet bekend wat het type van v is
            Predicate<int> a4 = (v) => { return v % 2 == 0; };

            // En maar 1 argument mogen ronde haakjes weg
            Predicate<int> a5 = v => { return v % 2 == 0; };

            // Met maar 1 statement mogen de return en accolades weg
            Predicate<int> a6 = v => v % 2 == 0;
        }

        private bool MethodeMetEenNaam(int v)
        {
            return v % 2 == 0;
        }

        [Fact]
        public void PrintDeNamenVanDeAnonymousMethods()
        {
            var nested = GetType().GetNestedTypes(BindingFlags.NonPublic);
            foreach (var type in nested)
            {
                output.WriteLine(type.Name);
                output.WriteLine("========");
                var methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
                output.WriteLine(string.Join(Environment.NewLine, methods.Select(m => m.Name)));

                // I rest my case...
                //output.WriteLine(string.Join(Environment.NewLine, methods.Select(delegate (MethodInfo m) { return m.Name; })));
            }
        }
    }
}
