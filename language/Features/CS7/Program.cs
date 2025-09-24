using ClassLibrary;
using CS7;

// New Features in C#7
// https://devblogs.microsoft.com/dotnet/new-features-in-c-7-0/
Console.WriteLine("--- C# 7.0 Features ---");

// out
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/out
{
    string text = "123";
    int number;

    if (int.TryParse(text, out number))
    {
        Console.WriteLine(number); // 123
    }
}

// out variables (C#7.0)
{
    string text = "123";
    if (int.TryParse(text, out int number)) // out var number
    {
        Console.WriteLine(number); // 123
    }
}

// expression bodied members/methods (functions with single body statements or return value)
// https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/expression-bodied-members
{
    Student student = new Student();
    Console.WriteLine(student.GetNameLength());
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
