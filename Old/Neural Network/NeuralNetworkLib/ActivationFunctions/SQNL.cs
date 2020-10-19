using System;

namespace NeuralNetworkLib.ActivationFunctions
{
    class SQNL : IActivation
    {
        public double Activate(double x)
        {
            return x > 2 ? 1 :
                x >= 0 ? x - Math.Pow(x, 2) / 4 :
                x >= -2 ? x + Math.Pow(x, 2) / 4 :
                -1;
        }

        public double Gradient(double x)
        {
            return x > 2 ? 0 :
                x >= 0 ? 1 - x / 2 :
                x >= -2 ? 1 + x / 2 :
                0;
        }
    }
}
