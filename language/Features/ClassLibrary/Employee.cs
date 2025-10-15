using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string? Job { get; set; } // nullable string

        public Employee() { }
        public Employee(int id, string name)
        {
            EmployeeID = id;
            Name = name;
        }
        public static string CapitalizeJob(Employee employee) => employee.Job?.ToUpper() ?? String.Empty;
    }

}
