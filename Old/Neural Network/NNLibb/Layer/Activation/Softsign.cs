using NNLib.Layer.Activiation;
using System;
using System.Collections.Generic;
using System.Text;

namespace NNLib.Layer.Activation
{
    class Softsign : IActivation
    {
        public double Activate(double x)
        {
            return x / (1 - Math.Abs(x));
        }

        public double Gradient(double x)
        {
            return 1 / Math.Pow(1 + Math.Abs(x), 2);
        }
    }
}
