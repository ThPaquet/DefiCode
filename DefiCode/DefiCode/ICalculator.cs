using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefiCode
{
    public interface ICalculator
    {
        public string Calculate(string expression, bool isInitialExpression = true);
    }
}
