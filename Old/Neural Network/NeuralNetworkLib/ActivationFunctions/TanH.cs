using System;

namespace NeuralNetworkLib.ActivationFunctions
{
    class TanH : IActivation
    {
        public double Activate(double x)
        {
            return (Math.Exp(x) - Math.Exp(-x)) / (Math.Exp(x) + Math.Exp(-x));
        }

        public double Gradient(double x)
        {
            return 1 - Math.Pow(Activate(x), 2);
        }
    }
}
