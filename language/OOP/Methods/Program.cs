Console.WriteLine("-- Optional Parameters ---");

Add(10, 20);
Add(10);
Add2(12, 15.2F);
Add2(y: 15.2F);

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


#region methods

void Add(int x, int y = 0) => Console.WriteLine($"{x} + {y} = {x + y}");

void Add2(int x = 0, float y = 0)
{
    Console.WriteLine($"{x} + {y} = {x + y}");
}

void Multiply(int x, int y)
{
    Console.WriteLine($"{x} * {y} = {x * y}");
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

void ShowResult(int result)
{
    Console.WriteLine($"Result is {result}");
}

#endregion

