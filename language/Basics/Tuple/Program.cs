// Tuple class
// https://learn.microsoft.com/en-us/dotnet/api/system.tuple?view=net-9.0
// https://learn.microsoft.com/es-es/dotnet/api/system.tuple-2?view=net-9.0
Console.WriteLine("Tuples");

Tuple<string, int> person1 = new Tuple<string, int>("John", 21);

Console.WriteLine(person1.Item1); // John
Console.WriteLine(person1.Item2); // 21

Tuple<string, int, decimal> person2 = new Tuple<string, int, decimal>("David", 30, 400000);

Console.WriteLine(person2.Item1); // David
Console.WriteLine(person2.Item2); // 30
Console.WriteLine(person2.Item3); // 400000

Console.ReadLine();

// Value Tuples (C#7.1)
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/value-tuples
Console.WriteLine("Value Tuples");

(string, int) t1 = ("John", 21);
Console.WriteLine(t1.Item1); // John
Console.WriteLine(t1.Item2); // 21

(string Name, int Age) t1WithFields = ("John", 21);

Console.WriteLine(t1WithFields.Name);
Console.WriteLine(t1WithFields.Age);

(string Name, int Age, decimal TotalMoney) t2 = ("David", 30, 400000);

Console.WriteLine(t2.Name);
Console.WriteLine(t2.Age);
Console.WriteLine(t2.TotalMoney);

Console.ReadLine();

// Tuple assignment and deconstruction
var tPerson = ("John", 21);
var (name, age) = tPerson;  // deconstruction

Console.WriteLine(name);    // John
Console.WriteLine(age);     // 21

var tPlace = ("post office", 3.6);
var (destination, distance) = tPlace;
Console.WriteLine($"Distance to {destination} is {distance} kilometers.");

Console.ReadLine();

// Discard (allows to skip values of the tuple that we don't require)
// https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/functional/discards
// https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/functional/deconstruct#tuple-elements-with-discards
(string Name, int Age, decimal TotalMoney) person = ("Alice", 30, 500000);
var (personName, _, personTotalMoney) = person;
Console.WriteLine($"Name: {personName}, Total Money: {personTotalMoney}");