using System;
using System.Collections.Generic;
using System.Text;

namespace NNLib
{
    class Train
    {
        public Train(NN nn, double[,] trainingInputs, double[,] trainingOutputs)
        {
            //if (trainingInputs.GetLength(0) != trainingOutputs.GetLength(0))
            //    throw new Exception();
            //for (int i = 0; i < trainingInputs.GetLength(0); i++)
            //    nn.FeedForward(trainingInputs[i]);
            //    input[i] = trainingInputs[iter, i];
            //double[] output = new double[trainingOutputs.GetLength(1)];
            //for (int i = 0; i < output.GetLength(0); i++)
            //    input[i] = trainingOutputs[iter, i];
            //nn.FeedForward(input);
            //nn.BackPropogate(output);
        }
    }
}
