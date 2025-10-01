using System.Xml.Linq;

namespace ClassLibrary
{
    public class Student
    {
        private int grade = 100;
        private string name = "John";

        // auto-implemented property initializer (C#6)
        public string Location { get; set; } = "Málaga"; 
        
        public int Age { get; set; }
        public string Name { get => name; set => name = value; }

        public Student() { }

        // traditional constructor (verbose)
        //public Student(string name, int age)
        //{
        //    Name = name;
        //    Age = age;
        //}
        // primary constructor with property initializers
        public Student(string name, int age) => (Name, Age) = (name, age);

        public void PrintGrade()
        {
            Console.WriteLine("Grade: " + grade);
        }
   
        // local function (C#7.0)
        public void DisplayGrades(params int[] grades)
        {
            for (int i = 0; i < grades.Length; i++)
            {
                Console.WriteLine("Grade " + i + ": " + grades[i]);
            }
            Console.WriteLine("Average " + getAverageGrade());

            double getAverageGrade()
            {
                double total = 0;
                for(int i = 0; i < grades.Length; i++)
                {
                    total += grades[i];
                }
                return total / grades.Length;
            }
        }

        // local static local function (C#8.0)
        public void DisplayGradesWithStaticFunction(params int[] grades)
        {
            for (int i = 0; i < grades.Length; i++)
            {
                Console.WriteLine("Grade " + i + ": " + grades[i]);
            }
            Console.WriteLine("Average " + getAverageGrade(grades));

            static double getAverageGrade(params int[] grades)
            {
                double total = 0;
                for (int i = 0; i < grades.Length; i++)
                {
                    total += grades[i];
                }
                return total / grades.Length;
            }
        }

        public void DisplaySubjects(params string[] subjects)
        {
            for (int i = 0; i < subjects.Length; i++)
            {
                Console.WriteLine(subjects[i]);
            }
        }

        /*
        public int GetNameLength()
        {
            return name.Length;
        }
        */
        // expression-bodied method (C#7.0)
        public int GetNameLength() => name.Length;

        // public method with ref return (C# 7.3)
        public ref int RefReturnExample()
        {
            return ref grade;
        }
        
        public override string ToString() => $"{Name} - {Age}";
    }
}
