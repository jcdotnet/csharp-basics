// Arrays (= reference type = multiple variables of the same type)
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/arrays
// type[] referenceVariableName  = new type[size]
// array reference variable = memory address of the first element
using Classes;

Console.WriteLine("Arrays");


int[] arr = new int[2]; // init array

// get(access) the array values
Console.WriteLine(arr[0]); // 0
Console.WriteLine(arr[1]); // 0

// set the array values
arr[0] = 1;
arr[1] = 2;
Console.WriteLine(arr[0]); // 1
Console.WriteLine(arr[1]); // 2
// Console.WriteLine(arr[2]); // runtime exception (System.IndexOutOfRangeException)

// int[] intArr = new int[2] { 1, 2 }; // init array and values
int[] intArr = { 1, 2 }; // same as above
// string[] sArr = new string[3] { "One", "Two", "Three" }; // init array and values
string[] sArr = { "One", "Two", "Three" }; // same as above

// for and foreach
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/iteration-statements
for (int i = 0; i < sArr.Length; i++)
{
    Console.WriteLine(sArr[i]);
}

foreach (int i in intArr) {  Console.WriteLine(i); }
foreach (var s in sArr) { Console.WriteLine(s); }

//System.Array (every array is treated as an object from the Array class)
// https://learn.microsoft.com/en-us/dotnet/api/system.array?view=net-9.0
double[] doubleArr = { 10, 10, 20, 30, 50 };
Console.WriteLine($"30 is found at index {Array.IndexOf(doubleArr, 30)}"); // 3
Console.WriteLine($"60 is found at index {Array.IndexOf(doubleArr, 60)}"); // -1 (not found)
int index = Array.IndexOf(doubleArr, 10, 1); // starting from index 1 (included)
Console.WriteLine($"Second ocurrence of 10 is found at index {index}"); // 1

// BinarySearch (better performance but only works in sorted arrays)
Console.WriteLine($"30 is found at index {Array.BinarySearch(doubleArr, 30)}"); // 3
Console.WriteLine($"60 is found at index {Array.BinarySearch(doubleArr, 60)}"); // -6 (not found)

Array.Clear(doubleArr, 0, doubleArr.Length); // clears all the array

foreach (var d in doubleArr) { Console.WriteLine(d); } // 0 0 0 0 0 

// doubleArr = new double[] { 10, 20, 30, 40, 50 };
doubleArr = [ 10, 20, 30, 40, 50 ]; // same (collection expression)

Array.Clear(doubleArr, 2, 2); // clears 2 elements starting from index 2

foreach (var d in doubleArr) { Console.WriteLine(d); } // 10 20 0 0 50

Array.Resize(ref doubleArr, 6);
foreach (var d in doubleArr) { Console.WriteLine(d); } // 10 20 0 0 50 0

doubleArr = [10, 0, 30, 20, 50, 40];
Array.Sort(doubleArr); // ascending order
foreach (var d in doubleArr) { Console.WriteLine(d); } // 0 10 20 30 40 50
Array.Sort(sArr); // ascending order
foreach (var s in sArr) { Console.WriteLine(s); } // One Three Two

Array.Reverse(sArr); // reverses array
foreach (var s in sArr) { Console.WriteLine(s); } // Two Three One

// multidimensional arrays
// https://www.w3schools.com/cs/cs_arrays_multi.php

// array of objects
Employee employee1 = new Employee() { Id = 1, Name = "John Doe"};
Employee employee2 = new Employee() { Id = 2, Name = "Jane Doe" };
Employee employee3 = new Employee() { Id = 3, Name = "Mike Doe" };

Employee[] employees = new Employee[] { employee1, employee2, employee3 };

// also
Employee[] employees2 = new Employee[]
{
    new Employee() { Id = 1, Name = "John Doe"},
    new Employee() { Id = 2, Name = "Jane Doe" },
    new Employee() { Id = 3, Name = "Mike Doe" }
};

// also (as we learned)
Employee[] employees3 =
[
    new Employee() { Id = 1, Name = "John Doe"},
    new Employee() { Id = 2, Name = "Jane Doe" },
    new Employee() { Id = 3, Name = "Mike Doe" }
];

foreach (var employee in employees) Console.WriteLine(employee);

// CopyTo (shallow copy)
// https://learn.microsoft.com/en-us/dotnet/api/system.array.copyto?view=net-9.0

Employee[] copiedArray = new Employee[3];
employees.CopyTo(copiedArray, 0); // start index 0

// employees.CopyTo(bestEmployees, 2); // System.ArgumentException

// Clone (shallow copy)
// https://learn.microsoft.com/es-es/dotnet/api/system.array.clone?view=net-9.0

var bestEmployees = employees.Clone(); // returns object
// foreach (var employee in bestEmployees) // compile-time array

var clonedArray = (Employee[])employees.Clone(); // returns object

// Deep Copy (creates new object for each element)

var deepCopy = new Employee[employees.Length];
for (int i = 0; i < employees.Length; i++)
{
    deepCopy[i] = (Employee)employees[i].Clone();
}

employees[0].Position = "Developer";

Console.WriteLine("Copied Array:");
foreach (var employee in copiedArray) Console.WriteLine(employee);

Console.WriteLine("Cloned Array:");
foreach (var employee in clonedArray) Console.WriteLine(employee);

Console.WriteLine("Deep Copied Array:");
foreach (var employee in deepCopy) Console.WriteLine(employee);