using System;
using System.Collections.Generic;
using System.Text;

namespace Lazy
{
    public static class LazyFactory
    {
        public static ILazy<T> CreateOneThreadLazy<T>(Func<T> supplier) 
            => new OneThreadLazy<T>(supplier);


        public static ILazy<T> CreateMultiThreadLazy<T>(Func<T> supplier)
            => new MultiThreadLazy<T>(supplier);
    }
}
