using ClassLibrary;
using ProductInfo = (string Name, double Price, bool Discount); // alias for a product tuple
using NullableInt = int?;                                       // alias to improve readability

// https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-12
// https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-version-history#c-version-12
Console.WriteLine("C#12 features");

// feat - primary constructors
var person = new Person("John", "Doe", 40); // using primary constructor in the Person class
Console.WriteLine(person);

// feat - collection expressions
int[] array = [1, 2, 3, 5, 7]; // array
// List<int> list = new List<int>() { 1, 3, 3, 5, 7 }; // collection initializer (before C#12)
List<int> list = [1, 2, 3, 5, 7]; // list
List<Person> people = [
    new Person("John", "Doe", 40),
    new Person("Jane", "Doe", 40),
];
foreach (var element in list) Console.Write(element);

// feat - default lambda parameters
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

// feat - ref readonly parameters (not very used in real world projects)
// prevent accidental modifications of the data passed
// used when you need to access data passed by ref but you don't intend to modify it within the method
var PrintNumber = (ref int number) =>
{
    Console.WriteLine(number);
    number++; // Compile error if ref readonly int number
};
int myNumber = 32;
PrintNumber(ref myNumber);
PrintNumber(ref myNumber);

// feat - alias any type
ProductInfo product = ("Laptop", 1999.99, true);
Console.WriteLine($"{product.Name} costs {product.Price} euros {(product.Discount ? "with discount":"")}");

NullableInt x = 10;