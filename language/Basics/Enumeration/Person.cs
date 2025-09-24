using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enumeration
{
    internal class Person // accessible only within files in the same assembly
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public LifeStageEnumeration LifeStage { get; set; }
    }


    //enum LifeStageEnumeration // default underlying type is int
    //{
    //    Child, // 0
    //    Teenager, //1
    //    Adult,// 2
    //    Senior // 3
    //}

    enum LifeStageEnumeration : ushort // underlying type is now ushort
    {
        Child, // 0
        Teenager, // 1
        Adult = 100, // 100
        Senior // 101
    }

}
