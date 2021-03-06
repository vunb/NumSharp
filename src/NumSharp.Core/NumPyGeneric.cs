﻿using System;
using System.Collections.Generic;
using System.Text;
using NumSharp.Core.Extensions;

namespace NumSharp.Core
{
    /// <summary>
    /// API bridge between NumSharp and Python NumPy  
    /// </summary>
    [Obsolete("please use NumPy")]
    public class NumPyGeneric<T>
    {
        

        public NDArrayGeneric<T> arange(int stop)
        {
            return arange(0, stop);
        }

        public NDArrayGeneric<T> arange(int start, int stop, int step = 1)
        {
            if(start > stop)
            {
                throw new Exception("parameters invalid");
            }

            switch (typeof(T).Name)
            {
                case "Int32":
                    {
                        var n = new NDArrayGeneric<int>();
                        n.arange(stop, start, step);
                        return n as NDArrayGeneric<T>;
                    }

                case "Double":
                    {
                        var n = new NDArrayGeneric<double>();
                        n.arange(stop, start, step);
                        return n as NDArrayGeneric<T>;
                    }
                default:
                    throw new NotImplementedException();
            }
        }

        public NDArrayGeneric<T> array(T[] data)
        {
            var n = new NDArrayGeneric<T>();
            n.Data = data;
            n.Shape = new Shape(new int[] { data.Length });

            return n;
        }

        public NDArrayGeneric<T> array(T[][] data)
        {
            int size = data.Length * data[0].Length;
            var all = new T[size];

            int idx = 0;
            for (int row = 0; row < data.Length; row++)
            {
                for (int col = 0; col < data[row].Length; col++)
                {
                    all[idx] = data[row][col];
                    idx++;
                }
            }

            var n = new NDArrayGeneric<T>();
            n.Data = all;
            n.Shape = new Shape(new int[] { data.Length, data[0].Length });

            return n;
        }

        public NDArrayGeneric<double> hstack(params NDArrayGeneric<double>[] nps)
        {
            var n = new NDArrayGeneric<double>();
            return n.HStack(nps);
        }

        public NumPyRandom random 
        {
            get
            {
                return new NumPyRandom();
            }
        }

        public NDArrayGeneric<int> reshape(NDArrayGeneric<int> np, params int[] shape)
        {
            np.Shape = new Shape(shape);

            return np;
        }

        public NDArrayGeneric<double> vstack(params NDArrayGeneric<double>[] nps)
        {
            var n = new NDArrayGeneric<double>();
            return n.VStack(nps);
        }
    }
}
