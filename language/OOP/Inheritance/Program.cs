// See https://aka.ms/new-console-template for more information
using ClassLibrary;
using Inheritance;

Console.WriteLine("Inheritance");

Employee employee = new Employee();
employee.Id = 101;
employee.Name = "Alice";
employee.Location = "Málaga";

Manager manager = new Manager();
manager.Id = 101;
manager.Name = "David";
manager.Location = "Málaga";
manager.Department = "Accounting";
manager.Work();
manager.Rest();
Console.WriteLine(manager.GetDepartmentInfo());
Console.WriteLine(manager.GetTotalSales());

Salesman sales1 = new Salesman();
sales1.Id = 101;
sales1.Name = "John";
sales1.Location = "Málaga";
sales1.Region = "Centro";
sales1.Work();
sales1.Rest();
Console.WriteLine(sales1.GetTotalSales());


Salesman sales2 = new Salesman();
sales2.Id = 101;
sales2.Name = "Jane";
sales2.Location = "Málaga";
sales2.Region = "Centro";
sales2.Work();
sales2.Rest();
Console.WriteLine(sales2.GetTotalSales());



var calc = new Calculator(15, 20);
Console.WriteLine($"Addition result is : {calc.Add()}");
Console.WriteLine($"Subtraction result is : {calc.Subtract()}");
//calc.firstNumber = 12;

var calcPro = new CalculatorPro(10, 20);
Console.WriteLine($"Power2 result is : {calcPro.Power2()}");
Console.WriteLine($"PowerX result is : {calcPro.PowerX()}");
Console.WriteLine($"Addition Pro result is : {calcPro.Add()}");
//calcPro.firstNumber = 10;

var penguin = new Penguin();
penguin.Eat();
penguin.Swim();
penguin.LayEgg();

