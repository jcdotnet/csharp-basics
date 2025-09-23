// See https://aka.ms/new-console-template for more information
Console.WriteLine("Types in C#");
// Value types
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/value-types
// primitive types are value types and built-in single types
// primitive types are the most basic data types that are directly supported by the language

// Integral numeric types
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/integral-numeric-types
// Floating-point numeric types
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types
// bool type that represents a Boolean value
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool
// char type that represents a Unicode UTF-16 character
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/char

// Reference types
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/reference-types
// built-in types
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/reference-types

// type conversion
// https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/types/casting-and-type-conversions

{
    // Implicit casting (from lower numerical type to higher numerical type -requires more bytes - )
    sbyte a = 10;
    char b = 'A';
    int c;
    c = a; // implicit type casting // 10
    c = b; // implicit type casting // 65 (ASCII value of A)
}
{
    // Explicit casting (from higher numerical to lower numerical -requires less number of bytes --)
    // lossy convertion: ocucurs when the size of the destination is not enough to store the value
    int a = 100;
    byte b;
    float c;
    c = a; // implicit works (100)
    c = (float)a; // 100
    b = (byte)a; // 10

    // lossy
    a = 500;
    b = (byte)a; // 244 
}
{
    // Parsing / TryParsing (from string to numerical)
    string a = "100";
    int.Parse(a); // 100
    double.Parse(a);

    a = "10 0";
    //int.Parse(a); // runtime error

    string s;
    Console.Write("Enter an integer: ");
    s = Console.ReadLine();
    bool conversion = int.TryParse(s, out int n);
    if (conversion) Console.WriteLine(n);
    else Console.WriteLine("Invalid integer");
}
{
    // Conversion Methods (from primitive to primitive)
    // System.Convert.ToXXX
    int x = 100;
    decimal d = 11.56M;
    Console.WriteLine(Convert.ToString(x)); // 100 (string)
    Console.WriteLine(Convert.ToString(d)); // 11,56
}
