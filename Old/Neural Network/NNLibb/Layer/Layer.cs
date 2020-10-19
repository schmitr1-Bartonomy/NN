using System;
using static NNLib.Layer.Activation.Activation;

namespace NNLib.Layer
{
    enum LayerType
    {
        INPUT = 1,
        HIDDEN = 2,
        OUTPUT = 3
    }
    internal class Layer
    {
        int[] TOPOLOGY;
        int INDEX;
        Activation.Activation ACTIVATION;
        LayerType TYPE;

        double[] INPUTS;
        double[,] WEIGHTS;
        double BIAS;
        double[] THRESHOLD;
        double[] OUTPUTS;
        internal Layer(int[] topology, int index, ActivationType activation = ActivationType.DEFAULT)
        {
            TOPOLOGY = topology;
            INDEX = index;
            if (INDEX < 0 || INDEX > TOPOLOGY.Length - 1)
            {
                throw new Exception();
            }
            TYPE = INDEX == 0 ? LayerType.INPUT : INDEX == TOPOLOGY.Length - 1 ? LayerType.OUTPUT : LayerType.HIDDEN;
            Build(activation);
        }
        internal void Build(ActivationType type)
        {
            if (TYPE == LayerType.INPUT)
            {
                INPUTS = new double[TOPOLOGY[INDEX]];
                WEIGHTS = new double[TOPOLOGY[INDEX], TOPOLOGY[INDEX]];
                ACTIVATION = type == ActivationType.DEFAULT ?
                    new Activation.Activation(ActivationType.IDENTITY) :
                    new Activation.Activation(type);
                Populate();
            }
            else
            {
                INPUTS = new double[TOPOLOGY[INDEX - 1]];
                WEIGHTS = new double[TOPOLOGY[INDEX - 1], TOPOLOGY[INDEX]];
                ACTIVATION = type == ActivationType.DEFAULT ?
                    TYPE == LayerType.HIDDEN?
                    new Activation.Activation(ActivationType.RELU) :
                    new Activation.Activation(ActivationType.SIGMOID) :
                    new Activation.Activation(type);
                Populate(new Random());
            }
            THRESHOLD = new double[TOPOLOGY[INDEX]];
            OUTPUTS = new double[TOPOLOGY[INDEX]];
        }

        internal void Populate()
        {
            if (TYPE != LayerType.INPUT)
                throw new Exception();
            for (int i = 0; i < WEIGHTS.GetLength(0); i++)
                WEIGHTS[i, i] = 1;
            BIAS = 0;
        }
        internal void Populate(Random random)
        {
            for (int i = 0; i < WEIGHTS.GetLength(0); i++)
                for (int j = 0; j < WEIGHTS.GetLength(1); j++)
                    WEIGHTS[i, j] = 2 * random.NextDouble() - 1;
            BIAS = 2 * random.NextDouble() - 1;
        }
        internal double[] FeedForward(double[] inputs)
        {
            if (inputs.Length != INPUTS.Length)
                throw new Exception();
            INPUTS = inputs;
            if (TYPE == LayerType.INPUT)
                THRESHOLD = INPUTS;
            else
            {
                for (int i = 0; i < THRESHOLD.Length; i++)
                {
                    THRESHOLD[i] = BIAS;
                    for (int j = 0; j < INPUTS.Length; j++)
                        THRESHOLD[i] += INPUTS[j] * WEIGHTS[j, i];
                }
            }
            return Activate();
        }
        internal double[] Activate() {
            for (int i = 0; i < THRESHOLD.Length; i++)
                OUTPUTS[i] = ACTIVATION.Activate(THRESHOLD[i]);
            return OUTPUTS;
        }

        internal double[] BackPropogate(double[] outputs)
        {
            if (outputs.Length != OUTPUTS.Length)
                throw new Exception();
            return INPUTS;
        }
        public override string ToString()
        {
            return $"\n -------- Layer --------\n" +
                $"Index: {INDEX}\n" +
                $"Type: {TYPE}\n" +
                $"Activation: {ACTIVATION.TYPE}\n" +
                $"Inputs:\n" +
                $"{StringBuilder1D(INPUTS)}\n" +
                $"Weights:\n" +
                $"{StringBuilder2D(WEIGHTS)}\n" +
                $"Bias:\n" +
                $"{string.Format("{0:0.0000}",BIAS),10}\n" +
                $"Threshold:\n" +
                $"{StringBuilder1D(THRESHOLD)}\n" +
                $"Outputs:\n" +
                $"{StringBuilder1D(OUTPUTS)}";
        }
        private string StringBuilder1D(double[] array)
        {
            string[] line = new string[array.GetLength(0)];
            for (int i = 0; i < line.Length; i++)
                line[i] = string.Format("{0:0.0000}", array[i]).PadLeft(10, ' ');
            return string.Join("\t", line);
        }
        private string StringBuilder2D(double[,] array)
        {
            string[] line = new string[array.GetLength(0)];
            for (int i = 0; i < line.Length; i++)
            {
                string[] row = new string[array.GetLength(1)];
                for (int j = 0; j < row.Length; j++)
                    row[j] = string.Format("{0:0.0000}", array[i, j]).PadLeft(10,' ');
                line[i] = string.Join("\t", row);
            }
            return string.Join("\n", line);
        }
    }
}
