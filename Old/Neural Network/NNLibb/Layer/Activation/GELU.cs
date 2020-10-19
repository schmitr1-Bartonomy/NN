using NNLib.Layer.Activiation;
using System;
using System.Collections.Generic;
using System.Text;

namespace NNLib.Layer.Activation
{
    class GELU : IActivation
    {
        public double Activate(double x)
        {
            return 0.5 * x * (1 + Math.Tanh(Math.Sqrt(2 / Math.PI) * (x + 0.044715 * Math.Pow(x, 3))));
        }

        public double Gradient(double x)
        {
            return 0.5 * Math.Tanh(0.5 * (0.0713548 * Math.Pow(x, 3) + 1.59577 * x)) + 
                (0.0535161 * Math.Pow(x, 3) + 0.398942 * x) * SecH2(0.0356774 * Math.Pow(x, 3) + 0.797885 * x) + 1 / 2;
        }
        private double SecH2(double x)
        {
            return 4 / Math.Pow((Math.Exp(-x) + Math.Exp(x)),2);
        }
    }
}
