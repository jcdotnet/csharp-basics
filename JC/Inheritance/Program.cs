using Inheritance;

var calc = new Calculator(15,20);
Console.WriteLine($"Addition result is : {calc.Add()}");
Console.WriteLine($"Subtraction result is : {calc.Subtract()}");
//calc.firstNumber = 12;

var calcPro = new CalculatorPro(10,20);
Console.WriteLine($"Power2 result is : {calcPro.Power2()}");
Console.WriteLine($"PowerX result is : {calcPro.PowerX()}");
Console.WriteLine($"Addition Pro result is : {calcPro.Add()}");
//calcPro.firstNumber = 10;

var penguin = new  Penguin();
penguin.Eat();
penguin.Swim();
penguin.LayEgg();