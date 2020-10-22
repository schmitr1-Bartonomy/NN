using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork.Activation
{
    class Linear : IActivation
    {
        double a = 1.5;
        public double Activate(double x)
        {
            return a * x;
        }

        public double Gradient(double x)
        {
            return a;
        }
    }
}
