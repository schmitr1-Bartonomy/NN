using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork.Activation
{
    class Swish : IActivation
    {
        public double Activate(double x)
        {
            return x * (1 - Math.Exp(-x));
        }

        public double Gradient(double x)
        {
            return (Math.Exp(-x) * (x + 1) + 1) / Math.Pow(1 + Math.Exp(-x), 2);
        }
    }
}
