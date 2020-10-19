using System;
using NeuralNetwork;

namespace TestBed
{
    class Program
    {
        static void Main(string[] args)
        {
            NeuralNetwork.NeuralNetwork nn = new NeuralNetwork.NeuralNetwork();
            Console.WriteLine(nn);
            Console.WriteLine(string.Join(" ,",nn.FeedForward(new double[] { 2, 2 })));

        }
    }
}
