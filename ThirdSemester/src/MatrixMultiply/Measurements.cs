using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MatrixMultiply
{
    public class Measurements
    {
        public static long GetTime(Func<Matrix, Matrix, Matrix> function, Matrix matrix1, Matrix matrix2)
        {
            var watch = new Stopwatch();
            watch.Start();
            var res = function(matrix1, matrix2);
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        static public long[] hueta(Func<Matrix, Matrix, Matrix> func)
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
