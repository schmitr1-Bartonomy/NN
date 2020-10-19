using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork.Activation
{
    internal enum Type {
        BinaryStep,
        Linear,
        Sigmoid,
        TanH,
        ReLU,
        LeakyReLU,
        ParameterisedReLU,
        ExponentialLinearUnit,
        Swish,
        Softmax
    }
    class ActivationFunction
    {
        Type type;
        public ActivationFunction(Type type)
        {
            this.type = type;
        }

        public override string ToString()
        {
            return type.ToString();
        }
    }
}
