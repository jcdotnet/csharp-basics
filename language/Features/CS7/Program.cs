using ClassLibrary;
using CS7;

// New Features in C#7
// https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-version-history#c-version-70
// https://devblogs.microsoft.com/dotnet/new-features-in-c-7-0/
Console.WriteLine("--- C# 7.0 Features ---");

// out variables
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/out
// https://devblogs.microsoft.com/dotnet/new-features-in-c-7-0/#out-variables
// out variables are commonly used in TrySomething scenarios
{
    Console.WriteLine("Out variables before C#7");
    string text = "123";
    int number;

    if (int.TryParse(text, out number))
    {
        Console.WriteLine($"Parsed number: {number}"); // 123
    }
}

{
    // C#7.0 allows to declare an out variable directly within the method call,
    // eliminating the need to declare it beforehand.
    Console.WriteLine("Out variables in C#7");

    string text = "123";
    if (int.TryParse(text, out int number)) // out var number
    {
        Console.WriteLine($"Parsed number: {number}"); // 123
    }

    Point p = new Point() { X = 1, Y = 1 };
    p.GetCoords(out int xx, out int yy); // C#7: no need to declare xx and yy
    Console.WriteLine($"X: {xx}, Y:{yy}");

    string digit = "10";
    int.TryParse(digit, out int d); //
    Console.WriteLine();

}

// pattern matching
// https://devblogs.microsoft.com/dotnet/new-features-in-c-7-0/#pattern-matching
object obj = "hello";
if (obj is string s)
{
    Console.WriteLine($"String length: {s.Length}");
}

int intNum = 5;
if (intNum is 5)
{
    Console.WriteLine("Number is five.");
}

// local functions (C#7.0)
// https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/local-functions
void Hello(string message)
{
    Console.WriteLine("Hello " + message);
}
Hello("World");
{
    Student student = new Student();
    student.DisplayGrades(100, 70, 90, 100); // 100, 70, 90, 100, 90
}

// C#7.2
// https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-version-history#c-version-72
Console.WriteLine("--- C# 7.2 Features ---");

// read-only structures (to indicate that a struct is immutable)
Character c = new Character("Son Goku"); // name is initialized only one time in the constructor
// c.Name = "Test"; // compile-time error
c.PrintCharacterName();

// in parameter modifier (parameter that becomes read-only)
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/method-parameters#in-parameter-modifier
{
    void InParameterFunctionExample(in int num) // local function (C# 7.0)
    {
        // num = 100; // compiler error
        Console.WriteLine(num);
    }
    int num = 10;
    InParameterFunctionExample(in num); // 10
}

// ref returns (C#7.3, returns a variable as reference)
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/jump-statements#ref-returns
{
    Student student = new Student();
    ref int grade = ref student.RefReturnExample();
    grade = 80;
    student.PrintGrade(); // 80
}
Console.ReadKey();
