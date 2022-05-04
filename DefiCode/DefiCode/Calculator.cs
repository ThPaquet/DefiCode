using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefiCode
{
    public class Calculator
    {
        //public float Calculate(string expression)
        //{
        //    List<float> numbers = new List<float>();
        //    List<char> operators = new List<char>();

        //    expression = expression.Trim();
        //}

        public List<decimal> GetNumbersInCalculationString(string expression)
        {
            List<decimal> numbers = new List<decimal>();
            expression = expression.Replace(" ", "");

            for (int index = 0; index < expression.Length; index++)
            {
                if (char.IsDigit(expression[index]))
                {
                    string numberString = expression[index].ToString();

                    while (index < expression.Length - 1 && 
                        (char.IsDigit(expression[index + 1]) || 
                        (expression[index + 1] == '.') || expression[index + 1] == ','))
                    {
                        numberString += expression[index + 1];
                        index++;
                    }

                    numberString = numberString.Replace('.', ',');
                    decimal parsedNumber = decimal.Parse(numberString);
                    numbers.Add(parsedNumber);
                }
            }

            return numbers;
        }
    }
}
