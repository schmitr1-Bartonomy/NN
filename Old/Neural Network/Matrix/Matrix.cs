using System;
using System.Collections.Generic;

namespace MatrixLib
{
    public class Matrix
    {
        double[,] values;
        int m; // rows
        int n; // columns

        // -- Constructors
        public Matrix(int m, int n, double? init = null)
        {
            this.m = m;
            this.n = n;
            Build(init ?? 0);
        }
        public Matrix(int m, int n, Random random, double range = 1, double offset = 0)
        {
            this.m = m;
            this.n = n;
            Build(random,range,offset);
        }
        public Matrix(double[,] values)
        {
            m = values.GetLength(0);
            n = values.GetLength(1);
            this.values = values;
        }
        public Matrix(List<double> vector)
        {
            m = vector.Count;
            n = 1;
            Build(vector);
        }
        private void Build(double init)
        {
            values = new double[m, n];
            if (init != 0)
                for (int i = 0; i < m; i++)
                    for (int j = 0; j < n; j++)
                        values[i, j] = init;
        }
        private void Build(Random random,double range,double offset)
        {
            values = new double[m, n];
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    values[i, j] = range*random.NextDouble()+offset;
        }
        private void Build(List<double> vector)
        {
            values = new double[m,n];
            for (int i = 0; i < m; i++)
                values[i, 0] = vector[i];
        }
        // -- Getters
        public int GetRowCount()
        {
            return m;
        }
        public int GetColumnCount()
        {
            return n;
        }
        // -- Operators
        public double this[int i, int j]
        {
            get { return values[i, j]; }
            set { values[i, j] = value; }
        }
        public static Matrix operator -(Matrix M1, Matrix M2)
        {
            return M1 + -1 * M2;
        }
        public static Matrix operator +(Matrix M1, Matrix M2)
        {
            if (M1.m != M2.m || M1.n != M2.n)
                throw new Exception();
            Matrix ret = new Matrix(M1.m, M2.n);
            for (int i = 0; i < ret.m; i++)
                for (int j = 0; j < ret.n; j++)
                    ret[i, j] = M1[i, j] + M2[i, j];
            return ret;
        }
        public static Matrix operator *(double c, Matrix M)
        {
            Matrix ret = M.Clone();
            for (int i = 0; i < ret.m; i++)
                for (int j = 0; j < ret.n; j++)
                    ret[i, j] *= c;
            return ret;
        }
        public static Matrix operator *(Matrix M1, Matrix M2)
        {
            if (M1.n != M2.m)
                throw new Exception();
            Matrix ret = new Matrix(M1.m, M2.n);
            for (int i = 0; i < ret.m; i++)
                for (int j = 0; j < ret.n; j++)
                {
                    ret[i, j] = 0;
                    for (int k = 0; k < M1.n; k++)
                        ret[i, j] += M1[i, k] * M2[k, j];
                }
            return ret;
        }
        // -- Functions
        public Matrix Clone()
        {
            return new Matrix(values);
        }
        public Matrix Transpose()
        {
            Matrix ret = new Matrix(n, m);
            for (int i = 0; i < ret.m; i++)
                for (int j = 0; j < ret.n; j++)
                    ret[i, j] = values[j, i];
            return ret;
        }
        public Matrix Hadamard(Matrix M)
        {
            if (m != M.m || M.n != 1)
                throw new Exception();
            Matrix ret = new Matrix(M.m, M.n);
            for (int i = 0; i < ret.m; i++)
                ret[i,0] = values[i,0] * M[i,0];
            return ret;
        }
        // -- Booleans
        public bool IsVector()
        {
            return n == 1;
        }
        //}
        //// -- Getters
        //public int GetRows()
        //{
        //    return m;
        //}
        //public int GetColumns()
        //{
        //    return n;
        //}
        //// -- Identity
        //public Matrix Identity(int size)
        //{
        //    Matrix ret = new Matrix(size, size);
        //    for (int i = 0; i < size; i++)
        //        ret[i, i] = 1;
        //    return ret;
        //}
        //// -- Get/Set Values
        //public double this[int i, int j]
        //    {
        //    get { return values[i, j]; }
        //    set { values[i, j] = value; }
        //    }
        //// -- Clone
        //// -- Transpose
        //// -- Addition
        //public static Matrix operator +(Matrix M1, Matrix M2)
        //{
        //    if (M1.m != M2.m || M1.n != M2.n)
        //        throw new Exception();
        //    Matrix ret = new Matrix(M1.m,M1.n);
        //    for (int i = 0; i < ret.m; i++)
        //        for (int j = 0; j < ret.n; j++)
        //            ret[i, j] = M1[i, j] + M2[i, j];
        //    return ret;
        //}
        //// -- Multiplication
        //public static Matrix operator *(Matrix M, double c)
        //{
        //    Matrix ret = new Matrix(M.m, M.n);
        //    for (int i = 0; i < ret.m; i++)
        //        for (int j = 0; j < ret.n; j++)
        //            ret[i, j] *= c;
        //    return ret;
        //}
        //public static Matrix operator *(Matrix M1, Matrix M2)
        //{
        //    if (M1.IsVector() && M2.IsVector() && M1.n == M2.n)
        //        return Hadamard(M1, M2);
        //    if (M1.n != M2.m)
        //        throw new Exception();
        //    Matrix ret = new Matrix(M1.m, M2.n);
        //    for (int i = 0; i < ret.m; i++)
        //        for (int j = 0; j < ret.n; j++)
        //        {
        //            ret[i, j] = 0;
        //            for (int k = 0; k < M1.n; k++)
        //                ret[i, j] += M1[i, k] * M2[k, j];
        //        }
        //    return ret;
        //}
        //// -- IsVector
        //public bool IsVector()
        //{
        //    return n == 1;
        //}
        // -- ToString()

        public List<double> ToList()
        {
            if (!IsVector())
                throw new Exception();
            List<double> ret = new List<double>();
            for (int i = 0; i < m; i++)
                ret.Add(values[i, 0]);
            return ret;

        }

        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                    str += string.Format("{0:0.000}", values[i, j]).PadLeft(8, ' ');
                str += i == m-1? "":"\n";
            }
            return str;
        }
    }
}
