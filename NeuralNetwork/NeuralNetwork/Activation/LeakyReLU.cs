﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork.Activation
{
    class LeakyReLU : IActivation
    {
        public double Activate(double x)
        {
            return x < 0 ? 0.01 * x : x;
        }

        public double Gradient(double x)
        {
            return x < 0 ? 0.01 : 1;
        }
    }
}
