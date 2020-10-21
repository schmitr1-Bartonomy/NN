using NeuralNetwork.Train.Cost;
using System;
using System.Collections.Generic;
using System.Text;
using static NeuralNetwork.NeuralNetwork;

namespace NeuralNetwork.Train
{
    class Train
    {
        CostFunction costFunction;
        struct Data
        {
            public double[] input;
            public double[] output;
        }
        void Training(NeuralNetwork nn, Data data)
        {
            Info info = nn.FeedForwardInfo(data.input);
            //costFunction.Cost(info.activations[info.activations.Length - 1], data.output);
            nn.BackPropgate(info, costFunction, data.output);
        }
    }
}
