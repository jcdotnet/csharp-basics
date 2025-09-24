using ClassLibrary;
using Polymorphism;

Console.WriteLine("OOP - Abstraction / Polymorphism");

var calc = new Calculator(15, 20);
Console.WriteLine(calc.DoAllOperations());

var calcPro = new CalculatorPro(10, 20);
Console.WriteLine(calcPro.DoAllOperations());


// var animal = new Animal() // cannot create an instance of an abstract class
var animal = new Dog();
animal.Eat();
animal.MakeSound();

var cat = new Cat();
cat.Eat();

//var bird = new Bird();
//bird.Eat();

var bird = new Bird();
bird.Eat();

var penguin = new Penguin();
penguin.Eat();
penguin.Swim();
penguin.LayEgg();

var chicken = new Chicken();
chicken.Eat();
chicken.Walk();

var square = new Square(10);
Console.WriteLine(
    $"The area of the square is: {square.Area()}\n" +
    $"The perimeter of the square is: {square.Perimeter()}\n"
);

var rect = new Rectangle(10, 20);
Console.WriteLine(
    $"The area of the rectangle is: {rect.Area()}\n" +
    $"The perimeter of the rectangle is: {rect.Perimeter()}\n"
);

var masterStudent = new MasterStudent();
masterStudent.TakeExam();
masterStudent.Teach();

