using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Person(string firstName, string lastName, int age) // primary constructor
    {
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
        public int Age { get; set; } = age;
        public string Gender { get; set; } = "Female";

        public void Deconstruct(out Person person, out int? age, out string? gender) // tuple matching
        {
            person = this;
            age = Age;
            gender = Gender;
        }

        public override string ToString() => $"{FirstName} {LastName} - {Age}";
    }
}
