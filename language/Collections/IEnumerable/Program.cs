// IEnumerable (Arrays & Collections)
// https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=net-9.0
// https://learn.microsoft.com/en-us/dotnet/api/system.collections.ienumerable?view=net-9.0

// IEnumerator (Arrays & Collections)
// https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerator-1?view=net-9.0
// https://learn.microsoft.com/en-us/dotnet/api/system.collections.ienumerator?view=net-9.0


// IList<string> list; // ok
// ICollection<string> list;; // ok
IEnumerable<string> list;
// List<string> list; // ok
    
list = new List<string>() { "John", "Jane", "Alice", "Mike", "David" };

Console.WriteLine("IEnumerable:");
foreach (var item in list) Console.WriteLine(item);

var enumerator = list.GetEnumerator();
enumerator.Reset(); // set enumerator to its initial position (first element in the collection)

while (enumerator.MoveNext()) Console.WriteLine(enumerator.Current);
