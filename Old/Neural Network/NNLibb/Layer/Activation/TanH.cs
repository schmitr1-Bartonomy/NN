using NNLib.Layer.Activiation;
using System;
using System.Collections.Generic;
using System.Text;

namespace NNLib.Layer.Activation
{
    class TanH : IActivation
    {
        public double Activate(double x)
        {
            return (Math.Exp(x) - Math.Exp(-x)) / (Math.Exp(x) + Math.Exp(-x));
        }

        public double Gradient(double x)
        {
            return 1 - Math.Pow(Activate(x), 2);
        }
    }
}
