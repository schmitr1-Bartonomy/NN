using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork.Activation
{
    class BinaryStep : IActivation
    {
        public double Activate(double x)
        {
            return x < 0 ? 0 : 1;
        }

        public double Gradient(double x)
        {
            return 0;
        }
    }
}
