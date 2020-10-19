using MatrixLib;
using NeuralNetworkLib.CostFunctions;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetworkLib
{
    public class Cost
    {
        CostType TYPE;
        public enum CostType
        {
            DEFAULT = 0,
            QUADRATIC = 1
        }
        ICost costFunction;
        internal Cost(CostType type)
        {
            TYPE = type;
            costFunction = costFunctions[type];

        }
        internal double CalcCost(double prediction, double target)
        {
            return costFunction.Cost(prediction, target);
        }
        internal double CalcCost(Matrix prediction, Matrix target)
        {
            if (!prediction.IsVector() || !target.IsVector() || prediction.GetColumnCount() != target.GetColumnCount())
                throw new Exception();
            double cost = 0;
            for (int j = 0; j < prediction.GetColumnCount(); j++)
                cost += CalcCost(prediction[0, j], target[0, j]);
            return cost;
        }
        internal double dCost(double prediction, double target)
        {
            return costFunction.dCost(prediction, target);
        }
        internal Matrix dCost(Matrix prediction, Matrix target)
        {
            if (!prediction.IsVector() || !target.IsVector() || prediction.GetColumnCount() != target.GetColumnCount())
                throw new Exception();
            Matrix ret = new Matrix(prediction.GetRowCount(),1);
            for (int i = 0; i < ret.GetRowCount(); i++)
                ret[i,0] = dCost(prediction[i,0], target[i,0]);
            return ret;
        }

        Dictionary<CostType, ICost> costFunctions = new Dictionary<CostType, ICost>() {
            { CostType.DEFAULT, new Quadratic() },
            { CostType.QUADRATIC, new Quadratic() }
        };
    }
}
