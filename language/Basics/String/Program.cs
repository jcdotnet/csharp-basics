// String (= array of Unicode characters = char[] = immutable)
// https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/strings/
// https://learn.microsoft.com/en-us/dotnet/api/system.string?view=net-9.0

using System;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

Console.WriteLine("strings");

// String hello = "Hello, World!";
string hello = "Hello, World!"; // string = keyword for System.String 
Console.WriteLine(hello);           // Hello, World!
Console.WriteLine(hello.Length);    // 13
Console.WriteLine(hello[0]);        // H
Console.WriteLine(hello[12]);       // !

string hello2 = hello;
// the following will create a new object and store its memory location in hello,
// therefore, hello and hello2 will reference different memory locations
hello = "Hello, Beautiful World!";   
Console.WriteLine(hello2);          // Hello, World!

string hello3 = hello;              // hello and hello3 will store the same memory location
hello2 = hello3;                    // hello2 and hello3 will store the same memory location
Console.WriteLine($"{hello} {hello2} {hello3}");

Console.WriteLine(hello.ToUpper());
Console.WriteLine(hello.ToLower());

Console.WriteLine(hello.Substring(7));                  // Beautiful World! 
Console.WriteLine(hello.Substring(7, 9));               // Beautiful

Console.WriteLine(hello.Replace('H', 'B'));             // Bello, Beautiful World!
Console.WriteLine(hello.Replace('o', 'x'));             // Hellx, Beautiful Wxrld!
Console.WriteLine(hello.Replace("Beautiful", "My"));    // Hello, My World!

var words = hello.Split(' ');
foreach (var word in words) Console.WriteLine(word);    // Hello, // Beautiful // World!

Console.WriteLine(string.Join('-', words));             // Hello,-Beautiful-World!

foreach (var word in words) 
    Console.WriteLine(word.TrimEnd(','));               // Hello, // Beautiful World!

words = hello.Split(',');
foreach (var word in words) Console.WriteLine(word);    // Hello  //  Beautiful World!
foreach (var word in words) 
    Console.WriteLine(word.TrimStart());                // Hello  // Beautiful World!

var chars = hello.ToCharArray();
foreach (var c in chars) Console.WriteLine(c);

string hello4 = "Hello, Beautiful World!";

Console.WriteLine(hello == hello4);                     // True
Console.WriteLine(hello.Equals(hello4));                // True

Console.WriteLine(hello.StartsWith('H'));               // True
Console.WriteLine(hello.StartsWith("Hello"));           // True
Console.WriteLine(hello.StartsWith(hello4));            // True
Console.WriteLine(hello.EndsWith("World!"));            // True

Console.WriteLine(hello.Contains("Beautiful"));         // True
Console.WriteLine(hello.Contains(hello4));              // True

Console.WriteLine(hello.IndexOf("Beautiful"));          // 7
Console.WriteLine(hello.IndexOf('e'));                  // 1
Console.WriteLine(hello.IndexOf('z'));                  // -1
Console.WriteLine(hello.IndexOf('e', 2));               // 8
Console.WriteLine(hello.LastIndexOf('e'));              // 8

Console.WriteLine(string.IsNullOrEmpty(""));            // True
Console.WriteLine(string.IsNullOrEmpty(" "));           // False
Console.WriteLine(string.IsNullOrWhiteSpace(" "));      // True

string city = "Málaga", country = "Spain";
Console.WriteLine(string.Format("{0} is a city of {1}", city, country));
Console.WriteLine($"{city} is a city of {country}");

Console.WriteLine(hello.Insert(7, "Super"));                            // Hello, SuperBeautiful World!
Console.WriteLine(hello.Insert(hello.IndexOf("Beautiful"), "Super"));   // Hello, SuperBeautiful World!
Console.WriteLine(hello.Remove(7, 10));                                 // Hello, World!

char[] vowels = ['A', 'a', 'E', 'e', 'I', 'i', 'O', 'o', 'U', 'u'];
int vowelsCount = 0;
for (int i=0; i< hello.Length; i++)
{
    Console.WriteLine(hello[i]);
    if (Array.IndexOf(vowels, hello[i]) != -1) vowelsCount++;
}
Console.WriteLine(vowelsCount);                                         // 8
Console.WriteLine(hello.Count(ch => true));                             // 23 (LINQ)
Console.WriteLine(hello.Count(ch => Array.IndexOf(vowels, ch) != 0));   // 8

// String Builder (Mutable)
// https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder?view=net-9.0
string[] sentenceWords = ["Too","bad", "I", "hid", "a", "boot"];

StringBuilder sb = new StringBuilder();

foreach (string word in sentenceWords) sb.Append(word).Append(' ');

Console.WriteLine(sb.ToString().Trim());