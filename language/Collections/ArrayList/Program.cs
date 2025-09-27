// ArrayList (= group of elements of any type)
// implements the IEnumerable, ICollection and IList interfaces
// https://learn.microsoft.com/en-us/dotnet/api/system.collections.arraylist?view=net-9.0
using ClassLibrary;
using System.Collections;

Console.WriteLine("ArrayList");

ArrayList list = new ArrayList() { 100, "John"};

list.Add(3.141592);
list.Add(new Employee() { Id = "A01", Name = "John" });

Console.WriteLine(list.Count);
foreach (var item in list) Console.WriteLine(item);