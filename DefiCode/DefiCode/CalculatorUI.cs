using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefiCode
{
    public class CalculatorUI
    {
        private readonly ICalculator _calculator;

        public CalculatorUI(ICalculator p_calculator)
        {
            this._calculator = p_calculator;
        }

        public void Start()
        {
            string expression = "";

            while (expression != "q")
            {
                Console.Write("Write the expression to solve (q to quit) : ");
                expression = Console.ReadLine().ToLower();

                if (expression != "q")
                {
                    string result = _calculator.Calculate(expression);

                    Console.WriteLine($"Result : {result}");
                }
            }
        }
    }
}
