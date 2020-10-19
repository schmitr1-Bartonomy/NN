using NeuralNetwork.Activation;
using System;
using System.Collections.Generic;

namespace NeuralNetwork
{
    public class NeuralNetwork
    {
        ushort[] topology;
        Layer[] layers;
        ActivationFunction inputActivation;
        public NeuralNetwork()
        {
            Build(new ushort[] { 2, 4, 5, 1 }, new List<Activation.Type>() { Activation.Type.Linear, Activation.Type.ReLU, Activation.Type.ReLU, Activation.Type.Sigmoid });
        }
        private void Build(ushort[] topology, List<Activation.Type> activations)
        {
            this.topology = topology;
            layers = new Layer[topology.Length-1];
            inputActivation = new ActivationFunction(activations[0]);
            for (int i = 0; i < topology.Length-1; i++)
                layers[i] = new Layer(topology[i], topology[i+1], activations[i+1]);
        }
        public double[] FeedForward(double[] inputs)
        {
            foreach (Layer layer in layers)
                inputs = layer.FeedForward(inputs);
            return inputs;
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
