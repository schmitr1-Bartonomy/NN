using NNLib.Layer.Activiation;
using System;
using System.Collections.Generic;
using System.Text;

namespace NNLib.Layer.Activation
{
    class Sigmoid : IActivation
    {
        public double Activate(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }

        public double Gradient(double x)
        {
            return Activate(x) * (1 - Activate(x));
        }
    }
}
