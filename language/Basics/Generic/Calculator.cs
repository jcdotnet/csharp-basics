using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic
{
    internal class Calculator<T>
    {
        public T Add(T x, T y) => (dynamic)x + y;

        public dynamic DynamicProperty { get; set; }
    }
}
