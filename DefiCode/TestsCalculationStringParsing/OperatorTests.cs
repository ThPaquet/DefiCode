using DefiCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestsCalculationStringParsing
{
    public class OperatorTests
    {
        [Fact]
        public void Test_Ctor_Plus()
        {
            char symbol = '+';
            int expectedPriority = 1;

            Operator evaluatedOperator = new Operator(symbol);

            Assert.Equal(expectedPriority, evaluatedOperator.Priority);
            Assert.Equal(symbol, evaluatedOperator.Symbol);
        }

        [Fact]
        public void Test_Ctor_Minus()
        {
            char symbol = '-';
            int expectedPriority = 1;

            Operator evaluatedOperator = new Operator(symbol);

            Assert.Equal(expectedPriority, evaluatedOperator.Priority);
            Assert.Equal(symbol, evaluatedOperator.Symbol);
        }

        [Fact]
        public void Test_Ctor_Multiplication()
        {
            char symbol = '*';
            int expectedPriority = 2;

            Operator evaluatedOperator = new Operator(symbol);

            Assert.Equal(expectedPriority, evaluatedOperator.Priority);
            Assert.Equal(symbol, evaluatedOperator.Symbol);
        }

        [Fact]
        public void Test_Ctor_Division()
        {
            char symbol = '/';
            int expectedPriority = 2;

            Operator evaluatedOperator = new Operator(symbol);

            Assert.Equal(expectedPriority, evaluatedOperator.Priority);
            Assert.Equal(symbol, evaluatedOperator.Symbol);
        }

        [Fact]
        public void Test_Ctor_Exponent()
        {
            char symbol = '^';
            int expectedPriority = 3;

            Operator evaluatedOperator = new Operator(symbol);

            Assert.Equal(expectedPriority, evaluatedOperator.Priority);
            Assert.Equal(symbol, evaluatedOperator.Symbol);
        }

        [Fact]
        public void Test_Ctor_Root()
        {
            char symbol = '√';
            int expectedPriority = 3;

            Operator evaluatedOperator = new Operator(symbol);

            Assert.Equal(expectedPriority, evaluatedOperator.Priority);
            Assert.Equal(symbol, evaluatedOperator.Symbol);
        }
    }
}
