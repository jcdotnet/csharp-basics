using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic
{
    abstract class Student
    {
        public abstract double[] GetGrades();
    }

    internal class GraduateStudent : Student
    {
        public override double[] GetGrades()
        {
            // returns dummy hard-coded value
            return new double[] { 100, 90, 100 };
        }

    }

    internal class GradeBook<T> where T : Student /* class */
    {
        private T _student;

        public T Student { get => _student; set => _student = value; }

        public void Print()
        {
            double[] studentGrades = (Student as Student).GetGrades();
            foreach (double grade in studentGrades)
                Console.WriteLine(grade);
        }
    }
}
