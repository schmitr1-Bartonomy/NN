using NeuralNetwork.Activation;
using NeuralNetwork.Train.Cost;
using System;
using System.Collections.Generic;
using static NeuralNetwork.Train.Train;

namespace NeuralNetwork
{
    public class NeuralNetwork
    {
        ushort[] topology;
        Layer[] layers;
        ActivationFunction inputActivation;
        Train.Train train = new Train.Train();
        public NeuralNetwork()
        {
            Build(new ushort[] { 2,2, 1 }, new List<Activation.Type>() {Activation.Type.Linear,Activation.Type.ReLU, Activation.Type.Sigmoid });
        }
        public double Train(List<double[]> input, List<double[]> output)
        {
            List<Info> infos = new List<Info>();
            double cost = 0;
            for (int i = 0; i < input.Count; i++)
            {
                Data data = new Data()
                {
                    input = input[i],
                    output = output[i]
                };
                infos.Add(train.Training(this, data));
            }
            GradientDescent(infos);
            foreach (Info info in infos)
                cost += info.cost;
            return cost;
        }

        private void GradientDescent(List<Info> infos)
        {
            for (int i = layers.Length-1; i >= 0; i--)
            {
                layers[i].AdjustWeights(infos,i);
                layers[i].AdjustBiases(infos,i);
            }
        }

        private void Build(ushort[] topology, List<Activation.Type> activations)
        {
            this.topology = topology;
            layers = new Layer[topology.Length-1];
            inputActivation = new ActivationFunction(activations[0]);
            for (int i = 0; i < topology.Length-1; i++)
                layers[i] = new Layer(topology[i], topology[i+1], activations[i+1]);
        }

        internal Info BackPropgate(Info info, CostFunction costFunction, double[] target)
        {
            for (int i = info.errors.Length - 1; i > 0; i--)
            {
                info.errors[i] = i == info.errors.Length - 1 ?
                    layers[i - 1].BackPropogate(costFunction.Gradient(info.activations[i], target), info.thresholds[i]) :
                    layers[i - 1].BackPropogate(layers[i].weights, info.errors[i + 1], info.thresholds[i]);
            }
            foreach (double cost in costFunction.Cost(info.activations[info.activations.Length - 1], target))
                info.cost += cost;
            return info; 
        }
        public double[] FeedForward(double[] inputs)
        {
            inputs = inputActivation.Activate(inputs);
            foreach (Layer layer in layers)
                inputs = layer.FeedForward(inputs);
            return inputs;
        }
        internal Info FeedForwardInfo(double[] inputs)
        {
            Info info = new Info(topology);
            info.thresholds[0] = inputs;
            info.activations[0] = inputActivation.Activate(inputs);
            for (int i = 0; i < layers.Length; i++)
                info = layers[i].FeedForward(info, i);
            return info;
        }
        internal struct Info
        {
            internal double[][] thresholds;
            internal double[][] activations;
            internal double[][] errors;
            internal double cost;
            internal Info(ushort[] topology)
            {
                thresholds = new double[topology.Length][];
                activations = new double[topology.Length][];
                errors = new double[topology.Length][];
                cost = 0;
            }
        }

        public override string ToString()
        {
            string nnString = " ---------------- Neural Network ----------------\n" +
                $"Topology: {string.Join(" ,", topology)}\n";
            foreach (Layer layer in layers)
                nnString += layer.ToString();
            return nnString;
        }
    }
}
