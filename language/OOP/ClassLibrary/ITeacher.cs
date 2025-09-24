using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
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
    
}
