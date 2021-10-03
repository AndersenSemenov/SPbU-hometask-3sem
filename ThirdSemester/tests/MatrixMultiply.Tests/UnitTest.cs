using NUnit.Framework;
using System;
using System.IO;
using static MatrixMultiply.Exceptions;

namespace MatrixMultiply.Tests
{
    public class Tests
    {
        [TestCase(1, 1, 1, 100)]
        [TestCase(2, 3, 2, 100)]
        [TestCase(10, 8, 14, 100)]
        [TestCase(300, 150, 250, 1000)]
        [TestCase(100, 50, 100, 1000)]
        public void MultiplicationShouldBeEqual(int n, int k, int m, int range)
        {
            var matrix1 = new Matrix(n, k, range);
            var matrix2 = new Matrix(k, m, range);
            var sequenceResult = matrix1.Multiply(matrix2);
            var parallelResult = matrix1.ParallelMultiply(matrix2);
            Assert.IsTrue(sequenceResult.IsEqual(parallelResult));
        }

        [TestCase(5, 3, 2, 4, 100)]
        [TestCase(100, 310, 68, 40, 100)]
        public void CatchMultiplyException(int n, int k, int m, int l, int range)
        {
            var matrix1 = new Matrix(n, k, range);
            var matrix2 = new Matrix(m, l, range);
            Assert.Throws<InvalidMatrixFormatException>(() => matrix1.Multiply(matrix2));
            Assert.Throws<InvalidMatrixFormatException>(() => matrix1.ParallelMultiply(matrix2));
        }

        [TestCase(-5, 3, 100)]
        [TestCase(300, -220, 100)]
        public void CatchConstructorException(int n, int m, int range)
        {
            Assert.Throws<InvalidMatrixFormatException>(() => new Matrix(n, m));
            Assert.Throws<InvalidMatrixFormatException>(() => new Matrix(n, m, range));
        }

        [TestCase("..//..//..//..//..//data/MatrixMultiply/first.txt")]
        public void CatchEmptyFileException(string path)
        {
            Assert.Throws<EmptyFileException>(() => FileOperations.ReadMatrix(path));
        }

        [TestCase("..//..//..//..//..//data/MatrixMultiply/second.txt")]
        public void CatchFormatException(string path)
        {
            Assert.Throws<FormatException>(() => FileOperations.ReadMatrix(path));
        }

        [TestCase("..//..//..//..//..//data/MatrixMultiply/third.txt")]
        [TestCase("..//..//..//..//..//data/MatrixMultiply/fourth.txt")]
        public void CatchNotEqualColumnsRowsException(string path)
        {
            Assert.Throws<InvalidMatrixFormatException>(() => FileOperations.ReadMatrix(path));
        }
    }
}