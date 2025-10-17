using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS11
{
    internal class MyClassName
    {
        public required string Name { get; set; }
        public bool AnotherProperty { get; set; }

        // if required members are set in the constructor
        // it should be marked with [SetsRequiredMembers]
        //[SetsRequiredMembers]
        //public MyClassName()
        //{
        //    Name = "NameValue";
        //}
    }
}
