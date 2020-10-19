using MatrixLib;
using NeuralNetworkLib.ActivationFunctions;
using System;
using System.Collections.Generic;

namespace NeuralNetworkLib
{
    class Activation
    {
        ActivationType TYPE;
        public enum ActivationType
        {
            // https://en.wikipedia.org/wiki/Activation_function
            DEFAULT = 0,
            IDENTITY = 1,
            BINARY = 2,
            SIGMOID = 3,
            TANH = 4,
            RELU = 5,
            GELU = 6,
            SOFTPLUS = 7,
            ELU = 8,
            SELU = 9,
            LEAKYRELU = 10,
            PRELU = 11,
            ARCTAN = 12,
            SOFTSIGN = 13,
            SQNL = 14,
            SRELU = 15,
            BENT = 16,
            SILU = 17,
            SINUSOID = 18,
            SINC = 19,
            GAUSSIAN = 20,
            SQRBF = 21,
            SOFTMAX = 22,
            MAXOUT = 23,
            CUSTOM = -1
        }
        IActivation function;
        public Activation(ActivationType type)
        {
            TYPE = type;
            function = activationFunctions[type];
        }
        public Matrix Activate(Matrix x)
        {
            if (!x.IsVector())
                throw new Exception();
            Matrix ret = new Matrix(x.GetRowCount(), x.GetColumnCount());
            for (int i = 0; i < ret.GetRowCount(); i++)
                ret[i, 0] = function.Activate(x[i, 0]);
            return ret;
        }
        public Matrix Gradient(Matrix x)
        {
            if (!x.IsVector())
                throw new Exception();
            Matrix ret = new Matrix(x.GetRowCount(), x.GetColumnCount());
            for (int i = 0; i < ret.GetRowCount(); i++)
                ret[i, 0] = function.Gradient(x[i, 0]);
            return ret;
        }
        Dictionary<ActivationType, IActivation> activationFunctions = new Dictionary<ActivationType, IActivation>()
        {
            {ActivationType.DEFAULT, new Identity() },
            {ActivationType.IDENTITY, new Identity() },
            {ActivationType.BINARY, new BinaryStep() },
            {ActivationType.SIGMOID, new Sigmoid() },
            {ActivationType.TANH, new TanH() },
            {ActivationType.RELU, new ReLU() },
            {ActivationType.GELU, new GELU() },
            {ActivationType.SOFTPLUS, new SoftPlus() },
            {ActivationType.ELU, new ELU() },
            {ActivationType.SELU, new SELU() },
            {ActivationType.LEAKYRELU, new LeakyReLU() },
            {ActivationType.PRELU, new PReLU() },
            {ActivationType.ARCTAN, new ArcTan() },
            {ActivationType.SOFTSIGN, new Softsign() },
            {ActivationType.SQNL, new SQNL() },
            {ActivationType.SRELU, new SReLU() },
            {ActivationType.BENT, new BentIdentity()},
            {ActivationType.SILU, new SiLU() },
            {ActivationType.SINUSOID, new Sinusoid() },
            {ActivationType.SINC, new Sinc() },
            {ActivationType.GAUSSIAN, new Gaussian()},
            {ActivationType.SQRBF, new SQ_RBF() },
            {ActivationType.SOFTMAX, new Softmax() },
            {ActivationType.MAXOUT, new Maxout() }
        };

        public override string ToString()
        {
            return TYPE.ToString();
        }
    }
}
