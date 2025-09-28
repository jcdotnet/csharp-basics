using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }

        // traditional constructor (verbose)
        //public Student(string name, int age)
        //{
        //    Name = name;
        //    Age = age;
        //}
        // constructor with property initializer
        public Student(string name, int age) => (Name, Age) = (name, age);

        override
        public string ToString() => $"{Name} - {Age}";
    }

    class Activity : Student
    {
        public string Club { get; set; }
        Activity(string name, int age, string club) : base(name, age)
        {
            Club = club;
        }
    }
}
