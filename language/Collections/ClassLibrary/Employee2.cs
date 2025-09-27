using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClassLibrary
{
    public class Employee2 : IEquatable<Employee2>, IComparable<Employee2>
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public EmployeeType Type { get; set; } = EmployeeType.FullTime;

        public int CompareTo(Employee2? other)
        {
            return string.Compare(this.Id, other?.Id);
        }

        public bool Equals(Employee2? other)
        {
            return this.Id == other?.Id && this.Name == other?.Name; 
        }
    }
}
