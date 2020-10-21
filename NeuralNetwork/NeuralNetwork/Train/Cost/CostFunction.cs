using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork.Train.Cost
{
    class CostFunction
    {
        public double[] Cost(double[] prediction, double[] target)
        {
            double[] cost = new double[prediction.Length];
            for (int i = 0; i < prediction.Length; i++)
                cost[i] = .5 * Math.Pow(prediction[i] - target[i], 2);
            return cost;
        }

        public double[] Gradient(double[] prediction, double[] target)
        {
            double[] gradient = new double[prediction.Length];
            for (int i = 0; i < prediction.Length; i++)
                gradient[i] = prediction[i] - target[i];
            return gradient;
        }
    }
}
