using NUnit.Framework;
using System.IO;

namespace MatrixMultiply.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var s = Directory.GetCurrentDirectory();
            
            Assert.Pass();
        }
    }
}