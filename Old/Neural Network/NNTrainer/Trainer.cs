using MatrixLib;
using NeuralNetworkLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNTrain
{
    public static class NNTrainer
    {
        public static async void Train(NeuralNetwork nn, double learningRate, List<List<double>> inputs, List<List<double>> outputs, List<List<double>> testInputs, List<List<double>> testOutputs)
        {
            await Epoch(nn, learningRate, inputs, outputs, testInputs, testOutputs);
        }
        public async static Task<double> Epoch(NeuralNetwork nn, double learningRate, List<List<double>> inputs, List<List<double>> outputs, List<List<double>> testInputs,List<List<double>> testOutputs)
        {
            Random r = new Random();
            var inputsE = SplitList(inputs, 1000);
            var outputsE = SplitList(outputs, 1000);
            foreach (var pair in inputsE.Zip(outputsE))
            {
                double cost = await BatchAsync(nn, pair.First, pair.Second, learningRate, 100);
                int i = r.Next(testInputs.Count-20);
                Console.WriteLine(BatchAsync(nn, testInputs.GetRange(i, 10), testOutputs.GetRange(i, 10), learningRate));
                Console.WriteLine("Computed: " + string.Join(" ,",nn.Run(testInputs[i])));
                Console.WriteLine("Actual: " + string.Join(" ,", testOutputs[i]));
            }
            return 0;
        }
        public static async Task<double> BatchAsync(NeuralNetwork nn,List<List<double>> inputs, List<List<double>> outputs, double learningRate, int? mini = null)
        {
            if (inputs.Count != outputs.Count)
                throw new Exception();
            if (mini == null)
            {
                List<Task<Tuple<List<Matrix>, List<Matrix>,double>>> batchList = new List<Task<Tuple<List<Matrix>, List<Matrix>,double>>>();
                foreach (var pair in inputs.Zip(outputs))
                    batchList.Add(nn.Train(pair.First, pair.Second));
                Tuple<List<Matrix>,List<Matrix>,double>[] results = await Task.WhenAll(batchList);
                List<Tuple<List<Matrix>, List<Matrix>>> batchData = new List<Tuple<List<Matrix>, List<Matrix>>>();
                double cost = 0;
                foreach (Tuple<List<Matrix>,List<Matrix>,double> tuple in results)
                {
                    batchData.Add(new Tuple<List<Matrix>, List<Matrix>>(tuple.Item1, tuple.Item2));
                    cost += tuple.Item3;
                }
                nn.UpdateWeights(batchData, learningRate);
                Console.WriteLine(cost);
                return cost / batchData.Count;
            }
            if (mini < 1)
                throw new Exception();
            var inputsB = SplitList(inputs, (int)mini);
            var outputsB = SplitList(outputs, (int)mini);
            double costB = 0;
            foreach (var pair in inputsB.Zip(outputsB))
            {
                costB = await BatchAsync(nn, pair.First, pair.Second, learningRate);
                if (costB < 0.01) break;
            }
            return costB;
        }
        public static IEnumerable<List<T>> SplitList<T>(List<T> locations, int nSize = 30)
        {
            for (int i = 0; i < locations.Count; i += nSize)
                yield return locations.GetRange(i, Math.Min(nSize, locations.Count - i));
        }
    }
}
