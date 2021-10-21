using System;

namespace Lazy
{
    class Program
    {
        static void Main(string[] args)
        {
            var lazy = LazyFactory.CreateMultiThreadLazy<int>(() => 2 * 3);
            Console.WriteLine(lazy.Get());
        }
    }
}
