// https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-version-history#c-version-10
Console.WriteLine("C#10 features");

// feat: file scoped namespaces
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/namespace
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-10.0/file-scoped-namespaces
//namespace School // block scoped namespace
//{
//}
//namespace School; // file scoped namespace
//class Student {}
//class Teacher {}

// feat: global using directives
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/using-directive
// global using namespace_name // namespace_name will be imported in the current project files

// feat: structs with parameterless constructors (see Character struct in C# 7 project)

// feat: record structs (recordas that are structs after compilation instead of classes)
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/record
var person = new PersonRecord("John", 20); // new is dummy keyword here (as learned with structs)
person.Age = 40; // read-write (as opposed to read-only properties in class structs)
Console.WriteLine(person);

// feat: lambda expressions return type
// Lambda expressions can declare a return type when the compiler can't infer it.
// Return Type (Parameters List) => Return Value
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions#explicit-return-type
var GetMessage  = () => "Hello World";      // return type is Func<string> (inferred)
var GetResult   = object (double grade) =>  // also Func<double, string> GetResult = object(grade)
{
    if (grade > 50) return "Passed";
    else return 0;
};
Console.WriteLine(GetMessage());
Console.WriteLine(GetResult(100));

// impro: constant interpolated strings
// const DataType variableName = $"{constant_string}";
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-10.0/constant_interpolated_strings
Console.WriteLine(ConstantExample.ApiUrl);

#region type declarations

public record struct PersonRecord( string? Name, int? Age);

public class ConstantExample { 
    public const string BaseUrl = "http://example.com";
    public const string ApiUrl = $"{BaseUrl}/api";
}

#endregion