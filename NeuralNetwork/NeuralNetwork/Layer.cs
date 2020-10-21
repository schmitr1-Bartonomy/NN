﻿using NeuralNetwork.Activation;
using NeuralNetwork.Train.Cost;
using System;
using static NeuralNetwork.NeuralNetwork;

namespace NeuralNetwork
{
    class Layer
    {
        internal double[,] weights;
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
            return activationFunction.Activate(Plus(Multiply(weights, inputs), bias));
        }

        internal Info FeedForward(Info info, int depth)
        {
            info.thresholds[depth + 1] = Plus(Multiply(weights, info.activations[depth]), bias);
            info.activations[depth + 1] = activationFunction.Activate(info.thresholds[depth + 1]);
            return info;
        }
        internal double[] BackPropogate(double[] costGradient, double[] thresholds)
        {
            return Hadamard(costGradient, activationFunction.Gradient(thresholds));
        }
        internal double[] BackPropogate(double[,] weights_l, double[] errors_l, double[] thresholds)
        {
            return Hadamard(Multiply(Transpose(weights_l),errors_l), activationFunction.Gradient(thresholds));
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

        internal double[] Hadamard(double[] x, double[] y)
        {
            double[] ret = new double[x.Length];
            for (int i = 0; i < ret.Length; i++)
                ret[i] = x[i] * y[i];
            return ret;
        }
        
        internal double[,] Transpose(double[,] x)
        {
            double[,] ret = new double[x.GetLength(1), x.GetLength(0)];
            for (int i = 0; i < ret.GetLength(0); i++)
                for (int j = 0; j < ret.GetLength(1); j++)
                    ret[i, j] = x[j, i];
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
