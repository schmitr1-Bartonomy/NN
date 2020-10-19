using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NNLib
{
    class NNException : Exception
    {
        public NNException(string[] message) : base($"WARNING - ({new StackTrace().GetFrame(2).GetMethod().Name}:\n\t" + string.Join("\n\t", message)) { }
    }
}
