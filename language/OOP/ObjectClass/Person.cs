using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectClass
{
    /*
    class Object (namespace System)
    {
        virtual bool Equals(object value);
        virtual int GetHashCode();
        Type GetType();
        virtual string ToString();
        
    }
    */

    public class Person // derived from System.Object
    {
        public string Name { get; set; }
        public string Email { get; set; }

        // override object.Equals
        public override bool Equals(object obj)
        {
            Person other = obj as Person;
            return (other != null && this.Name == other.Name && this.Email == other.Email);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Email);
        }

        public override string ToString()
        {
            return $"Name: {Name}. Email: {Email}";
        }
    }

    public class PersonChildClass // derived from Person
    {

    }
}
