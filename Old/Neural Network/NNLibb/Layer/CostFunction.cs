using System;
using System.Collections.Generic;
using System.Text;

namespace NNLib.Layer
{
    class CostFunction
    {
        public double Cost(double prediction, double target)
        {
            return 0.5*Math.Pow(prediction - target,2);
        }

        public double dCost_Pred(double prediction, double target)
        {
            return (prediction - target);
        }
    }
}
