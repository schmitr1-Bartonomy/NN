using System;
using System.Linq;

namespace NNLib
{
    public class NN
    {
        int[] TOPOLOGY;
        int DEPTH;
        Layer.Layer[] LAYERS;
        public NN(int[] topology)
        {
            if (topology == null)
            {
                throw new Exception();
            }
            if (topology.Length < 2)
            {
                throw new Exception();
            }
            TOPOLOGY = topology;
            DEPTH = topology.Length;
            Build();
        }

        internal void Build()
        {
            LAYERS = new Layer.Layer[DEPTH];
            foreach (int i in Enumerable.Range(0,DEPTH))
            {
                LAYERS[i] = new Layer.Layer(TOPOLOGY, i);
            }
        }
        internal double[] FeedForward(double[] inputs)
        {
            for (int i = 0; i < TOPOLOGY.Length; i++)
                inputs =  LAYERS[i].FeedForward(inputs);
            return inputs;
        }

        internal void BackPropogate(double[] output)
        {
            //LAYERS[DEPTH - 1].BackPropogate();
            for (int i = DEPTH-1; i >= 0; i--)
                output = LAYERS[i].BackPropogate(output);
        }

        public override string ToString()
        {
            string[] layers = new string[LAYERS.Length];
            for (int i = 0; i < LAYERS.Length; i++)
            {
                layers[i] = LAYERS[i].ToString();
            }
            return string.Join("\n", layers);
        }
    }
}
