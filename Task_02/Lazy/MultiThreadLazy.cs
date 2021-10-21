using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Lazy
{
    class MultiThreadLazy<T> : ILazy<T>
    {
        public T Value { get; private set; }
        private bool isCalculated = false;
        private Func<T> supplier;
        private object lockObject = new object();

        public MultiThreadLazy(Func<T> supplier)
            => this.supplier = supplier ?? throw new ArgumentNullException("supplier is null");

        public T Get()
        {
            if (!Volatile.Read(ref isCalculated))
            {
                lock (lockObject)
                {
                    Value = supplier();
                    Volatile.Write(ref isCalculated, true);
                    supplier = null;
                    return Value;
                }
            }
            else
            {
                return Value;
            }
        }
    }
}
