
using Polymorphism;

var calc = new Calculator(15, 20);
Console.WriteLine(calc.DoAllOperations());

var calcPro = new CalculatorPro(10, 20);
Console.WriteLine(calcPro.DoAllOperations());

var animal = new Animal();
animal.Eat();

var cat = new Cat();
cat.Eat();

var bird = new Bird();
bird.Eat();

var penguin = new Penguin();
penguin.Eat();
//penguin.name = "gary";

