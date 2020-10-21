using NeuralNetwork.Activation;
using System;
using System.Collections.Generic;
using System.Text;
using static NeuralNetwork.NeuralNetwork;

namespace NeuralNetwork
{
    class Layer
    {
        double[,] weights;
        double[] bias;
        ActivationFunction activationFunction;
        public Layer(ushort numInputs, ushort numNeurons, Activation.Type activationType)
        {
            weights = new double[numNeurons, numInputs];
            bias = new double[numNeurons];
            Populate(new Random());
            activationFunction = new ActivationFunction(activationType);
        }
        void Populate(object value)
        {
            for (int i = 0; i < bias.Length; i++)
                for (int j = 0; j < weights.GetLength(1); j++)
                    switch (value.GetType().Name)
                    {
                        case "Random": 
                            Random r = (Random)value;
                            weights[i, j] = 2 * r.NextDouble() - 1;
                            bias[i] = 2 * r.NextDouble() - 1;
                            break;
                        case "double":
                            weights[i, j] = (double)value;
                            bias[i] = (double)value;
                            break;
                    }
        }

        internal double[] FeedForward(double[] inputs)
        {
            return Plus(Multiply(weights, inputs), bias);
        }

        internal Info FeedForward(Info info, int depth)
        {
            info.thresholds[depth + 1] = Plus(Multiply(weights, info.activations[depth]), bias);
            info.activations[depth+1] = activationFunction.
            return Plus(Multiply(weights, inputs[info[), bias);
        }

        internal double[] Multiply(double[,] x, double[] y)
        {
            double[] ret = new double[x.GetLength(0)];
            for (int i = 0; i < ret.Length; i++)
                for (int j = 0; j < y.GetLength(0); j++)
                    ret[i] += x[i,j] * y[j];
            return ret; 
        }

        internal double[] Plus(double[] x,double[] y)
        {
            double[] ret = new double[x.Length];
            for (int i = 0; i < ret.Length; i++)
                ret[i] = x[i] + y[i];
            return ret;
        }

        public override string ToString()
        {
            string ret = " -------- Layer --------\n" +
                $"Activation Function: {activationFunction.ToString()}\n" +
                $"Weights:\n";
            for (int i = 0; i < weights.GetLength(0); i++)
            {
                for (int j = 0; j < weights.GetLength(1); j++)
                    ret += string.Format("{00:0.000}", weights[i, j]).PadLeft(10,' ');
                ret += "\n";
            }
            ret += "Bias:\n";
            for (int i = 0; i < bias.Length; i++)
                ret += string.Format("{00:0.000}", bias[i]).PadLeft(10, ' ') + "\n";
            return ret;
        }
    }
}
