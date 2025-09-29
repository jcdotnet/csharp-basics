// https://learn.microsoft.com/en-us/dotnet/standard/base-types/regular-expressions
// https://learn.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference
// https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex?view=net-9.0

using System.Text.RegularExpressions;

Console.WriteLine("reg exs");

Regex digitRegex = new Regex("^[0-9]+$");

Console.Write("Enter one or more digits: ");
string input = Console.ReadLine();
Console.WriteLine(digitRegex.IsMatch(input) ? "Great!" : "Invalid input :(");

Regex intRegex = new Regex("^([+-]?[1-9]\\d*|0)$");

Console.Write("\nEnter an integer number: ");
input = Console.ReadLine();
Console.WriteLine(intRegex.IsMatch(input) ? "Great!" : "Invalid input :(");
