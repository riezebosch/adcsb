using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace ADCSB
{
    public class ExpressionTreeDemo
    {
        private readonly ITestOutputHelper output;

        public ExpressionTreeDemo(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public void HetVerschilTussenEenLambdaEnEenExpressionTree()
        {
            Predicate<int> pred = i => i % 2 == 0;
            PrintLambda(pred);

            Expression<Predicate<int>> expr = i => i % 2 == 0;
            PrintExpression(expr);
        }

        private void PrintExpression(Expression<Predicate<int>> expr)
        {
            //expr = Expression.Lambda<Predicate<int>>(Expression.Equal(((BinaryExpression)expr.Body).Left, Expression.Constant(1)));
            expr.Compile().Invoke(13);
            output.WriteLine(expr.ToString());
        }

        private void PrintLambda(Predicate<int> p)
        {
            p(13);
            output.WriteLine(p.ToString());
        }

        [Fact]
        public void LambdaVsExpression()
        {
            int[] items = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var even1 = items.Where(i => i % 2 == 0);

            using (var context = new DummyContext())
            {
                var even2 = context.Items.Where(i => i.Id % 2 == 0);
            }
        }

        private class DummyContext : DbContext
        {
            public DbSet<Item> Items { get; set; }
        }
        public class Item
        {
            public int Id { get; set; }
        }
    }


}
