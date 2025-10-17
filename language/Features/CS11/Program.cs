// https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-11
using CS11;

Console.WriteLine("C# 11 features");

// feat: raw string literals
// https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-11#raw-string-literals
Console.WriteLine("feat: raw string literals");
string jsonDoubleQuotes = "{\r\n  \"name\": \"John Doe\",\r\n  \"age\": 40\r\n}";
Console.WriteLine(jsonDoubleQuotes);

string json = """
{
  "name": "John Doe",
  "age": 40
}
""";
Console.WriteLine(json);

string name = "Jane Doe";
int age = 30;
string stringInterpolation = $$"""
{
  "name": {{name}},
  "age": {{age}}
}
""";
Console.WriteLine(stringInterpolation);

// feat: pattern matching - list pattern
// matches sequence of elements in lists or arrays
Console.WriteLine("feat: list pattern");
int[] intValues = [ 10, 20, 100 ]; // collection init // array init = {10, 20, 100}

bool condition1 = intValues.Length == 3;
bool condition2 = intValues[0] == 10;
bool condition3 = intValues[1] < 50;
bool condition4 = intValues[2] == 50 || intValues[2] == 100;
Console.WriteLine(condition1 && condition2 && condition3 && condition4);

// list pattern
bool condition = intValues is [10, < 50 /*relational pattern */, 50 or 100 /* logical pattern*/];
Console.WriteLine(condition);  

// feat: pattern matching - slice pattern
// used within list pattern to match 0 or more consecutive elements
Console.WriteLine(intValues is [.., 100]);      // True
Console.WriteLine(intValues is [10, ..]);       // True
Console.WriteLine(intValues is [10, .., 100]);  // True

// feat: pattern matching - var pattern
// used within list pattern to capture and assign elements no variables
Console.WriteLine(intValues is [10, var b, var c] && b < c);    // True

// feat: file local types
// they are declared with the file access modifier, their visibility is
// restricted to the single source file where they have been declared
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/file

// feat: required members
// field or properties from classes or structs that must be initialized
// either in the costructor or in the object initializer 
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/required

//MyClassName myClassName = new MyClassName(); // required member error


// feat: Auto-default structs
// struct fields are initialized to their default values if they are not
// explicitly assigned in a constructor
Console.WriteLine("feat: auto-default structs");

MyStruct myStruct = new MyStruct();
Console.WriteLine(myStruct.X);      // 0
Console.WriteLine(myStruct.Y);      // 0
Console.WriteLine(myStruct.Name);   // null

// feat: ref fields in ref structs (= value types allocated on the stack)
// ref fields can hold references to other vars
Console.WriteLine("feat: ref fields");

int value = 10;
RefStruct refStruct = new RefStruct(ref value);

Console.WriteLine(refStruct.myField);   // 10
Console.WriteLine(value);               // 10

value = 100;                            // this affects myfield also
Console.WriteLine(refStruct.myField);   // 100


#region type declarations
struct MyStruct
{
    public int X;
    public int Y;
    public string Name;
}

ref struct RefStruct
{
    public ref int myField;
    public RefStruct(ref int myField)
    {
        this.myField = ref myField;
    }
}

#endregion