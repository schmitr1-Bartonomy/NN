using NeuralNetworkLib;
using System;
using System.Collections.Generic;
using NNTrain;
using System.IO;
using System.Linq;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            M();
            //var testImages = IDXFileParser<byte>("C:\\MNist\\t10k-images.idx3-ubyte");
            //var testLabels = IDXFileParser<byte>("C:\\MNist\\t10k-labels.idx1-ubyte");
            //List<List<double>> testInputs = new List<List<double>>();
            //List<List<double>> testOutputs = new List<List<double>>();
            //foreach (var pair in testImages.Zip(testLabels))
            //{
            //    DigitImage _ = new DigitImage(pair);
            //    testInputs.Add(_.GetPixelInput());
            //    testOutputs.Add(_.GetLabelOutput());
            //}
            //NeuralNetwork nn = new NeuralNetwork(new List<int> { 28 * 28, 300,200, 100, 50, 10 });
            //Console.WriteLine(string.Join(" ,", nn.Run(testInputs[0])));
        }
        static void M() { 

            // Get Data
            var images = IDXFileParser<byte>("C:\\MNist\\train-images.idx3-ubyte");
            var labels = IDXFileParser<byte>("C:\\MNist\\train-labels.idx1-ubyte");
            var testImages = IDXFileParser<byte>("C:\\MNist\\t10k-images.idx3-ubyte");
            var testLabels = IDXFileParser<byte>("C:\\MNist\\t10k-labels.idx1-ubyte");
            // Process
            List<List<double>> inputs = new List<List<double>>();
            List<List<double>> outputs = new List<List<double>>();
            List<List<double>> testInputs = new List<List<double>>();
            List<List<double>> testOutputs = new List<List<double>>();
            foreach (var pair in images.Zip(labels))
            {
                DigitImage _ = new DigitImage(pair);
                inputs.Add(_.GetPixelInput());
                outputs.Add(_.GetLabelOutput());
            }
            foreach (var pair in testImages.Zip(testLabels))
            {
                DigitImage _ = new DigitImage(pair);
                testInputs.Add(_.GetPixelInput());
                testOutputs.Add(_.GetLabelOutput());
            }
            NeuralNetwork nn = new NeuralNetwork(new List<int>() { 28 * 28,200,50, 10 });
            NNTrainer.Train(nn,0.01,inputs,outputs,testInputs,testOutputs);
        }

        static Dictionary<byte, Tuple<object,int,Func<byte[],object>>> dataTypes = new Dictionary<byte, Tuple<object, int, Func<byte[],object>>>()
        {
            {0x08, Tuple.Create<object,int,Func<byte[],object>>(byte.MinValue,1,(byte[] b) => b[0])},
            {0x09, Tuple.Create<object,int,Func<byte[],object>>(sbyte.MinValue,1,(byte[] b) => (sbyte)b[0])},
            {0x0B, Tuple.Create<object,int,Func<byte[],object>>(short.MinValue,2,(byte[] b) => BitConverter.ToUInt16(b))},
            {0x0C, Tuple.Create<object,int,Func<byte[],object>>(int.MinValue,4,(byte[] b) => BitConverter.ToInt32(b))},
            {0x0D, Tuple.Create<object,int,Func<byte[],object>>(float.MinValue,4,(byte[] b) => BitConverter.ToSingle(b)) },
            {0x0E, Tuple.Create<object,int,Func<byte[],object>>(double.MinValue,8,(byte[] b) => BitConverter.ToDouble(b))},
        };
        private static List<object> IDXFileParser<T>(string fileName)
            where T : new()
        {
            if (!File.Exists(fileName))
                throw new Exception();
            FileStream stream = new FileStream(fileName, FileMode.Open);
            BinaryReader binaryReader = new BinaryReader(stream);
            byte[] magic = binaryReader.ReadBytes(4); // Get Magic Number
            if (typeof(T) != dataTypes[magic[2]].Item1.GetType()) // Check if types match
                throw new Exception();
            int dimension = Convert.ToInt32(magic[3]);
            List<int> sizes = new List<int>();
            for (int i = 0; i < dimension; i++)
            {
                byte[] size = binaryReader.ReadBytes(4);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(size);
                sizes.Add(BitConverter.ToInt32(size));
            }
            List<object> ret = new List<object>();
            for (int index = 0; index < sizes[0]; index++)
            {
                ret.Add(GetDataArray(sizes.GetRange(1, dimension - 1)));
            }
            stream.Close();
            binaryReader.Close();
            return ret;

            object ParseBytes(byte type, byte[] b)
            {
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(b);
                return dataTypes[type].Item3.Invoke(b); ;
            }

            object GetDataArray(List<int> arraySizes)
            {
                if (arraySizes.Count == 0)
                {
                    byte[] _data = binaryReader.ReadBytes(dataTypes[magic[2]].Item2);
                    T data = (T)ParseBytes(magic[2], _data);
                    return data;
                }
                List<object> dataArray = new List<object>();
                for (int i = 0; i < arraySizes[0]; i++)
                    dataArray.Add(GetDataArray(arraySizes.GetRange(1, arraySizes.Count - 1)));
                return dataArray;
            }
        }
    }
}
