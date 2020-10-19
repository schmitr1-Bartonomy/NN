using NNLib.Layer.Activiation;
using System;
using System.Collections.Generic;
using System.Text;

namespace NNLib.Layer.Activation
{
    class ReLU : IActivation
    {
        public double Activate(double x)
        {
            return Math.Max(0, x);
        }

        public double Gradient(double x)
        {
            return x < 0 ? 0 : 1;
        }
    }
}
