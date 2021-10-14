using System;
using System.Collections.Generic;
using System.Text;

namespace Lazy
{
    class MultiThreadedLazy<T> : ILazy<T>
    {
        public T value { get; private set; }
        private bool IsCalculated = false;
        private Func<T> supplier;

        public MultiThreadedLazy(Func<T> supplier)
            => this.supplier = supplier ?? throw new ArgumentNullException("supplier is null");

        public T Get()
        {
            lock(new object())
            {
                if (!IsCalculated)
                {
                    IsCalculated = true;
                    value = supplier();
                    supplier = null;
                }

                return value;
            }
        }
    }
}
