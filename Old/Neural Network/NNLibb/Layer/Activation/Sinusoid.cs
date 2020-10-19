using NNLib.Layer.Activiation;
using System;
using System.Collections.Generic;
using System.Text;

namespace NNLib.Layer.Activation
{
    class Sinusoid : IActivation
    {
        public double Activate(double x)
        {
            return Math.Sin(x);
        }

        public double Gradient(double x)
        {
            return Math.Cos(x);
        }
    }
}
