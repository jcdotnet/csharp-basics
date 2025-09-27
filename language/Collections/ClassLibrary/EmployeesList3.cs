using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class EmployeesList3 : ICollection<Employee>
    {
        private List<Employee> employees = new List<Employee>();

        public int Count => employees.Count;

        public bool IsReadOnly => false;

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

        // implements the IEnumerable interface (needed for the compiler)
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        // implements the IEnumerable<T> interface
        public IEnumerator<Employee> GetEnumerator()
        {
            for (int i = 0; i < employees.Count; i++)
            {
                // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/yield
                yield return employees[i];
            }
        }
        // methods that implements the ICollection interface
        public void Clear()
        {
            employees.Clear();
        }

        public bool Contains(Employee item)
        {
            return employees.Contains(item);
        }

        public void CopyTo(Employee[] array, int arrayIndex)
        {
            employees.CopyTo(array, arrayIndex);
        }

        public bool Remove(Employee item)
        {
           return employees.Remove(item);
        }

        // public methods that we want to add to this custom collection
        public Employee Find(Predicate<Employee> predicate)
        {
            // Predicate is the prefefined delegate that represents a method that returns a boolean
            return employees.Find(predicate);
        }

        public List<Employee> FindAll(Predicate<Employee> predicate)
        {
            // Predicate is the prefefined delegate that represents a method that returns a boolean
            return employees.FindAll(predicate);
        }
    }
}
