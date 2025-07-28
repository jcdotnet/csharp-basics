using Collections;
using Features;

Console.WriteLine("Collections: TO-DO");

Console.WriteLine("Collections Exercises");
SalesAnalysis sales = new SalesAnalysis(new List<string>() 
{ 
    "item1: 299",
    "item2: 149",
    "item3: 999",
});
Console.WriteLine(sales.ComputeTotalIncome());

Console.WriteLine("LINQ");
// Numbers list
List<int> list = new List<int>() { 23, 75, 30, 34, 98, 27, 0};

// Filtering the list using the where clause
var filteredNumbers = list.Where(number => number > 50);

foreach (int number in filteredNumbers)
{
    Console.WriteLine(number);
}

List<Student> students = new List<Student>() // maybe todo: import from a project with all common classes 
{
    new Student("John", 20),
    new Student("Jane", 17),
    new Student("Daniel", 21),
};
// projection
var studentNames = students.Select(student => student.Name);

var adultNames = students.Where(student => student.Age > 18).Select(student => student.Name);

foreach (string name in adultNames)
{
    Console.WriteLine(name);
}

// aggregation
// int totalSum = list.Sum();
int totalSum    = students.Sum(sum => sum.Age);
double mean     = students.Average(sum => sum.Age);
int count       = students.Count();

// Ordering
var orderedStudents = students.OrderBy(x => x.Name);
var ages            = students.OrderByDescending(x => x.Age);