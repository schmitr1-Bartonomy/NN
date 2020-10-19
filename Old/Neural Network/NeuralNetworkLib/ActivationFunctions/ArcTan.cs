using System;

namespace NeuralNetworkLib.ActivationFunctions
{
    class ArcTan : IActivation
    {
        public double Activate(double x)
        {
            return Math.Atan(x);
        }

        public double Gradient(double x)
        {
            return 1 / (Math.Pow(x, 2) + 1);
        }
    }
}
