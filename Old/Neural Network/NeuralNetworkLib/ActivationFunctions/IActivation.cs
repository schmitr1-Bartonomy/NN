

namespace NeuralNetworkLib.ActivationFunctions
{
    internal interface IActivation
    {
        double Activate(double x);
        double Gradient(double x);
    }
}
