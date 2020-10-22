using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork.Activation
{
    internal enum Type {
        // https://www.analyticsvidhya.com/blog/2020/01/fundamentals-deep-learning-activation-functions-when-to-use-them/
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
        Dictionary<Type, IActivation> typeDict = new Dictionary<Type, IActivation>()
        {
            {Type.BinaryStep, new BinaryStep() },
            {Type.Linear, new Linear() },
            {Type.Sigmoid, new Sigmoid() },
            {Type.TanH, new TanH() },
            {Type.ReLU, new ReLU() },
            {Type.LeakyReLU, new LeakyReLU() },
            {Type.ParameterisedReLU, new ParameterisedReLU() },
            {Type.ExponentialLinearUnit, new ExponentialLinearUnit() },
            {Type.Swish, new Swish() },
            {Type.Softmax, new Softmax() }
        };
        Type type;
        IActivation function;
        public ActivationFunction(Type type)
        {
            this.type = type;
            function = typeDict[type];
        }
        public double[] Activate(double[] threshold)
        {
            double[] activation = new double[threshold.Length];
            for (int i = 0; i < activation.Length; i++)
                activation[i] = function.Activate(threshold[i]);
            return activation;
        }
        public double[] Gradient(double[] d)
        {
            return d;
        }

        public override string ToString()
        {
            return type.ToString();
        }
    }
}
