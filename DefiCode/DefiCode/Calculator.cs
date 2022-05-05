using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DefiCode
{
    public class Calculator
    {
        public string Calculate(string expression)
        {
            if (!IsValidCalculationString(expression))
            {
                return "Erreur";
            }

            List<Operator> operators = GetOperatorsInCalculationString(expression);
            List<decimal> numbers = GetNumbersInCalculationString(expression);

            while (operators.Count > 0)
            {
                Operator currentOperator = operators.FirstOrDefault(o => 
                    o.Priority == operators.Max(o => o.Priority));

                if (currentOperator != null)
                {
                    int index = operators.IndexOf(currentOperator);

                    switch (currentOperator.Symbol)
                    {
                        case '+':
                            numbers[index] = numbers[index] + numbers[index + 1];
                            break;
                        case '-':
                            numbers[index] = numbers[index] - numbers[index + 1];
                            break;
                        case '*':
                            numbers[index] = numbers[index] * numbers[index + 1];
                            break;
                        case '/':
                            numbers[index] = numbers[index] / numbers[index + 1];
                            break;
                        case '^':
                            numbers[index] = Convert.ToDecimal(Math.Pow((double)numbers[index], (double)numbers[index + 1]));
                            break;
                        default:
                            break;
                    }

                    operators.Remove(currentOperator);
                    numbers.RemoveAt(index + 1);
                }
            }

            return numbers[0].ToString();
        }
        public List<decimal> GetNumbersInCalculationString(string expression)
        {
            List<decimal> numbers = new List<decimal>();
            expression = expression.Replace(" ", "");

            for (int index = 0; index < expression.Length; index++)
            {
                if (char.IsDigit(expression[index]))
                {
                    string numberString = "";

                    if (IsNegativeNumberInExpression(expression, index))
                    {
                        numberString += "-";
                    }
                    
                    numberString += expression[index].ToString();

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
        public List<Operator> GetOperatorsInCalculationString(string expression)
        {
            char[] recognizedOperators = { '+', '-', '*', '/', '^', '√' };
            List<Operator> operators = new List<Operator>();
            expression = expression.Replace(" ", "");

            for (int index = 0; index < expression.Length; index++)
            {
                if (recognizedOperators.Contains(expression[index]) &&
                    (index > 0 || expression[index] == '√') &&
                    !(index > 0 && recognizedOperators.Contains(expression[index - 1]) && expression[index] == '-'))
                {
                    Operator operatorToAdd = new Operator(expression[index]);
                    operators.Add(operatorToAdd);
                }
            }

            return operators;
        }

        public bool IsValidCalculationString(string expression)
        {
            char[] recognizedOperators = { '+', '-', '*', '/', '^', '√' };
            bool isValid = true;

            expression = expression.Replace(" ", "");

            isValid = !(
                Regex.IsMatch(expression, "^[+*/^]") ||
                Regex.IsMatch(expression, "^-[+/*^]") ||
                Regex.IsMatch(expression, "^--") ||
                Regex.IsMatch(expression, "√[+*/^√]") ||
                Regex.IsMatch(expression, "[-+*/^][+*/^]") ||
                Regex.IsMatch(expression, "/0") ||
                Regex.IsMatch(expression, "[-+*/^√]$"));

            return isValid;
        }
        public bool IsNegativeNumberInExpression(string expression, int index)
        {
            return (index == 1 && expression[index - 1] == '-') ||
                        (index >= 2 && expression[index - 1] == '-' && !char.IsDigit(expression[index - 2]));
        }
    }
}
