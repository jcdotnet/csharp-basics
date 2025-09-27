// Queue (= first-in, first-out collection of objects)
// implements the IEnumerable<T> interface
// https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1?view=net-9.0
Console.WriteLine("Queue");

Queue<int> intQueue = new Queue<int>();

intQueue.Enqueue(10); // adds to the end
intQueue.Enqueue(20);
intQueue.Enqueue(40); // memory: beginning (front) 10 20 40 end

foreach (int i in intQueue) Console.WriteLine(i);

int number = intQueue.Dequeue(); // removes from the beginning // memory: beginning 20 40 end

foreach (int i in intQueue) Console.WriteLine(i);

// Peek: returns the element at the beginning without removing it
Console.WriteLine(intQueue.Peek());         // 20
Console.WriteLine(intQueue.Contains(20));   // True
