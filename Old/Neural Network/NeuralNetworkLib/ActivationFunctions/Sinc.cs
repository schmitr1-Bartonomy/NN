using System;

namespace NeuralNetworkLib.ActivationFunctions
{
    class Sinc : IActivation
    {
        public double Activate(double x)
        {
            return x == 0 ? 1 : Math.Sin(x) / x;
        }

        public double Gradient(double x)
        {
            return x == 0 ? 0 : Math.Cos(x) / x - Math.Sin(x) / Math.Pow(x, 2);
        }
    }
}
