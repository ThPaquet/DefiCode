using DefiCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestsCalculationStringParsing
{
    public class CalculatorTest 
    {

        #region GetNumbersInCalculationString 
        [Fact]
        public void Test_GetNumbersInCalculationString_OneInt()
        {
            Calculator calculator = new Calculator();
            List<decimal> numbers = new List<decimal>();
            string calculationString = "1";

            numbers = calculator.GetNumbersInCalculationString(calculationString);

            Assert.Equal(1, numbers.Count);
            Assert.Equal(1, numbers[0]);
        }

        [Fact]
        public void Test_GetNumbersInCalculationString_OneInt_SplitByWhitespace()
        {
            Calculator calculator = new Calculator();
            List<decimal> numbers = new List<decimal>();
            string calculationString = "1 2 3";

            numbers = calculator.GetNumbersInCalculationString(calculationString);

            Assert.Equal(1, numbers.Count);
            Assert.Equal(123, numbers[0]);
        }

        [Fact]
        public void Test_GetNumbersInCalculationString_TwoInts()
        {
            Calculator calculator = new Calculator();
            List<decimal> numbers = new List<decimal>();
            string calculationString = "1 + 2";

            numbers = calculator.GetNumbersInCalculationString(calculationString);

            Assert.Equal(2, numbers.Count);
            Assert.Equal(1, numbers[0]);
            Assert.Equal(2, numbers[1]);
        }

        [Fact]
        public void Test_GetNumbersInCalculationString_TwoInts_SplitByWhitespace()
        {
            Calculator calculator = new Calculator();
            List<decimal> numbers = new List<decimal>();
            string calculationString = "1 2 3 + 43 2";

            numbers = calculator.GetNumbersInCalculationString(calculationString);

            Assert.Equal(2, numbers.Count);
            Assert.Equal(123, numbers[0]);
            Assert.Equal(432, numbers[1]);
        }

        [Fact]
        public void Test_GetNumbersInCalculationString_OneDecimals()
        {
            Calculator calculator = new Calculator();
            List<decimal> numbers = new List<decimal>();
            string calculationString = "1.2";

            numbers = calculator.GetNumbersInCalculationString(calculationString);

            Assert.Equal(1, numbers.Count);
            Assert.Equal(1.2m, numbers[0]);
        }

        [Fact]
        public void Test_GetNumbersInCalculationString_OneDecimals_SplitByWhitespace()
        {
            Calculator calculator = new Calculator();
            List<decimal> numbers = new List<decimal>();
            string calculationString = "1 . 2";

            numbers = calculator.GetNumbersInCalculationString(calculationString);

            Assert.Equal(1, numbers.Count);
            Assert.Equal(1.2m, numbers[0]);
        }

        [Fact]
        public void Test_GetNumbersInCalculationString_TwoDecimals()
        {
            Calculator calculator = new Calculator();
            List<decimal> numbers = new List<decimal>();
            string calculationString = "1.2 + 2.7";

            numbers = calculator.GetNumbersInCalculationString(calculationString);

            Assert.Equal(2, numbers.Count);
            Assert.Equal(1.2m, numbers[0]);
            Assert.Equal(2.7m, numbers[1]);
        }

        [Fact]
        public void Test_GetNumbersInCalculationString_TwoDecimals_SplitByWhitespace()
        {
            Calculator calculator = new Calculator();
            List<decimal> numbers = new List<decimal>();
            string calculationString = "1 .2 + 2. 72";

            numbers = calculator.GetNumbersInCalculationString(calculationString);

            Assert.Equal(2, numbers.Count);
            Assert.Equal(1.2m, numbers[0]);
            Assert.Equal(2.72m, numbers[1]);
        }

        #endregion
        #region GetOperatorsInCalculationString

        [Fact]
        public void Test_GetOperatorsInCalculationString_OnePlus()
        {
            Calculator calculator = new Calculator();
            List<Operator> operators = new List<Operator>();
            string expression = "1 + 1";

            operators = calculator.GetOperatorsInCalculationString(expression);
            
            Assert.Equal(1, operators.Count);
            Assert.Equal('+', operators[0].Symbol);
            Assert.Equal(1, operators[0].Priority);
        }

        [Fact]
        public void Test_GetOperatorsInCalculationString_OneRoot()
        {
            Calculator calculator = new Calculator();
            List<Operator> operators = new List<Operator>();
            string expression = "√1";

            operators = calculator.GetOperatorsInCalculationString(expression);

            Assert.Equal(1, operators.Count);
            Assert.Equal('√', operators[0].Symbol);
            Assert.Equal(3, operators[0].Priority);
        }

        [Fact]
        public void Test_GetOperatorsInCalculationString_PlusMinus()
        {
            Calculator calculator = new Calculator();
            List<Operator> operators = new List<Operator>();
            string expression = "1 +-1";

            operators = calculator.GetOperatorsInCalculationString(expression);

            Assert.Equal(1, operators.Count);
            Assert.Equal('+', operators[0].Symbol);
            Assert.Equal(1, operators[0].Priority);
            Assert.DoesNotContain(operators, o => o.Symbol == '-');
        }

        [Fact]
        public void Test_GetOperatorsInCalculationString_MultipleOperators()
        {
            Calculator calculator = new Calculator();
            List<Operator> operators = new List<Operator>();
            string expression = "1 +-1 / 3 ^5";

            operators = calculator.GetOperatorsInCalculationString(expression);

            Assert.Equal(3, operators.Count);
            Assert.Contains(operators, o => o.Symbol == '+');
            Assert.Contains(operators, o => o.Symbol == '^');
            Assert.Contains(operators, o => o.Symbol == '/');
        }
        #endregion
        #region IsValidCalculationString

        [Fact]
        public void Test_IsValidCalculationString_StartWithInvalidOperator()
        {
            Calculator calculator = new Calculator();
            string expression = "";

            expression = "+1";
            Assert.False(calculator.IsValidCalculationString(expression));

            expression = "*5";
            Assert.False(calculator.IsValidCalculationString(expression));

            expression = "/3";
            Assert.False(calculator.IsValidCalculationString(expression));

            expression = "^4";
            Assert.False(calculator.IsValidCalculationString(expression));

            expression = "--2";
            Assert.False(calculator.IsValidCalculationString(expression));
        }

        [Fact]
        public void Test_IsValidCalculationString_StartWithValidOperator()
        {
            Calculator calculator = new Calculator();
            string expression = "";

            expression = "-1";
            Assert.True(calculator.IsValidCalculationString(expression));

            expression = "√4";
            Assert.True(calculator.IsValidCalculationString(expression));
        }

        [Fact]
        public void Test_IsValidCalculationString_InvalidOperatorSequence()
        {
            Calculator calculator = new Calculator();
            string expression = "";

            expression = "-+1";
            Assert.False(calculator.IsValidCalculationString(expression));

            expression = "+*1";
            Assert.False(calculator.IsValidCalculationString(expression));

            expression = "*/1";
            Assert.False(calculator.IsValidCalculationString(expression));

            expression = "√+4";
            Assert.False(calculator.IsValidCalculationString(expression));

            expression = "^+1";
            Assert.False(calculator.IsValidCalculationString(expression));
        }

        [Fact]
        public void Test_IsValidCalculationString_ValidOperatorSequence()
        {
            Calculator calculator = new Calculator();
            string expression = "";

            expression = "√-1";
            Assert.True(calculator.IsValidCalculationString(expression));

            expression = "3 * -1";
            Assert.True(calculator.IsValidCalculationString(expression));

            expression = "3 / -1";
            Assert.True(calculator.IsValidCalculationString(expression));

            expression = "3 ^ -1";
            Assert.True(calculator.IsValidCalculationString(expression));

            expression = "3 ^ √1";
            Assert.True(calculator.IsValidCalculationString(expression));
        }

        [Fact]
        public void Test_IsValidCalculationString_DividedByZero()
        {
            Calculator calculator = new Calculator();
            string expression = "";

            expression = "7  / 0";
            Assert.False(calculator.IsValidCalculationString(expression));
        }

        [Fact]
        public void Test_IsValidCalculationString_EndsWithOperator()
        {
            Calculator calculator = new Calculator();
            string expression = "";

            expression = "3  / 4 + ";
            Assert.False(calculator.IsValidCalculationString(expression));

            expression = "4-";
            Assert.False(calculator.IsValidCalculationString(expression));

            expression = "1*2* ";
            Assert.False(calculator.IsValidCalculationString(expression));

            expression = "7  / ";
            Assert.False(calculator.IsValidCalculationString(expression));

            expression = "2 ^ 2 ^ ";
            Assert.False(calculator.IsValidCalculationString(expression));

            expression = "0 * 3 √";
            Assert.False(calculator.IsValidCalculationString(expression));
        }
        #endregion
    }
}
