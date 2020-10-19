using System;

namespace NeuralNetworkLib.ActivationFunctions
{
    class Sinusoid : IActivation
    {
        public double Activate(double x)
        {
            return Math.Sin(x);
        }

        public double Gradient(double x)
        {
            return Math.Cos(x);
        }
    }
}
