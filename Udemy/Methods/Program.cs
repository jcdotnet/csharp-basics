// See https://aka.ms/new-console-template for more information
using Methods;

Console.WriteLine("-- Callback Methods ---");

Sum(12, 15, ShowResult); // using ShowResult as a callback
ShowResult(15); // using ShowResult in the normal way

Console.WriteLine("-- Anonymous Methods ---");

Subtract(50, 20, (result) => // passing a anonymous method by using a lambda expression
{
    Console.WriteLine($"Result for subtract is {result}");
});

var show = delegate (int result) // used for callback methods (and events)
{
    Console.WriteLine($"Result using delegate calling is {result}");
};

Subtract(10, 20, show);

show(50);

Console.WriteLine("-- Delegates ---");

var calcMul = new Calculate(Multiply);

calcMul(12, 12);
calcMul(13, 13);
calcMul.Invoke(14, 14);

var calcAdd = new Calculate(Add);

calcAdd(12, 12);
calcAdd(13, 13);
calcAdd(14, 14);

Console.WriteLine("-- Delegate Multicasting ---");

var multiCalc = new Calculate(Add);
multiCalc += Multiply;

multiCalc(14, 14);

var circle = new Circle();
var circleDelegate = new Circle.CircleDelegate(circle.ShowInfo);
circleDelegate += circle.Area;
circleDelegate += circle.Perimeter;

circleDelegate.Invoke(15);

circleDelegate -= circle.Area;
circleDelegate?.Invoke(16);

#region

void ShowResult(int result)
{
    Console.WriteLine($"Result is {result}");
}

void Add(int x, int y) 
{
    Console.WriteLine($"{x} + {y} = {x + y}");
}

void Sum(int x, int y, Action<int> showResult) // the third parameter is a callback method
{
    var result = x + y;
    showResult(result);
}

void Subtract(int x, int y, Action<int> showResult) 
{
    var result = x - y;
    showResult(result);
}

void Multiply(int x, int y)
{
    Console.WriteLine($"{x} * {y} = {x * y}");
}

#endregion

public delegate void Calculate(int x, int y);
