// Hashtable
// key/value pairs that are organized based on the hash code of the key
// implements the IEnumerable, ICollection and IDictionary interfaces
// https://learn.microsoft.com/en-us/dotnet/api/system.collections.hashtable?view=net-9.0
using System.Collections;

Console.WriteLine("Hashtables");

Hashtable employees = new Hashtable()
{
    { 11, "Jane" },
    { 10, "John" },
    { 12, "Mike" },
    { "Alice", 25 },
    { "Jane", 19 },
    { "David", 30 }
};

string john = (string)employees[10];    // casting from object to string
Console.WriteLine(john);                // John

employees.Add(20, "David");
employees.Remove(12);

foreach (var employee in employees) Console.WriteLine(employee);

Console.WriteLine(employees.ContainsKey(20));           // True
Console.WriteLine(employees.ContainsValue("David"));    // True
Console.WriteLine(employees.ContainsValue("Alice"));    // False

foreach (var key in employees.Keys) Console.WriteLine(key);
foreach (var value in employees.Values) Console.WriteLine(value);