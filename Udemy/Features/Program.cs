// See https://aka.ms/new-console-template for more information
using Features;

Character c = new Character(Features.Type.Elf);
c.Name = "My Character";
Console.WriteLine(c.Armor);
Console.WriteLine(ArmedByKnife(c));
Console.WriteLine(c); // ToString()

List<Character> characters = new List<Character>()
{
    new Character(Features.Type.Elf),
    new Character(Features.Type.Ork)
};

List<Character> characters2 = new List<Character>() // C#6: collection initialization
{
    Features.Type.Elf,
    Features.Type.Ork
};

Point p = new Point() { X = 1, Y = 1 };
//int xx, yy;
p.GetCoords(out int xx, out int yy); // C#7: out variables
Console.WriteLine($"X: {xx}, Y:{yy}");

string digit = "10";
int.TryParse(digit, out int d); // out variables are commonly used in TryDoSomething scenarios
Console.WriteLine($"Parsed Digit: {d}");

#region C#8 features

// feat: default Interface methods // see ICar interface
ICar f1 = new Ferrari();
f1.Run();
Ferrari f2 = new Ferrari();
f2.Run();
Ferrari f3 = new Ferrari();
((ICar)f2).Run(120);

// feat: working with nulls // see Employee class
Employee john = new Employee(1, "John Doe");
Console.WriteLine($"Job: {CapitalizeJob(john)}");

// feat: null-coalescing assignment operator
List<int> numbers = new List<int>();
int? i = null; // nullable int

numbers.Add(i ??= 10);

// feat: disposable ref structs // see Car ref struct below
using (var car = new Car())
{
    Console.WriteLine("My car is very cool");
}

// feat: switch expressions
Months month = Months.March;
Console.WriteLine(CheckMonthOldWay(month));
Console.WriteLine(CheckMonth(month));

// feat: property patterns
City viennaAus = new City(Cities.Vienna, "Austria");
City parisFr = new City(Cities.Paris, "France");
City parisUS = new City(Cities.Paris, "USA");

Console.WriteLine(CheckPrices(viennaAus));
Console.WriteLine(CheckPrices(parisUS));

// feat: tuple patterns
Console.WriteLine(CheckPrices2("Vienna", "Austria"));
Console.WriteLine(CheckPrices2("Paris", "France"));
Console.WriteLine(CheckPrices2("Paris", "US"));

Console.WriteLine(CheckPrices3("Vienna", "Austria", "German"));
Console.WriteLine(CheckPrices3("Paris", "FR", "FR"));

// feat: positional patterns
Point startPoint = new Point()
{
    X = 0,
    Y = 0,
};
Console.WriteLine(DisplayPosition(startPoint));
Point point = new Point()
{
    X = 100,
    Y = 150,
};
Console.WriteLine(DisplayPosition(point));

// feat: readonly members
Coord coord = new Coord();
coord.X = 2;
coord.Y = 4;
Console.WriteLine(coord.DisplayPosition());


// feat: static local functions
LocalFunctions();

// feat: asynchronous Streams
Console.WriteLine("--- Asynchronous Streams ---");
Task _ = AsyncStreams(); // Task task // c#7: discard operator 

// feat: verbatim interpolated strings
Console.WriteLine("--- Verbatim Interpolated Strings ---");
string normal = "Vienna is a very beautiful city. \nI visited it multiple times";
Console.WriteLine(normal);

string verbatim = @"Vienna is a very beautiful city
beautiful city.
I visited it mutiple times.";

Console.WriteLine(verbatim);

int visits = 2;
Console.WriteLine("------");

string concat = "I visited Vienna " + visits + " times.";
Console.WriteLine(concat);

string interpolate = $"I visited Vienna {visits} times.";
Console.WriteLine(interpolate);

string format = string.Format("I visited Vienna {0} times.", visits);
Console.WriteLine(format);

string travelToVienna = @$"Vienna is a very beautiful city 
I visited it {visits} times";
Console.WriteLine(travelToVienna);

string[] @for = { "Jack", "Jennifer", "Smith" };
foreach (var name in @for)
{
    Console.WriteLine(name);
}

// feat: index/hat operator
Console.WriteLine("---  Hat Operator ---");
var cities = new string[] {
    "New York", // 0 - 5
    "Vienna",   // 1 - 4
    "Madrid",   // 2 - 3
    "London",   // 3 - 2
    "Cairo"     // 4 - 1
};

Console.WriteLine(cities[cities.Length - 1]);
Console.WriteLine(cities[^2]);

List<int> numbersList = new List<int>();
numbersList.Add(0);
numbersList.Add(1);
numbersList.Add(4);
numbersList.Add(5);

Console.WriteLine(numbersList[^2]);

// feat: range operator
//var copyCities = cities[0..3];
//var copyCities = cities[^2..^1];
//var copyCities = cities[..];
//var copyCities = cities[..3];
var copyCities = cities[0..];

foreach (var city in copyCities)
{
    Console.WriteLine(city);
}

Range cityRange = 0..4;
Console.WriteLine(cityRange.Start);
Console.WriteLine(cityRange.End);

var newCities = cities[cityRange];
foreach (var city in newCities)
{
    Console.WriteLine(city);
}

#endregion

#region C#12 features

Console.WriteLine("--- C#12 features ---");
Console.ReadLine();

// feat: primary constructors
Student student = new Student("John Doe", 20); // see primary constructor in the Student class
Console.WriteLine(student);

// feat: collection enhacements
List<int> list1 = new List<int>() { 1, 3, 3, 5, 7};
// inline initilization
List<int> list2 = new List<int> { 1, 3, 3, 5, 7 };
List<int> list3 = [ 1, 3, 3, 5, 7 ];

