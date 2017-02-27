using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace ADCSB
{
    public class EventDemo
    {
        private readonly ITestOutputHelper output;

        public EventDemo(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void DemoVanPublishSubscribeMetDelegates()
        {
            var iets = new Iets1();
            iets.React += Console.WriteLine;

            bool aangeroepen = false;
            iets.React += i => aangeroepen = true;

            iets.Notify(13);
            Assert.True(aangeroepen);
        }

        [Fact]
        public void HetProbleemBijPublishSubscribeMetDelegates()
        {
            var iets = new Iets1();
            iets.React += Console.WriteLine;

            bool aangeroepen = false;
            iets.React += i => aangeroepen = true;

            iets.React = null;

            iets.Notify(13);

            // De lambda wordt niet meer uitgevoerd, daarom verwachten we False
            Assert.False(aangeroepen);
        }

        public void HetProbleemOpgelostMetEvents()
        {
            var iets = new Iets2();

            // Je mag subscriben
            iets.React += (sender, e) => Console.WriteLine(e.Value);

            // Je mag removen
            iets.React -= (sender, e) => Console.WriteLine(e.Value);

            // Maar je mag niet het lijstje overschrijven:
            // iets.React = null;
        }

        private class Iets1
        {
            public Action<int> React { get; set; }

            public void Notify(int v)
            {
                React?.Invoke(v);
            }
        }

        private class Iets2
        {
            // ipv Action<int> het officiele event pattern van MS:
            public event EventHandler<ReactEventArgs> React;

            // Een event levert door de compiler de volgende code op:
            // public Action<int> React { add; remove; }
            //                            ===  ======

            public void Notify(int v)
            {
                React?.Invoke(this, new ReactEventArgs { Value = v });
            }

        }
        public class ReactEventArgs : EventArgs
        {
            public int Value { get; set; }
        }

        [Fact]
        public void KunnenComponentenBijElkaarsRegistratie()
        {
            var iets = new Iets2();
            var componentA = new ComponentA(iets);
            var componentB = new ComponentB(iets);

            iets.Notify(25);
            Assert.NotEqual(25, componentA.Received);
        }

        [Fact]
        public void BewijsVoorDeGenereerdeMethodesBijEenEvent()
        {
            var i1 = typeof(Iets1).GetMethods();
            output.WriteLine("Delegate in een property:");
            output.WriteLine(string.Join(", ", i1.Select(m => m.Name)));

            var i2 = typeof(Iets2).GetMethods();
            output.WriteLine("Met een event:");
            output.WriteLine(string.Join(", ", i2.Select(m => m.Name)));

        }

        private class ComponentA
        {
            public ComponentA(Iets2 iets)
            {
                iets.React += Whatever;
            }

            public int Received { get; private set; }

            private void Whatever(object sender, ReactEventArgs e)
            {
                Received = e.Value;
            }
        }

        private class ComponentB
        {
            public ComponentB(Iets2 iets)
            {
                // iets.React -= ???

                //iets.React -= new ComponentA(iets).Whatever;

                // Met reflection lukt het natuurlijk wel.
                var react = iets
                    .GetType()
                    .GetField("React", BindingFlags.NonPublic | BindingFlags.Instance);

                react.SetValue(iets, null);

            }
        }
    }
}
