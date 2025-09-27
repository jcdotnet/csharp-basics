using ClassLibrary;

Console.WriteLine("Custom Collections");

// IEnumerable (custom collection)
EmployeesList employees = new EmployeesList();

employees.Add(new Employee() { Id = "E101", Name = "John", Type = EmployeeType.FullTime });
employees.Add(new Employee() { Id = "E201", Name = "Jane", Type = EmployeeType.FullTime });

// we can also use the collection initializer (it will call Add for each Employee)
// EmployeesList employees = new EmployeesList() {
//    new Employee() { Id = "E101", Name = "John", Type = EmployeeType.FullTime },
//    new Employee() { Id = "E201", Name = "Jane", Type = EmployeeType.FullTime }
// };

// IEnumerator enumerator = employees.GetEnumerator();
// enumerator.MoveNext();
// Console.WriteLine(((Employee)enumerator.Current).Name);

// foreach: GetEnumerator() will be called automatically 
foreach (Employee employee in employees) 
{
    Console.WriteLine(employee.Name);
};

Console.ReadKey();

// IEnumerable<T> (custom collection)
EmployeesList2 employees2 = new EmployeesList2()
{
    new Employee() { Id = "E101", Name = "John", Type = EmployeeType.FullTime },
    new Employee() { Id = "E201", Name = "Jane", Type = EmployeeType.FullTime }
};
foreach (Employee employee in employees2)
{
    Console.WriteLine(employee.Name);
};

// not exposed by IEnumerable
// employees2.Find();
// employees2.Clear();

Console.ReadKey();

// ICollection<T> (custom collection)
EmployeesList3 employees3 = new EmployeesList3()
{
    new Employee() { Id = "E101", Name = "John", Type = EmployeeType.FullTime },
    new Employee() { Id = "E201", Name = "Jane", Type = EmployeeType.FullTime }
};

Console.WriteLine(employees3.Count);

foreach (Employee employee in employees3)
{
    Console.WriteLine(employee.Name);
};

Console.WriteLine(employees3.Find(e => e.Name == "John").Name); 

employees3.Clear();

// not exposed by ICollection
// employees3.IndexOf(employee);
// employees3.Insert(1, employee);

Console.ReadKey();

// IList<T> (custom collection)
EmployeesList4 employees4 = new EmployeesList4()
{
    new Employee() { Id = "E101", Name = "John", Type = EmployeeType.FullTime },
    new Employee() { Id = "E201", Name = "Jane", Type = EmployeeType.FullTime }
};

employees4.Insert(1, new Employee() { Id = "E202", Name = "Mike", Type = EmployeeType.FullTime });

Console.WriteLine(employees4.Count);

foreach (Employee employee in employees4)
{
    Console.WriteLine(employee.Name);
};

Console.WriteLine(employees4.Find(e => e.Name == "John").Name);

// Contains() compares object references
// Behavior: Contains() using a existing element returns true as expected
// Problem: Contains using a new object with values that exists return false  
// Solution: to inherit from the IEquatable and implement it (Equals method)

// This will return False
Console.WriteLine(employees4.Contains(new Employee(){ Id = "E202", Name = "Mike", Type = EmployeeType.FullTime }));

employees4.Clear();

Console.ReadLine();

// IEquatable (model class)
EmployeesList5 employees5 = new EmployeesList5()
{
    new Employee2() { Id = "E101", Name = "John", Type = EmployeeType.FullTime },
    new Employee2() { Id = "E201", Name = "Jane", Type = EmployeeType.FullTime }
};

employees5.Insert(1, new Employee2() { Id = "E202", Name = "Mike", Type = EmployeeType.FullTime });

Console.WriteLine(employees5.Count);

foreach (Employee2 employee in employees5)
{
    Console.WriteLine(employee.Name);
};

Console.WriteLine(employees5.Find(e => e.Name == "John").Name);

// This is now True
Console.WriteLine(employees5.Contains(new Employee2() { Id = "E202", Name = "Mike", Type = EmployeeType.FullTime }));

employees5.Clear();

Console.ReadLine();

// IComparable
List<int> intList = [20, 10, 50, 30];

intList.Sort();

foreach (int i in intList) Console.WriteLine(i); // 10 20 30 50

List<Employee> list1 = new List<Employee>() { 
    new Employee() { Id = "E101", Name = "John", Type = EmployeeType.FullTime },
    new Employee() { Id = "E202", Name = "Mike", Type = EmployeeType.FullTime },
    new Employee() { Id = "E201", Name = "Jane", Type = EmployeeType.FullTime },
};

// list1.Sort(); // System.InvalidOperationException: 'Failed to compare two elements in the array.'
// solution: to implement the IComparableList in Employee
List<Employee2> list2 = new List<Employee2>() {
    new Employee2() { Id = "E101", Name = "John", Type = EmployeeType.FullTime },
    new Employee2() { Id = "E202", Name = "Mike", Type = EmployeeType.FullTime },
    new Employee2() { Id = "E201", Name = "Jane", Type = EmployeeType.FullTime },
};

list2.Sort();
foreach (var employee in list2) Console.WriteLine(employee.Id);