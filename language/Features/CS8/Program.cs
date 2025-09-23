using ClassLibrary;
// https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-version-history#c-version-80
// https://learn.microsoft.com/en-us/shows/connect-microsoft-connect--2018/d140

// static local functions
// https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/local-functions
{
    Student student = new Student();
    student.DisplayGradesWithStaticFunction(100, 80); // 100, 80, 90
}