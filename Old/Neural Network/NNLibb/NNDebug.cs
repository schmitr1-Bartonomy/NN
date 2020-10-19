using System;
using System.Collections.Generic;
using System.Text;

namespace NNLib
{
    public class NNDebug
    {
        public NNDebug()
        {
            NN nn = new NN(new int[] { 2, 3, 1 });
            double[,] inputs = new double[4, 2]
            {
                {0,0 },
                {0,1 },
                {1,0 },
                {1,1 }
            };
            double[,] outputs = new double[4, 1]
            {
                {0 },
                {0 },
                {1 },
                {1 }
            };
            Console.WriteLine(nn);
            new Train(nn, inputs, outputs);
        }
        public NNDebug(NNDebug_Type type)
        {
            switch (type)
            {
                case 0: break;
            }
        }
    }
    public enum NNDebug_Type
    {
        TESTCASE = 0,


    }
}
