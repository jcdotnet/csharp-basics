using Features;

Console.WriteLine("Collections: TO-DO");

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