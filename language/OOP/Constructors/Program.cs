// See https://aka.ms/new-console-template for more information
using ClassLibrary;

Console.WriteLine("Constructors");

Console.WriteLine(Employee.Company); // DOE; 

Employee employee1 = new Employee(); // id = 101, name = Unnamed, salary = 0
Employee employee2 = new Employee(102, "John", "Málaga", 2000);
Employee employee3 = new Employee(103, "Jane", "Málaga", 3000);

// object initializr
Employee employee4 = new Employee() { }; // 101, Unnamed, 0
Employee employee5 = new Employee() { Id = 110 }; // 110, Unnamed, 0
Employee employee6 = new Employee() { Id = 120, Name = "Alice", Salary = 5000 };

Student studentNoInfo = new Student();
Console.WriteLine(studentNoInfo.ShowInfo());

Student student1 = new Student("John", 1, "Engineering");
student1.Score = 5;
Console.WriteLine(student1.ShowInfo());


Student student2 = new Student("Jane", 1, "Art");
student2.Score = 5;
Console.WriteLine(student2.ShowInfo());

Student student3 = new Student();
student3.Name = "David";
student3.Grade = 2;
student3.Department = "Engineering";
student3.Score = 9.25;
Console.WriteLine(student3.ShowInfo());

Car car = new Car(); // default constructor
Console.WriteLine(car[0]); // get accesor of the indexer