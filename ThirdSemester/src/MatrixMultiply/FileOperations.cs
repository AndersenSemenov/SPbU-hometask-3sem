using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using static MatrixMultiply.Exceptions;

namespace MatrixMultiply
{
    public class FileOperations
    {
        public static int[,] ReadMatrix(string path)
        {

            var fileStrings = File.ReadAllLines(path);

            if (fileStrings.Length == 0)
            {
                throw new EmptyFileException("File is empty, you should write matrix into it");
            }

            var splitted = fileStrings.Select(s => s.Split(" ")).ToArray();
            var matrix = new int[splitted.Length, splitted[0].Length];
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                if (splitted[i].Length != splitted[0].Length)
                {
                    throw new InvalidMatrixFormatException("Lengths of rows have to be equal");
                }

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
                for (var i = 0; i < matrix.AmountOfColumns; i++)
                {
                    var str = "";
                    for (var j = 0; j < matrix.AmountOfRows; j++)
                    {
                        str += $"{matrix.Value[i, j]} ";
                    }
                    writer.WriteLine(str);
                }
            }
        }
    }
}
