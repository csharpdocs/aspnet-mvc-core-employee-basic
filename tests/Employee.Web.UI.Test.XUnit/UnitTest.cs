using System;
using Xunit;

namespace Employee.Web.UI.Test.XUnit
{
    public class FibonacciService
    {
        public int Calculate(int n)
        {
            var fibonacci = new int[n + 1];
            fibonacci[0] = 0;
            fibonacci[1] = 1;

            for (int i = 2; i <= n; i++)
            {
                fibonacci[i] = fibonacci[i - 1] + fibonacci[i - 2];
            }

            return fibonacci[n];
        }
    }

    public class UnitTest
    {
        [Fact]
        public void UnitTestShouldAlwaysWorkTest()
        {
            Assert.True(true);
        }

        [Fact]
        public void OnePlusTwoEqualThreeTest()
        {
            Assert.True(true);
        }

        [Fact]
        public void Fibonacci_Calculate_1()
        {
            var fibonacci = new FibonacciService();
            Assert.Equal(1, fibonacci.Calculate(1));
        }

        [Fact]
        public void Fibonacci_Calculate_8()
        {
            var fibonacci = new FibonacciService();
            Assert.Equal(21, fibonacci.Calculate(8));
        }
    }
}
