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
            string calculationString = "1 .2 + 2. 7";

            numbers = calculator.GetNumbersInCalculationString(calculationString);

            Assert.Equal(2, numbers.Count);
            Assert.Equal(1.2m, numbers[0]);
            Assert.Equal(2.7m, numbers[1]);
        }
    }
}
