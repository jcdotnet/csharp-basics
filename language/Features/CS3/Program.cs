using ClassLibrary;

// https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-version-history#c-version-30
Console.WriteLine("C#3 features");

// auto-implemented properties & object and collection initializers
// https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/auto-implemented-properties
// https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/object-and-collection-initializers
var book = new Book() { Title = "Harry Potter" , Author= "J. K. Rowling" };
Console.WriteLine(book.Title); // auto-implemented property

// List<string> fruits = new List<string>();
// fruits.Add("Apple");
// fruits.Add("Banana");

List<string> fruits = new List<string> { "Apple", "Banana" };
List<int> digits = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };