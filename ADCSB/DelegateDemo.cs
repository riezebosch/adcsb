using System;
using System.Linq;
using Xunit;

namespace ADCSB
{
    delegate void Delegator();
    delegate void Delegator1(int i);
    delegate void Delegator2(int i = 10, string s = null);

    delegate void Delegator<T>(T t1);
    delegate void Delegator<T1, T2>(T1 t1, T2 t2);
    delegate void Delegator<T1, T2, T3>(T1 t1, T2 t2, T3 t3);

    interface DelegatorByInterface
    {
        void Invoke();
    }

    public class DelegateDemo
    {

        [Fact]
        public void TestMethod1()
        {
            Delegator d = new Delegator(VoorbeeldFunctie);
            d.Invoke();

            DoeIetsMetDeDelegator(d);
            DoeIetsMetDeDelegator(() => Console.WriteLine("hoi"));

            //DoeIetsMetDeDelegatorInterface(this.VoorbeeldFunctie);
            //DoeIetsMetDeDelegatorInterface(() => Console.WriteLine("hoi"));

            ////De java manier:
            //DoeIetsMetDeDelegatorInterface(new DelegatorByInterface() { Invoke() { Console.WriteLine("hoi" } });
        }

        private void DoeIetsMetDeDelegator(Delegator d)
        {
            d.Invoke();
        }

        private void DoeIetsMetDeDelegatorInterface(DelegatorByInterface d)
        {
            d.Invoke();
        }

        private void VoorbeeldFunctie()
        {
            Console.WriteLine("Voorbeeld");
        }

        [Fact]
        public void VerschillendeSchrijfwijzenVoorEenDelegate()
        {
            Delegator d0 = new Delegator(VoorbeeldFunctie);
            var d1 = new Delegator(VoorbeeldFunctie);
            Delegator d2 = VoorbeeldFunctie;

            // Links of rechts moet je expliciet zijn
            //var d3 = VoorbeeldFunctie;

            d0.Invoke();
            d0();

            Delegator d3 = null;
            if (d3 != null)
            {
                d3();
            }

            // De enige plek waar ik expliciet een Invoke doe is
            // met de null-condition omdat die op de andere notatie niet werkt
            // d3 ?();
            d3?.Invoke();
        }

        

        [Fact]
        public void NogMeerDelegates()
        {
            Delegator1 d1 = VoorbeeldFunctie;
            d1(10);

            Delegator2 d2 = VoorbeeldFunctie;
            d2(10, "hoi");
            d2();
            d2(13);


            Delegator<int, string> d3 = VoorbeeldFunctie;
            d3(10, "hoi");

            // De generic delegates zonder return type die standaard 
            // in het .NET Framework al zitten
            Action<int, string> d4 = VoorbeeldFunctie;
            d4(10, "hoi");

            // De generic delegates MET return type die standaard
            // in het .NET Framework zitten.
            Func<int> f = VoorbeeldFunctieMetResult;
            int result = f();
            Assert.Equal(13, result);

            // Deze twee zijn functioneel gelijk aan elkaar.
            Predicate<int> p1 = VoorbeeldFunctieMetResult;
            Func<int, bool> p2 = VoorbeeldFunctieMetResult;
        }

        private static void VoorbeeldFunctie(int i)
        {

        }

        private static void VoorbeeldFunctie(int i, string s)
        {
        }

        private static int VoorbeeldFunctieMetResult()
        {
            return 13;
        }

        private static bool VoorbeeldFunctieMetResult(int i)
        {
            return true;
        }
    }
}
