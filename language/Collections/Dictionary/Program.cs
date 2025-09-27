// Dictionary (map in C++, abstract parent class of HashTable in Java)
// key, value pairs
// key can't be null or duplicate
// implements the IEnumerable<T>, ICollection<T> and IDictionary<TKey, TValue> interfaces
// https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2?view=net-9.0
Console.WriteLine("Dictionaries");

Dictionary<string, int> ages = new Dictionary<string, int>(); // empty dictionary
ages["John"] = 22;
ages["Jane"] = 19;
ages["John"] = 30;
ages["Alice"] = 20;
ages.Add("David", 25); // put in Java

Console.WriteLine(ages.Count);
Console.WriteLine(ages["Alice"]);

Dictionary<int, string> employees = new Dictionary<int, string>() // collection initializer
{
    { 10, "John" },
    { 11, "Jane" },
    { 12, "Mike" },
};

foreach (KeyValuePair<int, string> pair in employees) Console.WriteLine(pair.Value);
foreach (var pair in employees) Console.WriteLine(pair.Value);

// Keys
Dictionary<int, string>.KeyCollection keys = employees.Keys;
foreach (var key in keys) Console.WriteLine(key);

employees[12] = "David";
// employees.Add(12, "Alice"); // System.ArgumentException (keys must be unique)

employees.Remove(12);
Console.WriteLine(employees.ContainsKey(12));       // False
Console.WriteLine(employees.ContainsValue("Mike")); // False

employees.Clear();
