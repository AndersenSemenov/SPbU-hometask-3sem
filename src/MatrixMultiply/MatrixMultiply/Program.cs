using System;

namespace MatrixMultiply
{
    class Program
    {
        static void Main(string[] args)
        {
            var matrix1 = FileOperations.ReadMatrix(args[0]);
            var matrix2 = FileOperations.ReadMatrix(args[1]);
            var matrix3 = Matrix.Multiply(matrix1, matrix2);
            //Assert.throws
        }
    }
}
