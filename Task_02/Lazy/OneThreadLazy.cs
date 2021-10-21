using System;
using System.Collections.Generic;
using System.Text;

namespace Lazy
{
    public class OneThreadLazy<T>: ILazy<T>
    {
        public T Value { get; private set; }
        private bool isCalculated = false;
        private Func<T> supplier;

        public OneThreadLazy(Func<T> supplier)
            => this.supplier = supplier ?? throw new ArgumentNullException("supplier is null");

        public T Get()
        {
            if (!isCalculated)
            {
                Value = supplier();
                isCalculated = true;
                supplier = null;
            }
            return Value;
        }
    }
}
