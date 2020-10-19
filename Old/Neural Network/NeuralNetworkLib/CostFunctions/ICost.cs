using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetworkLib.CostFunctions
{
    interface ICost
    {
        double Cost(double prediction, double target);
        double dCost(double predicition, double target);
    }
}
