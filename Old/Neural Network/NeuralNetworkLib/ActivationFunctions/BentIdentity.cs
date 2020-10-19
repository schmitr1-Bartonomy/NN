using System;

namespace NeuralNetworkLib.ActivationFunctions
{
    class BentIdentity : IActivation
    {
        public double Activate(double x)
        {
            return (Math.Sqrt(Math.Pow(x, 2) + 1) - 1) / 2 + x;
        }

        public double Gradient(double x)
        {
            return x / (2 * Math.Sqrt(Math.Pow(x, 2) + 1)) + 1;
        }
    }
}
