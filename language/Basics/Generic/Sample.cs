using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic
{
    internal class Sample<T>
    {
        T field;

        public T Field
        {
            get { return field; }
            set { field = value; }
        }

        public string ShowInfo()
        {
            return $"The field value is {field} and the type is {field.GetType()}";
        }
    }
}
