using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork.Activation
{
    class ExponentialLinearUnit : IActivation
    {
        double a = 2;
        public double Activate(double x)
        {
            return x < 0 ? a * (Math.Exp(x) - 1) : x;
        }

        public double Gradient(double x)
        {
            return x < 0 ? a * Math.Exp(x) : 1;
        }
    }
}
