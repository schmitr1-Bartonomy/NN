using System;

namespace NeuralNetworkLib.ActivationFunctions
{
    class Gaussian : IActivation
    {
        public double Activate(double x)
        {
            return Math.Exp(-Math.Pow(x, 2));
        }

        public double Gradient(double x)
        {
            return -2 * x * Math.Exp(-Math.Pow(x, 2));
        }
    }
}
