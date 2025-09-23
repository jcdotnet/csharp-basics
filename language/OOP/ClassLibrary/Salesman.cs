using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    // Base class: Employee
    // Derived class: Salesman (specialization of employee)
    // Salesman is a employee
    public class Salesman: Employee
    {
        private string _region;

        public string Region { get; set; }

        // learning purposes
        // IRL maybe either to override from parent or implement from a sales interface/abstract class
        public long GetTotalSales()
        {
            return 10000; // dummy value
        }

    }
}
