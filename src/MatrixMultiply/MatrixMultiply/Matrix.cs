using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MatrixMultiply
{
    class Matrix
    {
        public int amountOfColumns { get; private set; }
        public int amountOfRows { get; private set; }
        public int[,] value { get; private set; }

        public Matrix(int[,] matrix)
        {
            this.value = matrix;
            this.amountOfColumns = matrix.GetLength(0);
            this.amountOfRows = matrix.GetLength(1);
        }

        public Matrix(int n, int m)
        {
            var matrix = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    matrix[i, j] = 0;
                }
            }

            this.amountOfColumns = n;
            this.amountOfRows = m;
            this.value = matrix;
        }

        public Matrix(int n, int m, int range)
        {
            var matrix = new int[n, m];
            var rand = new Random();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    matrix[i, j] = rand.Next(range);
                }
            }

            this.amountOfColumns = n;
            this.amountOfRows = m;
            this.value = matrix;
        }

        public Matrix ParallelMultiply(Matrix matr)
        {
            if (this.amountOfRows != matr.amountOfColumns)
            {
                throw new ArgumentException("Incorrect format of the matrix");
            }

            var threads = new Thread[Environment.ProcessorCount];
            var chunkSize = this.amountOfColumns * matr.amountOfRows / threads.Length + 1;
            var result = new Matrix(this.amountOfColumns, matr.amountOfRows);

            for (var t = 0; t < threads.Length; t++)
            {
                var currentI = chunkSize * t / result.amountOfRows;
                var currentJ = chunkSize * t % result.amountOfRows;
                var count = 0;

                threads[t] = new Thread(() =>
                {
                    for (var i = currentI; i < result.amountOfColumns && count < chunkSize; i++)
                    {
                        for (var j = currentJ; j < result.amountOfRows && count < chunkSize; j++)
                        {
                            for (var k = 0; k < this.amountOfRows; k++)
                            {
                                result.value[i, j] += this.value[i, k] * matr.value[k, j];
                            }
                            count++;
                        }
                        currentJ = 0;
                    }
                });
            }

            foreach (var thread in threads)
            {
                thread.Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            return result;
        }


        public Matrix Multiply(Matrix matr)
        {
            //null  на массивы

            if (this.amountOfRows != matr.amountOfColumns)
            {
                throw new ArgumentException("Incorrect format of the matrix"); //посмотреть наследники
            }

            var result = new Matrix(this.amountOfColumns, matr.amountOfRows);

            for (var i = 0; i < this.amountOfColumns; i++)
            {
                for (var j = 0; j < matr.amountOfRows; j++)
                {
                    for (var k = 0; k < this.amountOfRows; k++)
                    {
                        result.value[i, j] += this.value[i, k] * matr.value[k, j];
                    }
                }
            }
            return result;
        }

        public bool IsEqual(Matrix matr)
        {
            if (this.amountOfColumns != matr.amountOfColumns || this.amountOfRows != matr.amountOfRows)
            {
                return false;
            }
            for (var i = 0; i < this.amountOfColumns; i++)
            {
                for (var j = 0;  j < this.amountOfRows; j++)
                {
                    if (this.value[i, j] != matr.value[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
