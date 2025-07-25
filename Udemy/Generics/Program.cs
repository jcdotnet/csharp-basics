// See https://aka.ms/new-console-template for more information
using Generics;

Console.WriteLine("-- Generic Classes ---");

var mySample = new Sample<int>(); // Sample<int> sample = new();
mySample.Field = 1;
Console.WriteLine(mySample.ShowInfo());
ShowValueAndType<int>(mySample.Field);

var charSample = new Sample<char>();
charSample.Field = 'a';
Console.WriteLine(charSample.ShowInfo());

var calc = new Calculator<int>();
Console.WriteLine($"Addition Result: {calc.Add(12, 15)}");

var doubleCalc = new Calculator<double>();
Console.WriteLine($"Addition Result: {doubleCalc.Add(12.5, 15.3)}");

doubleCalc.DynamicProperty = "12";
//Console.WriteLine($"DynamicProperty type: {doubleCalc.DynamicProperty.GetType()}");
ShowValueAndType<dynamic>(doubleCalc.DynamicProperty);

doubleCalc.DynamicProperty = true;
//Console.WriteLine($"DynamicProperty type: {doubleCalc.DynamicProperty.GetType()}");
ShowValueAndType<dynamic>(doubleCalc.DynamicProperty);

doubleCalc.DynamicProperty = calc;
//Console.WriteLine($"DynamicProperty type: {doubleCalc.DynamicProperty.GetType()}");
ShowValueAndType<dynamic>(doubleCalc.DynamicProperty);

// List<Sample<int>> =  

#region Methods

void ShowValueAndType<TVariable>(TVariable myVar)
{
    Console.WriteLine($"The value is {myVar} and the type is {myVar.GetType()}");
}

#endregion