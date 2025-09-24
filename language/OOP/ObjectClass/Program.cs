// Base class: All classes inherits from the Object class
// https://learn.microsoft.com/en-us/dotnet/api/system.object?view=net-9.0

using ObjectClass;

Object o = new Person() { Name = "John Doe", Email = "johndoe@email.com" };

Console.WriteLine(o.Equals(new Person()));
Console.WriteLine(o.Equals(new Person() { Name = "John Doe", Email = "johndoe@email.com" }));
Console.WriteLine(o.GetHashCode());

Console.WriteLine(o.GetType()); // ObjectClass.Person

Console.WriteLine(o); // call to ToString() // if not implemented then ObjectClass.Person 
Console.WriteLine(o.ToString()); // same as above

// Boxing (convesion to value type to reference type) and Unboxing (reference to value)
// https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/types/boxing-and-unboxing
int x = 10;

// boxing
object xObj = x; // System.Object xObj = x;

//unboxing
int y = (int)xObj;