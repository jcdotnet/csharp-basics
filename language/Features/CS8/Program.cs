using ClassLibrary;
// https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-version-history#c-version-80
// https://learn.microsoft.com/en-us/shows/connect-microsoft-connect--2018/d140
// https://devblogs.microsoft.com/dotnet/building-c-8-0/
Console.WriteLine("C# 8 features");

// feat: static local functions
// https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/local-functions
{
    Student student = new Student();
    student.DisplayGradesWithStaticFunction(100, 80); // 100, 80, 90
}

// feat: default interface members
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/interface#default-interface-members
ICar car1 = new MyCar();
car1.Run();
MyCar car2 = new MyCar();
car2.Run();
MyCar car3 = new MyCar();
((ICar)car3).Run(120);

// feat: readonly members
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/readonly#readonly-instance-members
Coord coord = new Coord();
coord.X = 2;
coord.Y = 4;
Console.WriteLine(coord.DisplayPosition()); // read-only member

// feat: nullable reference types
// https://learn.microsoft.com/en-us/dotnet/csharp/nullable-references
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/nullable-reference-types
// see language > Basics > NullReferenceExceptions project
Employee employee = new Employee(1, "John Doe");
string job = employee.Job;

// feat: null forgiving operator (!)
// a way for developers to inform the compiler that a variable should not be treated as null
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-forgiving

// feat: null-coalescing assignment operator (??=)
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-coalescing-operator
List<string> jobs = new List<string>();
jobs.Add(job ??= "Developer");
Console.WriteLine("Job: " + jobs[0]);

// feat: disposable ref structs // see Car ref struct below
using (var computer = new Computer())
{
    Console.WriteLine("My computer is very cool");
}

// feat: pattern matching: switch expressions
Console.WriteLine("--- Switch Expressions ---");
string role = "admin";
string access;

switch (role) // classic switch
{
    case "admin":
        access = "Full Access";
        break;
    case "user":
        access = "Limited Access";
        break;
    case "guest":
        access = "Read Only";
        break;
    default: access = "No Access"; break;
}

access = role switch // C# 8++
{
    "admin" => "Full Access",
    "user" => "Limited Access",
    "guest" => "Read Only",
    _ => "No Access"
};

Console.WriteLine($"Access: {access}");

// see relational patterns in C# 9
Person person = new("Jane", "Doe", 20);
string result = person switch
{
    Person p when p.Age < 13 => $"{p} is a child",
    Person p when p.Age >= 13 && p.Age < 18 => $"{p} is a teenager",
    Person p when p.Age > 18 => $"{p} is an adult",
    _ => $"{person} is a person"
};
Console.WriteLine(result);

// see logical patterns in C# 9
Months month = Months.March;
string season = month switch
{
    Months.January or Months.February or Months.March => "Winter",
    Months.April or Months.May or Months.June => "Spring",
    Months.July or Months.August or Months.September => "Summer",
    Months.October or Months.November or Months.December => "Autumn",
    _ => "Invalid Input"
};
Console.WriteLine(season);

// feat: pattern matching: property pattern
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/patterns#property-pattern
Console.WriteLine("--- Property Pattern ---");

DateTime date = new DateTime(2020, 5, 20);
{
    Student student = new Student("David", 20);
    // if (student != null && student.Age > 18 && student.Name.Length > 3)
    if (student?.Age > 18 && student?.Name.Length > 3)
    {
        Console.WriteLine($"Send Email to {student.Name}");
    }
    if (student is { Age: > 18, Name.Length: > 3 })
    {
        Console.WriteLine($"Send Email to {student.Name}");
    }
}
// with logical pattern (see C# 9)
Console.WriteLine(date is { Year: 2020, Month: 5, Day: 19 or 20 or 21 } ? "It's conference day" : "It's NOT conference day");

// feat: pattern matching: tuple patterns
string greeting1 = (person, person.Age, person.Gender) switch
{
    (Person, _, "Female") => $"Ms. {person.FirstName}",
    (Person, < 18, "Male") => $"Master. {person.FirstName}",
    (Person, >= 18, "Male") => $"Mr. {person.FirstName}",
    (Person, _, not ("Male" or "Female")) => $"Mx. {person.FirstName}",
    //_ => person.FirstName
};
Console.WriteLine(greeting1);

// feat: pattern matching: positional patterns
string greeting2 = person switch
{
    (Person, _, "Female") => $"Ms. {person.FirstName}",
    (Person, < 18, "Male") => $"Master. {person.FirstName}",
    (Person, >= 18, "Male") => $"Mr. {person.FirstName}",
    (Person, _, not ("Male" or "Female")) => $"Mx. {person.FirstName}",
    _ => person.FirstName
};
Console.WriteLine(greeting2);

Point startPoint = new Point()
{
    X = 0,
    Y = 0,
};
Console.WriteLine(Point.DisplayPosition(startPoint));
Point point = new Point()
{
    X = 100,
    Y = 150,
};
Console.WriteLine(Point.DisplayPosition(point));

Console.ReadLine();

// feat: enhancement of interpolated verbatim strings
Console.WriteLine("--- Verbatim Interpolated Strings ---");
string normal = "Sevilla is a very beautiful city. \nI visited it multiple times";
Console.WriteLine(normal);

string verbatim = @"Sevilla is a very beautiful city
beautiful city.
I visited it mutiple times.";

Console.WriteLine(verbatim);

int visits = 2;

string concat = "I visited Sevilla " + visits + " times.";
Console.WriteLine(concat);

string interpolate = $"I visited Sevilla {visits} times.";
Console.WriteLine(interpolate);

string format = string.Format("I visited Sevilla {0} times.", visits);
Console.WriteLine(format);

string verbatimSevilla = @$"Sevilla is a very beautiful city 
I visited it {visits} times";
Console.WriteLine(verbatimSevilla);

string[] @for = { "John", "Jane", "Alice" };
foreach (var name in @for)
{
    Console.WriteLine(name);
}

Console.ReadLine();

// feat: index/hat and range operator
Console.WriteLine("---  Hat and Range Operators ---");
var cities = new string[] {
    "New York", // 0 - 5
    "Sevilla",  // 1 - 4
    "Madrid",   // 2 - 3
    "Málaga",   // 3 - 2
    "London"    // 4 - 1
};

Console.WriteLine(cities[cities.Length - 1]); // London
Console.WriteLine(cities[^2]); // Málaga 

List<int> numbersList = new List<int>() {0, 1, 2, 4, 5};

Console.WriteLine(numbersList[^2]); // 4

var range = cities[1..^2];
//var range = cities[0..3];
//var range = cities[^2..^1];
//var range = cities[..];
//var range = cities[..3];
// var range = cities[0..];

foreach (var city in range)
{
    Console.WriteLine(city); // Sevilla Madrid
}

Range cityRange = 0..4;
Console.WriteLine(cityRange.Start); // 0
Console.WriteLine(cityRange.End); // 4

var newCities = cities[cityRange];
foreach (var city in newCities)
{
    Console.WriteLine(city);
}