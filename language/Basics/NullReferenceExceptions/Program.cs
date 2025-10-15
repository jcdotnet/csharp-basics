// reference types
// the default value of reference types (classes, interfaces, etc) is null (no memory address)
// thus, reference types are nullable types by default
// we have to handle null exceptions
// to help us with that, C# introduces "nullable reference types" and "non-nullable reference types"
// to allow the compiler to perform analysis (and give warnings) for purpose of null-safety 
// this help from the compiler helps developers to AVOID / PREVENT null exceptions
// https://learn.microsoft.com/en-us/dotnet/csharp/nullable-references

// value types
// the default value of value types (structures, enums, etc) is NOT null (default type value)
// thus, value types are non-nullable types by default
// in order to support null we can convert value types to nullable value types by using ? or Nullable (value type)
// Nullable<int> x = null / 10;
// int?x = null / 10;
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/nullable-value-types

using NullReferenceExceptions;

/**
 * nullable value types
 */
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

/**
 * nullable reference types
 */
Person person3;                             // non-nullable reference type (default)
Person? person4;                            // nullable reference type (adding ?)

person3 = new Person();                     // no warnings (person3 is not null here)
person4 = new Person();                     // warning: person4 may be null here

Console.WriteLine(person3.FirstName);       // no warnings (FirstName is not null here)
Console.WriteLine(person3.LastName);        // warning: LastName may be null here
Console.WriteLine(person4.FirstName);       // warning: LastName may be null here
Console.WriteLine(person4.LastName);        // warning: LastName may be null here

person3.Children = 3;

// warning here: nullable value type may be null
// warning goes away after adding the line above
Console.WriteLine(person3.Children.Value);  // 3                              

if (person3.Children.HasValue)              
{
    // no warnings: checked null in condition
    Console.WriteLine(person3.Children.Value); 
}

bool isNotNull = person3.Children != null;

if (isNotNull)            
{
    // warning (compiler is not sure whether the variable is null or not)
    Console.WriteLine(person3.Children.Value);
    // we as developers are 100% sure that the variable is not null
    // so we use the null forgiving operator to convince the compiler
    Console.WriteLine(person3!.Children.Value); // no warnings 
}