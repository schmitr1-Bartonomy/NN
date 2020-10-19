using System;

namespace NeuralNetworkLib.ActivationFunctions
{
    class Sigmoid : IActivation
    {
        public double Activate(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }

        public double Gradient(double x)
        {
            return Activate(x) * (1 - Activate(x));
        }
    }
}
