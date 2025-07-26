using OOP;

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
