using System;

namespace NeuralNetworkLib.ActivationFunctions
{
    class Softsign : IActivation
    {
        public double Activate(double x)
        {
            return x / (1 - Math.Abs(x));
        }

        public double Gradient(double x)
        {
            return 1 / Math.Pow(1 + Math.Abs(x), 2);
        }
    }
}
