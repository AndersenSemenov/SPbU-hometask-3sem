using System;

namespace MatrixMultiply
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                //
            }
            var matrix1 = new Matrix(FileOperations.ReadMatrix(args[0]));
            var matrix2 = new Matrix(FileOperations.ReadMatrix(args[1]));
            var result = matrix1.ParallelMultiply(matrix2);
            FileOperations.WriteMatrix(args[2], result);

            //Assert.throws
        }
    }
}
