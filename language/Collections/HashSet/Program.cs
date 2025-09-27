// HashSet
// set of values 
// implements the IEnumerable<T> and the ICollection<T> interfaces
// https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1?view=net-9.0
Console.WriteLine("Sets");

HashSet<string> set = new HashSet<string>()
{
    "John",
    "Jane",
    "Mike",
};

Console.WriteLine(set.Count); // 3

foreach (string s in set) Console.WriteLine(s);

set.Add("Alice");
set.Add("David");
set.Add("Mike");
Console.WriteLine(set.Count); // 5

foreach (string s in set) Console.WriteLine(s);

set.Remove("Mike");
set.RemoveWhere(s => s.StartsWith('J'));

Console.WriteLine(set.Count); // 2 (Alice, David)

foreach (string s in set) Console.WriteLine(s);

Console.WriteLine(set.Contains("Alice")); // True

// Union
HashSet<string> setB = ["John", "Jane", "Alice"];
set.UnionWith(setB);

Console.WriteLine(set.Count); // 4

foreach (string s in set) Console.WriteLine(s);

// Intersection
setB = ["Laura", "John", "Jane", "Mike", "Peter"];
set.IntersectWith(setB);

Console.WriteLine(set.Count); // 2 (Jane, John)

foreach (string s in set) Console.WriteLine(s);