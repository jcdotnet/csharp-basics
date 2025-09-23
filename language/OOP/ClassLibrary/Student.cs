using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Student // PascalCase
    {
        // fields // camelCase
        private string name = "John"; // default value
        private int grade;
        private string department; // Department department (department can be an object instead)

        // properties (encapsulation) // PascalCase
        public string Name
        {
            get { return name; }
            set { if (!string.IsNullOrEmpty(value)) name = value; }
        }
        public int Grade
        {
            get { return grade; }
            set { if (value > 0) grade = value; }
        }
        public string Department
        {
            get { return department; }
            set { if (!string.IsNullOrEmpty(value)) department = value; }
        }
        public double Score { get; set; } // automatic property (C#3)
        public bool IsRegistered { get; } // read-only property 
        // public bool IsGraduated { set { } } // write-only property
        public bool IsGraduated { private get; set; } // we can read the value inside the class

        // constructor // PascalCase
        public Student()
        {
            IsRegistered = true;
            IsGraduated = false;
        }
        public Student(string name, int grade, string department)
        {
            this.name = name;
            this.grade = grade;
            this.department = department;
            IsRegistered = true;
            IsGraduated = false;
        }

        // methods // PascalCase

        //public void Register()
        //{
        //    // we can only assign values to a read only property inside constructors
        //    // in order to set the value here we can add private set to the property
        //    //IsRegistered = true; 
        //}
        public string ShowInfo()
        {
            return $"{name} is at grade {grade} in the {department} department with score {Score}. Graduated: {IsGraduated}";
        }

    }
}

