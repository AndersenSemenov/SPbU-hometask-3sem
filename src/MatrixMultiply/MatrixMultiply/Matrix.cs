using System;
using System.Threading;
using static MatrixMultiply.Exceptions;

namespace MatrixMultiply
{
    class Matrix
    {
        public int AmountOfColumns { get; private set; }
        public int AmountOfRows { get; private set; }
        public int[,] Value { get; private set; }

        public Matrix(int[,] matrix)
        {
            this.Value = matrix;
            this.AmountOfColumns = matrix.GetLength(0);
            this.AmountOfRows = matrix.GetLength(1);
        }

        public Matrix(int n, int m)
        {
            if (n <= 0 || m <= 0)
            {
                throw new InvalidMatrixFormatException("Dimensions have to be natural");
            }

            var matrix = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    matrix[i, j] = 0;
                }
            }

            this.AmountOfColumns = n;
            this.AmountOfRows = m;
            this.Value = matrix;
        }

        public Matrix(int n, int m, int range)
        {
            if (n <= 0 || m <= 0)
            {
                throw new InvalidMatrixFormatException("Dimensions have to be natural");
            }

            var matrix = new int[n, m];
            var rand = new Random();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    matrix[i, j] = rand.Next(range);
                }
            }

            this.AmountOfColumns = n;
            this.AmountOfRows = m;
            this.Value = matrix;
        }

        public Matrix ParallelMultiply(Matrix matrix)
        {
            if (this.AmountOfRows != matrix.AmountOfColumns)
            {
                throw new InvalidMatrixFormatException("Incorrect matrix format!");
            }

            var threads = new Thread[Environment.ProcessorCount];
            var chunkSize = this.AmountOfColumns * matrix.AmountOfRows / threads.Length + 1;
            var result = new Matrix(this.AmountOfColumns, matrix.AmountOfRows);

            for (var t = 0; t < threads.Length; t++)
            {
                var currentI = chunkSize * t / result.AmountOfRows;
                var currentJ = chunkSize * t % result.AmountOfRows;
                var count = 0;

                threads[t] = new Thread(() =>
                {
                    for (var i = currentI; i < result.AmountOfColumns && count < chunkSize; i++)
                    {
                        for (var j = currentJ; j < result.AmountOfRows && count < chunkSize; j++)
                        {
                            for (var k = 0; k < this.AmountOfRows; k++)
                            {
                                result.Value[i, j] += this.Value[i, k] * matrix.Value[k, j];
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


        public Matrix Multiply(Matrix matrix)
        {
            if (this.AmountOfRows != matrix.AmountOfColumns)
            {
                throw new InvalidMatrixFormatException("Incorrect matrix format!");
            }

            var result = new Matrix(this.AmountOfColumns, matrix.AmountOfRows);

            for (var i = 0; i < this.AmountOfColumns; i++)
            {
                for (var j = 0; j < matrix.AmountOfRows; j++)
                {
                    for (var k = 0; k < this.AmountOfRows; k++)
                    {
                        result.Value[i, j] += this.Value[i, k] * matrix.Value[k, j];
                    }
                }
            }
            return result;
        }

        public bool IsEqual(Matrix matrix)
        {
            if (this.AmountOfColumns != matrix.AmountOfColumns || this.AmountOfRows != matrix.AmountOfRows)
            {
                return false;
            }
            for (var i = 0; i < this.AmountOfColumns; i++)
            {
                for (var j = 0;  j < this.AmountOfRows; j++)
                {
                    if (this.Value[i, j] != matrix.Value[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
