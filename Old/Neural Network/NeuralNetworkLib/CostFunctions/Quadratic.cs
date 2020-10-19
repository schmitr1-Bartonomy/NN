using NeuralNetworkLib.CostFunctions;
using System;

namespace NeuralNetworkLib.CostFunctions
{
    internal class Quadratic : ICost
    {
        public double Cost(double prediction, double target)
        {
            return 0.5 * Math.Pow(prediction - target,2);
        }

        public double dCost(double prediction, double target)
        {
            return prediction - target;
        }
    }
}