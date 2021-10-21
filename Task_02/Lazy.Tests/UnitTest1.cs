using NUnit.Framework;
using Lazy;
using System;

namespace Lazy.Tests
{
    public class Tests
    {
        [Test]
        public void SupplierCantBeNull()
        {
            Assert.Throws<ArgumentNullException>(() => LazyFactory.CreateOneThreadLazy<object>(null));
            Assert.Throws<ArgumentNullException>(() => LazyFactory.CreateMultiThreadLazy<object>(null));
        }
    }
}