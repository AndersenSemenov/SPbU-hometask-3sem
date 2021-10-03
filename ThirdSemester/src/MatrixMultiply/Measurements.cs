using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MatrixMultiply
{
    /// <summary>
    /// Class for taking measurements
    /// </summary>
    public static class Measurements
    {
        /// <summary>
        /// Gets elapsed time of function work
        /// </summary>
        /// <param name="function">Function to measure</param>
        /// <param name="matrix1">First matrix to multiply</param>
        /// <param name="matrix2">Second matrix to multiply</param>
        /// <returns>Elapsed time while function was working</returns>
        public static long GetTime(Func<Matrix, Matrix, Matrix> function, Matrix matrix1, Matrix matrix2)
        {
            var watch = new Stopwatch();
            watch.Start();
            var res = function(matrix1, matrix2);
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Counts average time of matrix multiply with different dimension
        /// </summary>
        /// <param name="func">Function to take measurements on</param>
        /// <returns>long[,], where one row is a series of experiments with fixed matrix dimension</returns>
        static public long[] TakeMeasurements(Func<Matrix, Matrix, Matrix> func)
        {
            var start = 10;
            var step = 50;
            var stop = 410;
            var amountOfMeasurements = 10;
            var arr = new long[(stop - start)/step + 1];
            var range = 10000;
            for (var matrixDimension = start; matrixDimension <= stop; matrixDimension += step)
            {
                long averageTime = 0;
                for (var counter = 0; counter < amountOfMeasurements; counter++)
                {
                    var matrix1 = new Matrix(matrixDimension, matrixDimension, range);
                    var matrix2 = new Matrix(matrixDimension, matrixDimension, range);
                    averageTime += GetTime(func, matrix1, matrix2);
                }
                averageTime /= amountOfMeasurements;
                arr[(matrixDimension - start) / step] = averageTime;
            }
            return arr;
        }
    }
}
