using NNLib.Layer.Activiation;
using System;
using System.Collections.Generic;
using System.Text;

namespace NNLib.Layer.Activation
{
    class SReLU : IActivation
    {
        double tl = -1;
        double al = 0.2;
        double tr = 1;
        double ar = 0.2;
        public double Activate(double x)
        {
            return x <= tl ? tl + al * (x - tl) :
                x < tr ? x :
                tr + ar * (x - tr);
        }

        public double Gradient(double x)
        {
            return x <= tl ? al :
                x < tr ? 1 :
                ar;
        }
    }
}
