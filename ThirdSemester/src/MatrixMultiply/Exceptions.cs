using System;

namespace MatrixMultiply
{
    /// <summary>
    /// Class with specific exceptions
    /// </summary>
    public class Exceptions
    {
        /// <summary>
        /// Exception throwing when the file is empty
        /// </summary>
        public class EmptyFileException: Exception
        {
            public EmptyFileException(string message): base(message) { }
        }

        /// <summary>
        /// Exception throwing when the matrix format or attempt to multiply matrices are invalid
        /// </summary>
        public class InvalidMatrixFormatException : Exception
        {
            public InvalidMatrixFormatException(string message): base(message) { }
        }
    }
}
