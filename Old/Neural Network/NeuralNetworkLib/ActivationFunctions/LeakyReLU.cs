

namespace NeuralNetworkLib.ActivationFunctions
{
    class LeakyReLU : IActivation
    {
        public double Activate(double x)
        {
            return x < 0 ? 0.01 * x : x;
        }

        public double Gradient(double x)
        {
            return x < 0 ? 0.01 : 1;
        }
    }
}
