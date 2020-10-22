using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork.Activation
{
    class ParameterisedReLU : IActivation
    {
        double a = .5;
        public double Activate(double x)
        {
            return x < 0 ? a * x : x; 
        }

        public double Gradient(double x)
        {
            return x < 0 ? a : 1;
        }
    }
}