foreach (int number in list3) { Console.WriteLine(number); }

// feat: ref readonly parameters // prevent accidental modifications of the data passed
// used when you need to access data passed by ref but you don't inted to modify it within the method
var PrintNumber = (ref int number) =>
{
    Console.WriteLine(number);
    number++; // Compile error if readonly ref
};
int myNumber = 32;
PrintNumber(ref myNumber);
PrintNumber(ref myNumber);

//feat: default lambda parameters 
var IncrementBy = (int source, int increment = 1) =>
{
    return source + increment;
};
Console.WriteLine(IncrementBy(10)); // 11
Console.WriteLine(IncrementBy(10, 20)); // 30

var IncrementByWithOffset = (int source, int increment = 1, int offset = 100) =>
{
    return source + increment + offset;
};
Console.WriteLine(IncrementByWithOffset(10)); // 111
Console.WriteLine(IncrementByWithOffset(10, 20)); // 130

#endregion


#region static

static async Task AsyncStreams ()
{
    foreach (var prod in GetProducts())
    {
        Console.WriteLine(prod);
    }
    Console.WriteLine("---");

    await foreach (var prod in GetProductsAsync())
    {
        Console.WriteLine(prod);
    }
}


static IEnumerable<string> GetProducts()
{
    yield return "Getting Products...";
    yield return "Product 1";
    yield return "Product 2";
    yield return "Product 3";
}

static async IAsyncEnumerable<string> GetProductsAsync() // c#8: asynchronous version of the existing IEnumerable
{
    yield return "Getting Products...";
    await Task.Delay(2000);
    yield return "Product 1";
    await Task.Delay(2000);
    yield return "Product 2";
    await Task.Delay(2000);
    yield return "Product 3";
}

static void LocalFunctions()
{
    var numbers = new int[] { 5, 6, 4 };
    AddNumbers();

    void AddNumbers()
    {
        numbers[2] = 0;
        foreach (var no in numbers)
        {
            Console.WriteLine(no);
        }
    }

    string firstName = "Clark";
    string lastName = "Kent";
    Console.WriteLine(GetName(firstName, lastName));
    static string GetName(string first, string last) => first + "," + last;
}

static string DisplayPosition(Point point) => point switch
{
    (0, 0) => "Origin",
    var (x, y) when x > 0 && y > 0 => $"Current Position is {x},{y}",
    _ => "Unknow Position"
};

static string CheckPrices3(string city, string country, string language) => (city, country, language) switch // (city, country, language) is a tuple
{
    ("Vienna", "Austria", "German") => "Above Average",
    ("Paris", "France", "French") => "Pretty High",
    ("Paris", "USA", "English") => "Average",
    ("Paris", _, _) => "Pretty High",
    (_, _, _) => "Invalid Input"
};

static string CheckPrices2(string city, string country) => (city, country) switch // (city, country) is a tuple
{
    ("Vienna", "Austria") => "Above Average",
    ("Paris", "France") => "Pretty High",
    ("Paris", "USA") => "Average",
    ("Paris", _) => "Pretty High",
    (_, _) => "Invalid Input"
};

static string CheckPrices(City city) => city switch
{
    { CityName: Cities.Vienna } => $"{city.CityName} is above Average [cost of living]",
    { CityName: Cities.Budapest } => $"{city.CityName} is resonable [cost of living]",
    { CityName: Cities.Paris, CountryName: "USA" } => "Average",
    //{ CityName: Cities.Paris, CountryName: "France" } => "High",
    { CityName: Cities.Paris } => $"{city.CityName}, {city.CountryName} is high",
    _ => "City is not covered"
};

static string CheckMonth(Months month) => month switch
{
    Months.January => "Winter",
    Months.February => "Winter",
    Months.March => "Winter",
    Months.April => "Spring",
    Months.May => "Spring",
    Months.June => "Spring",
    Months.July => "Summer",
    Months.August => "Summer",
    Months.September => "Summer",
    Months.October => "Autumn",
    Months.November => "Autumn",
    Months.December => "Winter",
    _ => "Invalid Input"
};
static string CheckMonthOldWay(Months month)
{
    switch (month)
    {
        case Months.December:
        case Months.January:
        case Months.February:
        case Months.March:
            return "Winter";

        case Months.April:
        case Months.May:
        case Months.June:
            return "Spring";

        case Months.July:
        case Months.August:
        case Months.September:
            return "Summer";

        case Months.October:
        case Months.November:
            return "Autumn";

        default:
            return "Invalid input";
    }
}

static string CapitalizeJob(Employee employee) => employee.Job?.ToUpper() ?? String.Empty; // C#8: working with nulls

static bool ArmedByKnife(Character c)
{
    //return c != null && c.Weapon != null && c.Weapon.Name == "Knife";
    return c?.Weapon?.Name == "Knife"; // C#6: safe navigation operator
}

static void StraightPatternMatching(object o)
{
    if (o is int x || (o is string s && int.TryParse(s, out x))) { // C# 7: pattern matching
        Console.WriteLine($"X parsed with matching {x}");
    }
}
static class Extensions
{
    internal static void Add(this IList<Character> characters, Features.Type type)
    {
        characters.Add(new Character(type));
    }
}

#endregion

#region structs
public struct Coord
{
    public double X { get; set; }
    public double Y { get; set; }
    public readonly double Distance => Math.Sqrt(X * X + Y * Y);

    public readonly string DisplayPosition() =>
        $"({X},{Y}) is {Distance} from origin";
}

ref struct Car // : IDisposable
{
    public void Dispose()
    {
        Console.WriteLine("Dispose method called!");
    }

}
#endregion