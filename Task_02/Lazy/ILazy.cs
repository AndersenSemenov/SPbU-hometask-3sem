using System;
using System.Collections.Generic;
using System.Text;

namespace Lazy
{
    public interface ILazy<T>
    {
        T Get();
    }
}
