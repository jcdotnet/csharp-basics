using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class EmployeesList : IEnumerable
    {
        private List<Employee> employees = new List<Employee>();

        public void Add(Employee employee)
        {
            // validation logic
            if (employee.Id.ToUpper().StartsWith("E"))
            {
                employees.Add(employee);
            }
            else
            {
                Console.WriteLine("Invalid Employee"); // maybe to throw exception instead
            }
        }
        public IEnumerator GetEnumerator()
        {         
            for (int i = 0; i < employees.Count; i++)
            {
                // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/yield
                yield return employees[i];
            }
        }
    }
}
