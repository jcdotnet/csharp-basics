// Stack (= last-in-first-out (LIFO) non-generic collection of objects)
// implements the IEnumerable<T> interface
// https://learn.microsoft.com/en-us/dotnet/api/system.collections.stack?view=net-9.0
Console.WriteLine("Stack");

Stack<int> intStack = new Stack<int>();

intStack.Push(10); // inserts at the top
intStack.Push(20);
intStack.Push(40); // memory: top 40 20 10 bottom

foreach (int i in intStack) Console.WriteLine(i);

intStack.Pop(); // removes from the top // memory: top 20 10 bottom

foreach (int i in intStack) Console.WriteLine(i);

// Peek: returns the top element without removing it (top() in C++)
Console.WriteLine(intStack.Peek());         // 20
Console.WriteLine(intStack.Contains(20));   // True

