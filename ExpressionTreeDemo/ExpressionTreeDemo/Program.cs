using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTreeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            ExecuteExpression(m => m % 2 == 0);

            //new List<int> { 1, 2, 3, 4, 5 }.Where(i => i %2 ==0)
        }

        static void Execute(Func<int, bool> f)
        {
            Console.WriteLine(f.Method.Name);
            if (f(13))
            {
                Console.WriteLine("TRUE");
            }
            else
            {
                Console.WriteLine("FALSE");
            }
        }

        static void ExecuteExpression(Expression<Func<int, bool>> f)
        {
            Console.WriteLine(f);
            
        }
    }
}
