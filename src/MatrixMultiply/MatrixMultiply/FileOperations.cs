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
            var splitted = fileStrings.Select(s => s.Split(" ")).ToArray();
            var matrix = new int[splitted.Length, splitted[0].Length];
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = Convert.ToInt32(splitted[i][j]);
                }
            }

            return matrix;
        }

        public static void WriteMatrix(string path, Matrix matrix)
        {
            using (var writer = new StreamWriter(path))
            {
                for (var i = 0; i < matrix.amountOfColumns; i++)
                {
                    var str = "";
                    for (var j = 0; j < matrix.amountOfRows; j++)
                    {
                        str += $"{matrix.value[i, j]} ";
                    }
                    writer.WriteLine(str);
                }
            }
        }
    }
}
