using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefiCode
{
    public class Operator
    {
        public char Symbol { get; set; }
        public int Priority { get; }

        public Operator(char symbol)
        {
            if (symbol == '+' || symbol == '-')
            {
                this.Priority = 1;
            }

            else if (symbol == '*' || symbol == '/')
            {
                this.Priority = 2;
            }

            else if (symbol == '^' || symbol == '√')
            {
                this.Priority = 3;
            }

            else
            {
                throw new ArgumentException("Invalid Symbol");
            }

            this.Symbol = symbol;
        }
    }
}
