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
        static public void PrintMeasurements(Func<Matrix, Matrix, Matrix> func)
        {
            var start = 100;
            var step = 100;
            var stop = 1500;
            var amountOfMeasurements = 20;
            var range = 1000;
            for (var matrixDimension = start; matrixDimension <= stop; matrixDimension += step)
            {
                long expectedValue = 0;
                long variance = 0;
                for (var counter = 0; counter < amountOfMeasurements; counter++)
                {
                    var matrix1 = new Matrix(matrixDimension, matrixDimension, range);
                    var matrix2 = new Matrix(matrixDimension, matrixDimension, range);
                    var time = GetTime(func, matrix1, matrix2);
                    expectedValue += time;
                    variance += time * time;
                }
                expectedValue /= amountOfMeasurements;
                variance /= amountOfMeasurements;
                variance -= expectedValue * expectedValue;
                Console.WriteLine($"Measurements on {matrixDimension}x{matrixDimension} matrix:");
                Console.WriteLine($"Average time: {(double)(expectedValue) / 1000} seconds, standart deviation: {Math.Round(Math.Sqrt(variance) / 1000, 5)} seconds\n");
            }
        }
    }
}
