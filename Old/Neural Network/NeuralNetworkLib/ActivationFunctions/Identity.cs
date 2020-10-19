
namespace NeuralNetworkLib.ActivationFunctions
{
    class Identity : IActivation
    {
        public double Activate(double x)
        {
            return x;
        }

        public double Gradient(double x)
        {
            return 1;
        }
    }
}
