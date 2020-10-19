using System;

namespace NeuralNetworkLib.ActivationFunctions
{
    class SiLU : IActivation
    {
        public double Activate(double x)
        {
            return x / (1 + Math.Exp(-x));
        }

        public double Gradient(double x)
        {
            return (1 + Math.Exp(-x)+x*Math.Exp(-x))/Math.Pow(1+Math.Exp(-x),2);
        }
    }
}
