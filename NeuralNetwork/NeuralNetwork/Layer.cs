using NeuralNetwork.Activation;
using NeuralNetwork.Train.Cost;
using System;
using System.Collections.Generic;
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

        internal void AdjustBiases(List<Info> infos, int index)
        {
            double learningRate = .1;
            double[] sum = new double[bias.Length];
            for (int i = 0; i < infos.Count; i++)
                sum = Plus(sum, infos[i].errors[index + 1]);
            bias = Plus(bias, Multiply(-learningRate / infos.Count, sum));
            //Console.WriteLine(string.Join(" ,",bias));
        }

        internal void AdjustWeights(List<Info> infos,int index)
        {
            double learningRate = .1;
            double[,] sum = new double[weights.GetLength(0),weights.GetLength(1)];
            for (int i = 0; i < infos.Count; i++)
            {
                Console.WriteLine($"({infos[i].errors[index + 1].GetLength(0)},1),(1,{infos[i].activations[index].GetLength(0)})");

                sum = Plus(sum, Multiply(infos[i].errors[index + 1], Transpose(infos[i].activations[index])));
            }
            weights = Plus(weights,Multiply(-learningRate / infos.Count, sum));
            for (int i = 0; i < sum.GetLength(0); i++)
                for (int j = 0; j < sum.GetLength(1); j++)
                    continue;//Console.WriteLine(weights[i, j]);
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

        internal double[] Multiply(double x, double[] y)
        {
            double[] ret = new double[y.GetLength(0)];
            for (int i = 0; i < ret.GetLength(0); i++)
                    ret[i] += x * y[i];
            return ret;
        }

        internal double[,] Multiply(double x, double[,] y)
        {
            double[,] ret = new double[y.GetLength(0), y.GetLength(1)];
            for (int i = 0; i < ret.GetLength(0); i++)
                for (int j = 0; j < ret.GetLength(1); j++)
                    ret[i, j] += x * y[i, j];
            return ret;
        }

        internal double[] Multiply(double[,] x, double[] y)
        {
            double[] ret = new double[x.GetLength(0)];
            for (int i = 0; i < ret.Length; i++)
                for (int k = 0; k < y.GetLength(0); k++)
                    ret[i] += x[i, k] * y[k];
            return ret;
        }

        internal double[,] Multiply(double[] x, double[,] y)
        {
            double[,] ret = new double[1,y.GetLength(1)];
            for (int i = 0; i < ret.GetLength(0); i++)
                for (int j = 0; j < ret.GetLength(1); j++)
                    for (int k = 0; k < y.GetLength(0); k++)
                        ret[i,j] += x[i] * y[k,j];
            return ret;
        }

        internal double[] Plus(double[] x, double[] y)
        {
            double[] ret = new double[x.Length];
            for (int i = 0; i < ret.Length; i++)
                ret[i] = x[i] + y[i];
            return ret;
        }

        internal double[,] Plus(double[,] x, double[,] y)
        {
            double[,] ret = new double[x.GetLength(0), x.GetLength(1)];
            for (int i = 0; i < ret.GetLength(0); i++)
                for (int j = 0; j < ret.GetLength(1); j++)
                    ret[i, j] = x[i, j] + y[i, j];
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

        internal double[,] Transpose(double[] x)
        {
            double[,] ret = new double[1, x.GetLength(0)];
            for (int j = 0; j < ret.GetLength(1); j++)
                ret[0, j] = x[j];
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
