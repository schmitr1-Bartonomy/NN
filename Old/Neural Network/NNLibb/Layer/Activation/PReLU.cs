﻿using NNLib.Layer.Activiation;
using System;
using System.Collections.Generic;
using System.Text;

namespace NNLib.Layer.Activation
{
    class PReLU : IActivation
    {
        double a;
        public PReLU(double a = 0.5)
        {
            this.a = a;
        }
        public double Activate(double x)
        {
            return x < 0 ? a * x : x;
        }

        public double Gradient(double x)
        {
            return x < 0 ? a : 1;
        }
    }
}
