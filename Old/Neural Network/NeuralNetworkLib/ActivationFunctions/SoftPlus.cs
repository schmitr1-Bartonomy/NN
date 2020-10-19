using System;

namespace NeuralNetworkLib.ActivationFunctions
{
    class SoftPlus : IActivation
    {
        public double Activate(double x)
        {
            return Math.Log(1 + Math.Exp(x));
        }

        public double Gradient(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }
    }
}
