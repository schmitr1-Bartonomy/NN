using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork.Activation
{
    class TanH : IActivation
    {
        public double Activate(double x)
        {
            return Math.Tanh(x);
        }

        public double Gradient(double x)
        {
            return 1 - Math.Pow(Math.Tan(x), 2);
        }
    }
}
