using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork.Activation
{
    internal interface IActivation
    {
        double Activate(double x);
        double Gradient(double x);
    }
}
