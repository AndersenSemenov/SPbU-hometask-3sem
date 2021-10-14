using System;
using System.Collections.Generic;
using System.Text;

namespace Lazy
{
    public class OneThreadLazy<T>: ILazy<T>
    {
        public T value { get; private set; }
        private bool IsCalculated = false;
        private Func<T> supplier;

        public OneThreadLazy(Func<T> supplier)
            => this.supplier = supplier ?? throw new ArgumentNullException("supplier is null");

        public T Get()
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
