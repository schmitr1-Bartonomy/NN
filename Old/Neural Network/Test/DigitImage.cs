using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    class DigitImage
    {
        byte[][] pixels;
        byte label;
        public DigitImage((object pixels, object label) pair)
        {
            label = (byte)pair.label;
            List<object> _i = (List<object>)pair.pixels;
            pixels = new byte[_i.Count][];
            for (int i = 0; i < _i.Count; i++)
            {
                List<object> _j = (List<object>)_i[i];
                pixels[i] = new byte[_j.Count];
                for (int j = 0; j < _j.Count; j++)
                {
                    pixels[i][j] = (byte)_j[j];
                }
            }
        }

        public override string ToString()
        {
            string s = $"LABEL: {label}\n";
            for (int i = 0; i < pixels.GetLength(0); ++i)
            {
                for (int j = 0; j < pixels[i].Length; ++j)
                {
                    if (pixels[i][j] == 0)
                        s += " "; // white
                    else if (pixels[i][j] == 255)
                        s += "O"; // black
                    else
                        s += "."; // gray
                }
                s += "\n";
            }
            return s;
        }

        internal List<double> GetLabelOutput()
        {
            switch (label)
            {
                case 0: return new List<double>() { 0.99, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01 };
                case 1: return new List<double>() { 0.01, 0.99, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01 };
                case 2: return new List<double>() { 0.01, 0.01, 0.99, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01 };
                case 3: return new List<double>() { 0.01, 0.01, 0.01, 0.99, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01 };
                case 4: return new List<double>() { 0.01, 0.01, 0.01, 0.01, 0.99, 0.01, 0.01, 0.01, 0.01, 0.01 };
                case 5: return new List<double>() { 0.01, 0.01, 0.01, 0.01, 0.01, 0.99, 0.01, 0.01, 0.01, 0.01 };
                case 6: return new List<double>() { 0.01, 0.01, 0.01, 0.01, 0.01, 0.01, 0.99, 0.01, 0.01, 0.01 };
                case 7: return new List<double>() { 0.01, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01, 0.99, 0.01, 0.01 };
                case 8: return new List<double>() { 0.01, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01, 0.99, 0.01 };
                case 9: return new List<double>() { 0.01, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01, 0.99 };
                default: return null;
            }

        }

        internal List<double> GetPixelInput()
        {
            List<double> ret = new List<double>();
            for (int i = 0; i < pixels.Length; i++)
                for (int j = 0; j < pixels.Length; j++)
                    ret.Add(Convert.ToDouble(pixels[i][j])/255);
            return ret;
        }
    }
}
