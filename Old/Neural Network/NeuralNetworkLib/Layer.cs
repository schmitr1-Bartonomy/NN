using MatrixLib;
using System;
using System.Collections.Generic;
using static NeuralNetworkLib.Activation;

namespace NeuralNetworkLib
{
    class Layer
    {
        Matrix weights;
        Matrix bias;
        internal Activation activationFunction;
        internal Layer(int inputWidth, int width, ActivationType activationType, double? init=null)
        {
            activationFunction = new Activation(activationType);
            Build(inputWidth, width, init??0);
        }
        internal Layer(int inputWidth, int width, ActivationType activationType, Random random)
        {
            activationFunction = new Activation(activationType);
            Build(inputWidth, width, random);
        }
        private void Build(int i, int w, double init)
        {
            if (i < 1 || w < 1)
                throw new Exception();
            weights = new Matrix(w, i, init);
            bias = new Matrix(w, 1, init);
        }
        private void Build(int i, int w, Random random)
        {
            if (i < 1 || w < 1)
                throw new Exception();
            weights = new Matrix(w,i, random,2,-1);
            bias = new Matrix(w,1, random,2,-1);
        }
        internal int GetWidth()
        {
            return weights.GetColumnCount();
        }
        public Matrix GetWeights()
        {
            return weights;
        }
        public Matrix GetBias()
        {
            return bias;
        }

        internal Matrix FeedForward(Matrix input, out Matrix threshold)
        {
            threshold = weights * input + bias;
            return activationFunction.Activate(threshold);
        }

        internal Matrix BackPropogate(Matrix weightL, Matrix errorL, Matrix threshold)
        {
            return (weightL.Transpose() * errorL).Hadamard(activationFunction.Gradient(threshold));
        }

        internal void Update(Matrix weightGradient, Matrix biasGradient)
        {
            weights -= weightGradient;
            bias -= biasGradient;
        }

        //internal Layer(int inputs, int width, ActivationType activationFunction = ActivationType.DEFAULT)
        //{
        //    weights = new Matrix(width, inputs);
        //    bias = new Matrix(width, 1);
        //    this.activationFunction = new Activation(activationFunction);
        //}
        //}
        //public Matrix FeedForward(Matrix input)
        //{
        //    threshold = weights * input + bias;
        //    activated = activationFunction.Activate(threshold);
        //    return activated;
        //}
        //public Matrix BackPropogate(Matrix prediction, Matrix target, Cost costFunction)
        //{
        //    err = costFunction.dCost(prediction, target) * activationFunction.Gradient(threshold);
        //    return err;
        //}
        //public Matrix BackPropogate(Matrix w, Matrix error)
        //{
        //    err = (w.Transpose() * error) * activationFunction.Gradient(threshold);
        //    return err;
        //}

        public override string ToString()
        {
            return $"Activation Function: {activationFunction.ToString()}\n" +
                $"Weights:\n{weights}\n" +
                $"Biases:\n{bias}";
        }

        //internal void Update(double learningRate, Matrix activated)
        //{
        //    weights = weights + (err * activated.Transpose())* -learningRate;
        //    bias = bias + err * -learningRate;
        //}
    }
}
