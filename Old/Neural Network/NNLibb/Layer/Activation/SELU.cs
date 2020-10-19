﻿using NNLib.Layer.Activiation;
using System;
using System.Collections.Generic;
using System.Text;

namespace NNLib.Layer.Activation
{
    class SELU : IActivation
    {
        double lambda = 1.0507;
        double a = 1.67326;
        public double Activate(double x)
        {
            return lambda * new ELU(a).Activate(x);
        }

        public double Gradient(double x)
        {
            return lambda * (x < 0 ? a * Math.Exp(x) : 1);
        }
    }
}
