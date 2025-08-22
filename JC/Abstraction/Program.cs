using Abstraction;

Console.WriteLine("--- Abstraction ---");

// var animal = new Animal() // cannot create an instance of an abstract class
var animal = new Dog();
animal.Eat();
animal.MakeSound();

//var bird = new Bird();
//bird.Eat();

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

Console.WriteLine("--- Interfaces ---");

var masterStudent = new MasterStudent();
masterStudent.TakeExam();
masterStudent.Teach();

Console.WriteLine("--- Sealed Classes & Methods (stopping inheritance) ---");

var chicken = new Chicken();
chicken.Eat();
chicken.Walk();