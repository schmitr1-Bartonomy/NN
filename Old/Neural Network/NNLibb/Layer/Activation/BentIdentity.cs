using NNLib.Layer.Activiation;
using System;
using System.Collections.Generic;
using System.Text;

namespace NNLib.Layer.Activation
{
    class BentIdentity : IActivation
    {
        public double Activate(double x)
        {
            return (Math.Sqrt(Math.Pow(x, 2) + 1) - 1) / 2 + x;
        }

        public double Gradient(double x)
        {
            return x / (2 * Math.Sqrt(Math.Pow(x, 2) + 1)) + 1;
        }
    }
}
