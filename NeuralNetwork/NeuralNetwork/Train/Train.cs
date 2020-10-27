using NeuralNetwork.Train.Cost;
using System;
using System.Collections.Generic;
using System.Text;
using static NeuralNetwork.NeuralNetwork;

namespace NeuralNetwork.Train
{
    internal class Train
    {
        internal Train()
        {

        }
        internal struct Data
        {
            public double[] input;
            public double[] output;
        }
        internal Info Training(NeuralNetwork nn, Data data, CostFunction costFunction = null)
        {
            if (costFunction == null)
                costFunction = new CostFunction();
            Info info = nn.FeedForwardInfo(data.input);
            return nn.BackPropgate(info, costFunction, data.output);
        }
    }
}
