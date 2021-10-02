using System;

namespace MatrixMultiply
{
    public class Exceptions
    {
        public class EmptyFileException: Exception
        {
            public EmptyFileException(string message): base(message) { }
        }

        public class InvalidMatrixFormatException : Exception
        {
            public InvalidMatrixFormatException(string message): base(message) { }
        }
    }
}
