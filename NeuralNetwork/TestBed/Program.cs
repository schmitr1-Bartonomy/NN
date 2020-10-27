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
                new double[] { 0 },
                new double[] { 0 },
                new double[] { 1 }
            };
            for (int i = 0; i < 1; i++)
            {
                input.AddRange(input);
                output.AddRange(output);
            }
            for (int i = 0; i < 10000; i++)
            {
                double _ = nn.Train(input, output);
                if (!double.IsNaN(_))
                    Console.WriteLine("Cost:"+_);
            }
            Console.WriteLine(string.Join(" ,",nn.FeedForward(input[0])));
            Console.WriteLine(string.Join(" ,",nn.FeedForward(input[1])));
            Console.WriteLine(string.Join(" ,",nn.FeedForward(input[2])));
            Console.WriteLine(string.Join(" ,",nn.FeedForward(input[3])));

        }
    }
}
