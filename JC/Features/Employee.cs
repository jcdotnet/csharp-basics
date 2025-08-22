using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        //public string? Job { get; set; } // nullable string
        public string Job { get; set; }

        public Employee(int id, string name)
        {
            EmployeeID = id;
            Name = name;
        }

    }
}
