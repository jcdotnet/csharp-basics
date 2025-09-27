// List (= strongly typed list of objects that can be accessed by index)
// implements the IEnumerable<T>, ICollection<T> and IList<T> interfaces
// https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1?view=net-9.0

using ClassLibrary;

Console.WriteLine("Lists");

// List<int> intList = new List<int>() { 10, 20, 30}; // collection initializer
List<int> intList = [10, 20, 30]; // simplified

foreach (int i in intList) Console.WriteLine(i);

Console.WriteLine(intList[0]); // iterators in C++ for lists // intList.get(0) in Java

// Count
Console.WriteLine(intList.Count); // get number of elements (size() in C++)
for (int i = 0; i < intList.Count; i++) Console.WriteLine(intList[i]);

// Capacity
// get/set total number of elements the internal data structure can hold without resizing.
// capacity() in C++
Console.WriteLine(intList.Capacity); // 3/4

List<int> intList10 = new List<int>(10) { 10, 20, 30 };
Console.WriteLine(intList10.Count); // 3
Console.WriteLine(intList10.Capacity); // 10

// arrays are immutable and lists are resizable
intList.Add(40); // adds to the end of the list
intList.AddRange([ 50, 60 ]); // add collection to the end of the list
Console.WriteLine(intList.Count); // 6
Console.WriteLine(intList.Capacity); // 6/8

intList.Insert(1, 100); // 10, 100, 20, 30, 40, 50, 60
intList.InsertRange(2, [ 120, 150, 200 ]); // 10, 100, 120, 150, 200, 20, 30, 40, 50, 60

intList.Remove(30); // 10, 100, 120, 150, 200, 20, 40, 50, 60
intList.RemoveAt(2); // 10, 100, 150, 200, 20, 40, 50, 60
// intList.RemoveAt(20); // 10, 100, 150, 200, 20, 40, 50, 60 // System.ArgumentOutOfRangeException

intList.RemoveRange(1, 2); // 10, 200, 20, 40, 50, 60
intList.RemoveAll(n => n > 100); // 10, 20, 40, 50, 60

for(int i = 0; i < intList.Count; i++) Console.WriteLine(intList[i]);
Console.WriteLine(intList.Capacity); // 12

intList.Clear(); // removes all elements from the list
Console.WriteLine(intList.Count); // 0
Console.WriteLine(intList.Capacity); // 12
intList.TrimExcess(); // sets capacity to the actual numbers of the list
Console.WriteLine(intList.Capacity); // 0

intList.AddRange([10, 100, 30, 20, 50]);

// IndexOf (linear search)
Console.WriteLine(intList.IndexOf(30));     // 2
Console.WriteLine(intList.IndexOf(100));    // 1
Console.WriteLine(intList.IndexOf(120));    // -1
intList.Add(30);
int index = intList.IndexOf(30);
Console.WriteLine(intList.IndexOf(30, index+1)); // 5 // second ocurrence of 30

// BinarySearch (better performance for large collections, requires the collection to be sorted)
// Contains
Console.WriteLine(intList.Contains(30));    // True
Console.WriteLine(intList.Contains(100));   // True
Console.WriteLine(intList.Contains(120));   // False

intList.Reverse();
foreach (int i in intList) Console.WriteLine(i); // 30, 50, 20, 30, 100, 10
Console.WriteLine(intList.IndexOf(30)); // 0

intList.Sort();
foreach (int i in intList) Console.WriteLine(i); // 10, 20, 30, 30, 50, 100

int[] intArray = intList.ToArray(); // [ ... int list]
// int[] intArray = [.. intList]; // simplified

// ForEach
intList.ForEach(i => Console.WriteLine(i+10));

// more on lists
Console.WriteLine(intList.Exists(i => i > 100)); // False
Console.WriteLine(intList.Find(i => i > 10 && i < 100)); // 20
Console.WriteLine(intList.FindIndex(i => i > 10 && i < 100)); // 1
Console.WriteLine(intList.FindLast(i => i > 10 && i < 100)); // 50
Console.WriteLine(intList.FindLastIndex(i => i > 10 && i < 100)); // 4

var numbers = intList.FindAll(i => i > 10 && i < 100); // 20, 30, 30, 50
foreach (int i in numbers) Console.WriteLine(i);

var stringList = intList.ConvertAll<string>(n => { return Convert.ToString(n); });
foreach (string i in stringList) Console.WriteLine(i);

Console.ReadLine();

SalesAnalysis sales = new SalesAnalysis(new List<string>()
{
    "item1: 299",
    "item2: 149",
    "item3: 999",
});
Console.WriteLine(sales.ComputeTotalIncome());

Console.ReadLine();

List<Product> products = new List<Product>();
string input;
do
{
    Console.Write("Enter Product Id: ");
    int id = int.Parse(Console.ReadLine());
    Console.Write("Enter Product Name: ");
    string name = Console.ReadLine();
    Console.Write("Enter Product Price: ");
    double price = double.Parse(Console.ReadLine());
    Console.Write("Enter Product MFG Date (yyyy/MM/dd): ");
    DateTime date = DateTime.Parse(Console.ReadLine());

    Product product = new Product() { Id = id, Name= name, Price = price, Date = date };
    products.Add(product);

    Console.Write("Do you want to add another product (Y/n): ");
    input = Console.ReadLine();

} while (input?.ToLower() != "n");

foreach (Product p in products) 
    Console.WriteLine($"{p.Name}. Price: {p.Price}. MFG: {p.Date.ToShortDateString()}");