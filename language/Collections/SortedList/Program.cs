// SortedList
// key/value pairs that are sorted by the keys and are accessible by key and by index
// implements the IEnumerable<T>, ICollection<T> and IDictionary<TKey, TValue> interfaces
// https://learn.microsoft.com/en-us/dotnet/api/system.collections.sortedlist?view=net-9.0
Console.WriteLine("Sorted Lists");

SortedList<int, string> employees = new SortedList<int, string>()
{
    { 11, "Jane" },
    { 10, "John" },
    { 12, "Mike" },
};

Console.WriteLine(employees[10]);                   // John
Console.WriteLine(employees.IndexOfKey(10));        // 0 (list is sorted by key)
Console.WriteLine(employees.IndexOfValue("John"));  // 0

employees.Add(20, "David");
employees.Remove(12);

foreach (var employee in employees) Console.WriteLine(employee);
foreach (var pair in employees) Console.WriteLine(pair.Key);
foreach (var pair in employees) Console.WriteLine(pair.Value);

Console.WriteLine(employees.ContainsKey(20));           // True
Console.WriteLine(employees.ContainsValue("David"));    // False

foreach (var key in employees.Keys) Console.WriteLine(key);
foreach (var value in employees.Values) Console.WriteLine(value);