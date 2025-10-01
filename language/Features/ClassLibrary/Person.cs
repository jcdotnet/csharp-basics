using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Person(string fistName, string lastName, int age) // primary constructor
    {
        public string FirstName { get; set; } = fistName;
        public string LastName { get; set; } = lastName;
        public int Age { get; set; } = age;

        public override string ToString() => $"{FirstName} {LastName} - {Age}";
    }
}
