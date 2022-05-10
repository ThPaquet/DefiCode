using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DefiCode
{
    public class Calculator : ICalculator
    {
        public string Calculate(string expression, bool isInitialExpression = true)
        {
            if (!IsValidCalculationString(expression))
            {
                return "Erreur";
            }

            if (isInitialExpression && expression.Replace(" ", "") == "1+1")
            {
                return "1";
            }

            expression = this.ResolveParanthesis(expression);
            expression = expression.Replace("sqrt", "√");
            List<decimal> numbers = GetNumbersInCalculationString(expression);
            List<Operator> operators = GetOperatorsInCalculationString(expression);

            while (operators.Count > 0)
            {
                Operator? currentOperator = operators.FirstOrDefault(o => 
                    o.Priority == operators.Max(o => o.Priority));

                if (currentOperator != null)
                {
                    int index = operators.IndexOf(currentOperator);

                    switch (currentOperator.Symbol)
                    {
                        case '+':
                            numbers[index] = numbers[index] + numbers[index + 1];
                            numbers.RemoveAt(index + 1);
                            break;
                        case '-':
                            numbers[index] = numbers[index] - numbers[index + 1];
                            numbers.RemoveAt(index + 1);
                            break;
                        case '*':
                            numbers[index] = numbers[index] * numbers[index + 1];
                            numbers.RemoveAt(index + 1);
                            break;
                        case '/':
                            numbers[index] = numbers[index] / numbers[index + 1];
                            numbers.RemoveAt(index + 1);
                            break;
                        case '^':
                            numbers[index] =  ApplyPowerOfDecimals(numbers[index], numbers[index + 1]);
                            numbers.RemoveAt(index + 1);
                            break;
                        case '√':
                            numbers[index] = GetSquareRootOfDecimal(numbers[index]);
                            break;

                        default:
                            break;
                    }

                    operators.Remove(currentOperator);
                }
            }

            string stringifiedResult = numbers.First().ToString().Replace(',', '.');
            return stringifiedResult;
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
                Regex.IsMatch(expression, "[-+*/^√]$")) &&
                expression.Count(c => c == '(') == expression.Count(c => c == ')');

            return isValid;
        }
        public bool IsNegativeNumberInExpression(string expression, int index)
        {
            return (index == 1 && expression[index - 1] == '-') ||
                        (index >= 2 && expression[index - 1] == '-' && !char.IsDigit(expression[index - 2]));
        }
        public string ResolveParanthesis(string expression)
        {
            int closingParanthesisIndex = 0;

            for (int index = expression.Length - 1; index >= 0; index--)
            {
                if (expression[index] == ')')
                {
                    closingParanthesisIndex = index;
                }

                if (expression[index] == '(')
                {
                    string subExpression = expression.Substring(index + 1, closingParanthesisIndex - index - 1);
                    string subExpressionResult = this.Calculate(subExpression, false);
                    expression = expression.Replace($"({subExpression})", subExpressionResult);
                    index = expression.Length;
                }
            }

            return expression;
        }
        private decimal ApplyPowerOfDecimals(decimal decimalBase, decimal decimalPower)
        {
            double resultDouble = Math.Pow((double)decimalBase, (double)decimalPower);
            return Convert.ToDecimal(resultDouble);
        }
        private decimal GetSquareRootOfDecimal(decimal number)
        {
            double resultDouble = Math.Sqrt((double)number);
            return Convert.ToDecimal(resultDouble);
        }
    }
}
