using System;

namespace NeuralNetworkLib.ActivationFunctions
{
    class ELU : IActivation
    {
        double a;
        internal ELU(double a = 0.5)
        {
            this.a = a;
        }
        public double Activate(double x)
        {
            return x < 0 ? a * (Math.Exp(x) - 1) : x;
        }

        public double Gradient(double x)
        {
            return x <= 0 ? Activate(x) + a : 1;
        }
    }
}
