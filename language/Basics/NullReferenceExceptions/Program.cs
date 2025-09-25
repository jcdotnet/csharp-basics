// reference types
// the default value of reference types (classes, interfaces, etc) is null (no memory address)
// thus, reference types are nullable types by default
// we have to handle null exceptions
// https://learn.microsoft.com/en-us/dotnet/csharp/nullable-references

// value types
// the default value of value types (structures, enums, etc) is NOT null (default type value)
// thus, value types are non-nullable types by default
// in order to support null we can convert value types to nullable value types by using ? or Nullable (value type)
// Nullable<int> x = null / 10;
// int?x = null / 10;
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/nullable-value-types

using NullReferenceExceptions;

Person person1 = new Person() { Children = 3};
Console.WriteLine(person1.Children);

// Person person2 = new Person() { Children = 0 }; // we want Children to be nullable
Person person2 = new Person() { Children = null };
Console.WriteLine(person2.Children);

// prints the value only if Children is not null
// 1 by checking against null
if (person2.Children != null)
{
    Console.WriteLine(person2.Children);
}

// 2 HasValue property of Nullable value types
if (person2.Children.HasValue) 
{
    Console.WriteLine(person2.Children.Value);
}

// 3 HasValue and ternary conditional operator
Console.WriteLine(person2.Children.HasValue ? person2.Children.Value : 0);

// 4 null coalescing operator (??)
Console.WriteLine(person2.Children ?? 0);

// 5 null coalescing operator (?.)
Console.WriteLine(person2?.Children);

Person person3 = null; // classes are nullable (can be null)
Console.WriteLine(person3?.Children);

// prints the value only if Children is not null
if ((person3?.Children).HasValue)
{
    Console.WriteLine(person3.Children.Value);
}

Person? person4 = null; 
Console.WriteLine(person4.Children);

// prints the value only if Children is not null
if (person4.Children.HasValue)
{
    Console.WriteLine(person4.Children.Value);
}