// Generic classes and methods
// https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/generics
using Generic;

Console.WriteLine("Generics in C#");

User<int> user1 = new User<int>();
user1.RegistrationStatus = 1;
User<bool> user2 = new User<bool>();
user2.RegistrationStatus = false;
User<string> user3 = new User<string>();
user3.RegistrationStatus = "REGISTERED";

var mySample = new Sample<int>(); // Sample<int> sample = new();
mySample.Field = 1;
Console.WriteLine(mySample.ShowInfo());

var charSample = new Sample<char>();
charSample.Field = 'a';
Console.WriteLine(charSample.ShowInfo());

// List<Sample<int>> =  

Pair<string, int> pair1 = new Pair<string, int>();
pair1.Print(); // null (reference) 0 (int)
pair1.First = "John";
pair1.Second = 33;
pair1.Print(); // John 33

Pair<string, int> pair2 = new Pair<string, int>() { First = "Jane", Second = 21};
pair2.Print(); // Jane 21

Pair<string, int> pair3 = new Pair<string, int>("Alice", 30);
pair3.Print(); // Alice 30

var calc = new Calculator<int>();
Console.WriteLine($"Addition Result: {calc.Add(12, 15)}");

var doubleCalc = new Calculator<double>();
Console.WriteLine($"Addition Result: {doubleCalc.Add(12.5, 15.3)}");

doubleCalc.DynamicProperty = "12";
Console.WriteLine($"DynamicProperty type: {doubleCalc.DynamicProperty.GetType()}");

doubleCalc.DynamicProperty = true;
Console.WriteLine($"DynamicProperty type: {doubleCalc.DynamicProperty.GetType()}");

doubleCalc.DynamicProperty = calc;
Console.WriteLine($"DynamicProperty type: {doubleCalc.DynamicProperty.GetType()}");

// Generic constraints
// https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters
GradeBook<GraduateStudent> gradeBook = new GradeBook<GraduateStudent>();
gradeBook.Student = new GraduateStudent();
gradeBook.Print();