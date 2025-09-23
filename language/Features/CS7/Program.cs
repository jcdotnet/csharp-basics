using ClassLibrary;

// New Features in C#7
// https://devblogs.microsoft.com/dotnet/new-features-in-c-7-0/

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

// in parameter modifier (C#7.2, parameter that becomes read-only)
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
