using System;
using System.Collections.Generic;
using System.Text;

namespace NNLib.Layer.Activiation
{
    internal interface IActivation
    {
        double Activate(double x);
        double Gradient(double x);
    }
}
