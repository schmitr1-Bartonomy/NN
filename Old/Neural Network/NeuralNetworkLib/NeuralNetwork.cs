using MatrixLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static NeuralNetworkLib.Activation;

namespace NeuralNetworkLib
{
    public class NeuralNetwork
    {
        List<Layer> layers = new List<Layer>();
        List<int> topology;
        Cost costFunction;
        public NeuralNetwork(List<int> topology, Cost.CostType costType = Cost.CostType.DEFAULT)
        {
            this.topology = topology;
            costFunction = new Cost(costType);
            if (topology.Count() < 2)
                throw new Exception();
            if (topology.First() < 2)
                throw new Exception();
            Build(new Random());
        }

        private void Build(double? init = null)
        {
            for (int i = 1; i < topology.Count() - 1; i++)
                layers.Add(new Layer(topology[i - 1], topology[i], ActivationType.PRELU, init??0));
            layers.Add(new Layer(topology[topology.Count - 1], topology[topology.Count], ActivationType.SIGMOID, init??0));
        }

        private void Build(Random random)
        {
            for (int i = 1; i < topology.Count() - 1; i++)
                layers.Add(new Layer(topology[i - 1], topology[i], ActivationType.RELU, random));
            layers.Add(new Layer(topology[topology.Count-2], topology[topology.Count-1], ActivationType.SIGMOID, random));
        }

        public List<double> Run(List<double> input)
        {
            if (input.Count != layers[0].GetWidth())
                throw new Exception();
            Matrix inputMatrix = new Matrix(input);
            foreach (Layer layer in layers)
            {
                inputMatrix = layer.FeedForward(inputMatrix, out Matrix _);
            }
            return inputMatrix.ToList();
        }

        public Task<Tuple<List<Matrix>, List<Matrix>, double>> Train(List<double> input, List<double> output)
        {
            List<Matrix> activations = new List<Matrix>() { new Matrix(input) };
            List<Matrix> thresholds = new List<Matrix>() { new Matrix(input) };
            foreach (Layer layer in layers)
            {
                activations.Add(layer.FeedForward(activations.Last(), out Matrix threshold));
                thresholds.Add(threshold);
            }
            double cost = costFunction.CalcCost(activations.Last(), new Matrix(output));
            List<Matrix> errors = new List<Matrix>() { costFunction.dCost(activations.Last(), new Matrix(output)).Hadamard(layers.Last().activationFunction.Gradient(thresholds.Last())) };
            for (int i = layers.Count - 2; i >= 0; i--)
                errors.Add(layers[i].BackPropogate(layers[i + 1].GetWeights(), errors.Last(), thresholds[i + 1]));
            errors.Reverse();
            return Task.FromResult(new Tuple<List<Matrix>, List<Matrix>, double>(activations, errors, cost));
        }

        public void UpdateWeights(List<Tuple<List<Matrix>, List<Matrix>>> batchData, double lR_M)
        {
            for (int i = 0; i < layers.Count; i++)
            {
                Layer _ = layers[i];
                Matrix weightGradient = new Matrix(_.GetWeights().GetRowCount(), _.GetWeights().GetColumnCount());
                Matrix biasGradient = new Matrix(_.GetBias().GetRowCount(), _.GetBias().GetColumnCount());
                foreach (Tuple<List<Matrix>, List<Matrix>> data in batchData)
                {
                    weightGradient += data.Item2[i] * (data.Item1[i]).Transpose();
                    biasGradient += data.Item2[i];
                }

                layers[i].Update(lR_M * weightGradient, lR_M * biasGradient);
            }
        }
        public override string ToString()
        {
            string str = " ---------------- Neural Network ---------------\n";
            foreach (Layer layer in layers)
                str += $" -------- Layer --------\n{layer}\n";
            return str;
        }
    }
}
