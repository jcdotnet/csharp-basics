using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary 
{
    // is-a relationship (Manager is an Employee) // sealed (classes cannot inherit from Manager)
    sealed public class Manager: Employee, ISales 
    { 
        private string _department; 

        public string Department { get => _department; set => _department = value; }

        public Manager()
        {

        }
        public Manager(int id, string name, string location, string departmentName): base(id, name, location)
        {
            _department = departmentName;
        }

        // learning purposes
        // IRL maybe either to override from parent or implement from a sales interface/abstract class
        public long GetTotalSales() 
        {
            return 10000; // dummy hardcoded value
        }

        public string GetDepartmentInfo()
        {
            return Department + " at " + base.Location; // _location is also available here
        }

        public override void Rest()
        {
            // this method overrides the base method
            base.Rest(); // callig base method // optional
            Console.WriteLine("Manager: 30 extra minutes of rest");
        }

        public new void Work()
        {
            // this method hides the base method // the base method won't execute
            Console.WriteLine("Manager is overseeing operations.");
        }
    }
}
