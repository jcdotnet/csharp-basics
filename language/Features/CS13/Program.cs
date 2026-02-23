// See https://aka.ms/new-console-template for more information
using ClassLibrary;

Console.WriteLine("C#13 features");

// feat :: params collections
// https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-13#params-collections
var validator = new ValidationService();
validator.ValidateAll_CS13("John", "Jane", "johndoe@example.com");

List<string> emailList = ["johndoe@example.com", "janedoe", "janedoe@example.com"];
var result = validator.ValidateEmails(emailList);
if (!result.IsValid)
{
    foreach (var error in result.Errors) Console.WriteLine(error);
}

Console.WriteLine();

// feat :: partial properties and indexers
var person = new Person("John", "Doe", 20);
person.Address = "123 Main Street, Anytown, US 12345";
Console.WriteLine(person);
Console.WriteLine($"Value from indexer: {person[0]}");