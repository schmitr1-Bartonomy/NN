using NNLib.Layer.Activiation;
using System;
using System.Collections.Generic;
using System.Text;

namespace NNLib.Layer.Activation
{
    class SoftPlus : IActivation
    {
        public double Activate(double x)
        {
            return Math.Log(1 + Math.Exp(x));
        }

        public double Gradient(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }
    }
}
