using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NullReferenceExceptions
{
    internal class Person
    {
        // FirstName
        // non-nullable reference type (we don't want this property to be null)
        // since we don't allow null values, we should initialize the property here
        // or add the required keyword so the compiler forces us to initialize it
        public string FirstName { get; set; } // = "John"

        // LastName
        // nullable reference type (we want this property to be null)
        // since we allow null, compiler will give uwarnings for null-safety
        public string? LastName { get; set; }

        // Children
        // nullable value type
        public int? Children { get; set; } 
       
    }
}
