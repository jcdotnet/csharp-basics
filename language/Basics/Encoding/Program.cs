// https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding?view=net-9.0
// decimal number system // base 10  // [0-9]
// binary number system // base 2  // [0-1]
// octal number system // base 8  // [0-7]
// hexadecimal number system // base 16  // [0-9ABCDEF] where A=10, B=11, C=12, D=13, E=14, F=15
// 100 = 0b1100100 // 0o144 // 0x64

using System.Text;

int dec = 12;
Console.WriteLine(Convert.ToString(dec, 2));        // decimal to binary // 1100
Console.WriteLine(Convert.ToInt32("1100", 2));      // binary to decimal // 12
Console.WriteLine(Convert.ToInt32("1101", 2));      // binary to decimal // 13
Console.WriteLine(Convert.ToInt32("1100100", 2));   // binary to decimal // 100

// binary literal
int binary = 0b1100100;         // 100
Console.WriteLine(binary + 1);  // 101

Console.WriteLine(Convert.ToString(dec, 8));        // decimal to octal // 14
Console.WriteLine(Convert.ToString(100, 8));        // decimal to octal // 144
Console.WriteLine(Convert.ToInt32("14", 8));        // octal to decimal // 12

Console.WriteLine(Convert.ToString(dec, 16));        // decimal to hexadecimal // c
Console.WriteLine(Convert.ToString(100, 16));        // decimal to hexadecimal // 64
Console.WriteLine(Convert.ToInt32("64", 16));        // hexadecimal to decimal // 12

// hexadecimal literal
int hex = 0x64;                 // 100
Console.WriteLine(hex + 1);     // 101

// character encoding
// https://learn.microsoft.com/en-us/dotnet/standard/base-types/character-encoding-introduction
// ASCII // american standard code for info exchange // one-byte character // 128 caracters (0 to 127)
// https://www.ascii-code.com/
// https://commons.wikimedia.org/wiki/File:ASCII-Table-wide.svg
// https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.ascii?view=net-9.0
// UTF-16 (unicode) // universal code // 2/4-byte character // about 300k characters (unicode 17)
// https://en.wikipedia.org/wiki/List_of_Unicode_characters
char aChar = 'a';
Console.WriteLine((byte)aChar); // 97
Console.WriteLine((char)97);    // a
Console.WriteLine((char)65);    // A

byte[] bytes = new byte[128];
for (byte i = 0; i < bytes.Length; i++) bytes[i] = i;
Console.WriteLine(Encoding.ASCII.GetString(bytes));     // all ASCII characters

string sentence = "Hello, World!";
var sentenceBytes = Encoding.ASCII.GetBytes(sentence);
foreach (byte b in sentenceBytes) Console.WriteLine(b); // ASCII bytes from sentence

string pi       = "Π";
string spanish  = "El niño aprendió el número Π";
Console.WriteLine(pi);                                  // ? (Windows command line only)
Console.WriteLine(spanish);                             // El niño aprendió el número ?
Console.WriteLine("El niño aprendió el número \u03a0"); // El niño aprendió el número ?

sentenceBytes = Encoding.ASCII.GetBytes("ñóú");
foreach (byte b in sentenceBytes) Console.WriteLine(b); // ASCII bytes from Spanish characters // 63 63 63

sentenceBytes = Encoding.Unicode.GetBytes("ñóú");
foreach (byte b in sentenceBytes) Console.WriteLine(b); // Unicode bytes // 241 0 243 0 250 0

sentenceBytes = Encoding.Unicode.GetBytes("Aa");
foreach (byte b in sentenceBytes) Console.WriteLine(b); // Unicode bytes // 65 0 97 0

sentenceBytes = Encoding.Unicode.GetBytes(spanish);
foreach (byte b in sentenceBytes) Console.WriteLine(b); // Unicode bytes from the Spanish sentence