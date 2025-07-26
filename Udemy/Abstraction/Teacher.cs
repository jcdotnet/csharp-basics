using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction
{
    public interface ITeacher
    {
        void Teach();
        void TakeExam();
    }

    public class BachelorTeacher : ITeacher
    {
        public void TakeExam()
        {
            throw new NotImplementedException();
        }

        public void Teach()
        {
            throw new NotImplementedException();
        }
    }

    public class MasterTeacher : ITeacher
    {
        public void TakeExam()
        {
            throw new NotImplementedException();
        }

        public void Teach()
        {
            throw new NotImplementedException();
        }
    }
    public class Student
    {
        public void Study()
        {

        }
        public void Register()
        {

        }
    }

    public class BachelorStudent : Student
    {

    }

    public class MasterStudent :Student, ITeacher
    {
        public void TakeExam()
        {
            Console.WriteLine("The master student is taking an exam");
        }

        public void Teach()
        {
            Console.WriteLine("The master student is teaching");

        }
    }
}
