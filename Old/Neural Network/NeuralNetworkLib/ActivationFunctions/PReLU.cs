

namespace NeuralNetworkLib.ActivationFunctions
{
    class PReLU : IActivation
    {
        double a;
        public PReLU(double a = 0.5)
        {
            this.a = a;
        }
        public double Activate(double x)
        {
            return x < 0 ? a * x : x;
        }

        public double Gradient(double x)
        {
            return x < 0 ? a : 1;
        }
    }
}
