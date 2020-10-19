using System;
using System.Collections.Generic;
using System.Linq;
using MatrixLib;
using NeuralNetworkLib;

namespace NNTrainer
{
    public class Train
    {
        public Train(NeuralNetwork nn)
        {
            List<List<double>> inputs = new List<List<double>>()
            {
                new List<double>() { 0 , 0 },
                new List<double>() { 0 , 1 },
                new List<double>() { 1 , 0 },
                new List<double>() { 1 , 1 }
            };
            List<List<double>> outputs = new List<List<double>>()
            {
                new List<double>() { 0 },
                new List<double>() { 1 },
                new List<double>() { 0 },
                new List<double>() { 1 }
            };

            for (int i = 0; i < inputs.Count; i++)
            {
                nn.Train(inputs[i], outputs[i]);
            }
        }
        public void Batch(IEnumerable<IEnumerable<double>> batch)
        {
            //foreach(IEnumerable<double>)
        }
    }
}