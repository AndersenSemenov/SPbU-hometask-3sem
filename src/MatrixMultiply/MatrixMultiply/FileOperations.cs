using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace MatrixMultiply
{
    class FileOperations
    {
        public static int[,] ReadMatrix(string path)
        {
            if (!File.Exists(path))
            {
                throw new ArgumentException();
            }

            var fileStrings = File.ReadAllLines(path);
            var matrix = new int[fileStrings.Length, fileStrings[0].Length];
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = Convert.ToInt32(fileStrings[i][j]);
                }
            }

            return matrix;
        }

        public static void WriteMatrix(string path)
        {
            using (var reader = FileReader)
        }
    }
}
