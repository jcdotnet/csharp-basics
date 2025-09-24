// https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/
using Delegate;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

Console.WriteLine("--- Delegates ---");
var methods = new DelegatesTest();
var calcMul = new Calculate(methods.Multiply);

calcMul(12, 12);
calcMul(13, 13);
calcMul.Invoke(14, 14);

var calcAdd = new Calculate(methods.Add);

calcAdd(12, 12);
calcAdd(13, 13);
calcAdd(14, 14);

// multi-cast delegates (contains references of multiple methods) 
Console.WriteLine("--- Delegate Multicasting ---");
var multiCalc = new Calculate(methods.Add);
multiCalc += methods.Multiply;

multiCalc(14, 14);

var circle = new Circle();
var circleDelegate = new CircleDelegate(circle.ShowInfo);
circleDelegate += circle.Area;
circleDelegate += circle.Perimeter;

circleDelegate.Invoke(15);

circleDelegate -= circle.Area;
circleDelegate?.Invoke(16);

// events (we can think of events like a notification system between classes)
// https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/events/
Console.WriteLine("--- Events ---");
// Subscriber subscriber = new Subscriber(); // Program.cs is the susbribe class now
Publisher publisher = new Publisher();

publisher.myEvent += (a, b) => a + b; // inline lambda exp // subscribe to the event

Console.WriteLine(publisher.RaiseEvent(10, 20)); // invoking the event
Console.WriteLine(publisher.RaiseEvent(10, 70));
Console.WriteLine(publisher.RaiseEvent(15, 15));

// Func (predefined generic-delegate, which can be used to create events quickly) 
// https://learn.microsoft.com/es-es/dotnet/api/system.func-1

// Action (predefined generic-delegate without return, which can be used to create events quickly) 
// https://learn.microsoft.com/en-us/dotnet/api/system.action
// we can change to
// public event Action<int, int> myEvent; in the publisher class
// change the RaiseEvent return type to void again 
// and the inline lambda exp above to something like: 
// publisher.myEvent += (a, b) => { int c = a + b; Console.Writeline(c);};

// Predicate (predefined generic-delegate with one parameter and boolean return)
// https://learn.microsoft.com/es-es/dotnet/api/system.predicate-1?view=net-9.0
// same, we can change the code to use a predicate delegate

// EventHandler
// https://learn.microsoft.com/en-us/dotnet/api/system.eventhandler?view=net-9.0
new Test().DoWork();

// Expression trees
// https://learn.microsoft.com/en-us/dotnet/csharp/advanced-topics/expression-trees/
Student s = new Student() { Id = 101, Name = "John Doe", Age = 21 };

Expression<Func<Student, bool>> expression = st => st.Age > 18 && st.Age < 25;
Func<Student, bool> myDelegate = expression.Compile(); 

bool result = myDelegate.Invoke(s);
Console.WriteLine(result);
