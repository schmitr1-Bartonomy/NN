using System;

namespace NeuralNetworkLib.ActivationFunctions
{
    class SQ_RBF : IActivation
    {
        public double Activate(double x)
        {
            return Math.Abs(x) <= 1 ? 1 - Math.Pow(x, 2) / 2 :
                Math.Abs(x) < 2 ? Math.Pow(2 - Math.Abs(x), 2) / 2 :
                0;
        }

        public double Gradient(double x)
        {
            return Math.Abs(x) <= 1 ? -x :
                Math.Abs(x) < 2 ? x - Math.Sign(x):
                0;
        }
    }
}
