using System;
using System.Collections.Generic;

namespace TestBed
{
    class Program
    {
        static void Main(string[] args)
        {
            NeuralNetwork.NeuralNetwork nn = new NeuralNetwork.NeuralNetwork();
            List<double[]> input = new List<double[]>() {
                new double[] { 0, 0 },
                new double[] { 0, 1 },
                new double[] { 1, 0 },
                new double[] { 1, 1 }
            };
            List<double[]> output = new List<double[]>() {
                new double[] { 0 },
                new double[] { 1 },
                new double[] { 0 },
                new double[] { 1 }
            };
            for (int i = 0; i < 100000; i++)
            {
                nn.Train(input, output);
            }
        }
    }
}
