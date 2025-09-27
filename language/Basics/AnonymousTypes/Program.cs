// https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/anonymous-types
using AnonymousTypes;
using System;

Console.WriteLine("Anonymous Types");

// compiler automatically creates an anonymous class with the Amount and Message Propertie
var v = new { Amount = 108, Message = "Hello" };
Console.WriteLine(v.Amount + v.Message);

// Product Example
Product product = new Product();
{
    // Product Basic Info
    // compiler automatically creates an anonymous class with ProductName and ProductPrice Properties
    var productExplicit = new { ProductName = product.Name, ProductPrice = product.Price };

    Console.WriteLine(productExplicit.ProductName);
    Console.WriteLine(productExplicit.ProductPrice);
}
{
    // Product Basic Info (without providing properties names)
    // compiler automatically creates an anonymous class with Name and Price Properties
    var productImplicit = new { product.Name, product.Price };
    Console.WriteLine(productImplicit.Name);
    Console.WriteLine(productImplicit.Price);
}

var products = new List<Product>();
products.Add(new Product());
products.Add(new Product() { Name = "Affordable Product", Color = "Green", Price = 100 });
products.Add(new Product() { Name = "Expensive Product", Color = "Red", Price = 10000 });

var productQuery =
    from prod in products
    select new { prod.Color, prod.Price };

foreach (var p in productQuery)
{
    Console.WriteLine("Color={0}, Price={1}", p.Color, p.Price);
}

// Person Example with explicit member names.
var personExplicit = new { FirstName = "John", LastName = "Doe" };

// Person Example with inferred member names 
var firstName = "John";
var lastName = "Doe";
var personInferred = new { firstName, lastName }; // projection initializers

// Both create equivalent anonymous types with the same property names.
Console.WriteLine($"Explicit: {personExplicit.FirstName} {personExplicit.LastName}");
Console.WriteLine($"Inferred: {personInferred.firstName} {personInferred.lastName}");

var title = "Software Engineer";
var department = "Engineering";
var salary = 75000;

// Using projection initializers.
var employee = new { title, department, salary };

// Equivalent to explicit syntax:
// var employee = new { title = title, department = department, salary = salary };

Console.WriteLine($"Title: {employee.title}, Department: {employee.department}, Salary: {employee.salary}");

Console.ReadLine();

// Nested anonymous types
Console.WriteLine("Nested Anonymous Types");

var person =  new { firstName, lastName, Address = new { Street = "Evergreen Terrace", City = "Springfield"} };
Console.WriteLine($"{person.firstName} {person.lastName} {person.Address.Street}");

// Anonymous arrays
Console.WriteLine("Anonymous Arrays");
var people = new[] { 
    new { FirstName = "Homer", LastName = "Simpson", Address = new { Street = "Evergreen Terrace", City = "Springfield"} },
    new { FirstName = "Marge", LastName = "Simpson", Address = new { Street = "Evergreen Terrace", City = "Springfield"} },
};

foreach (var p in people) Console.WriteLine($"{p.FirstName} {p.LastName} {p.Address.Street}");