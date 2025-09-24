using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate
{
    
    // wants to suscribe to the event
    public class Subscriber  //  real world example: form in a GUI
    {
        public void Add(int a, int b) // event hanlder (executed when the event is raised)
        {
            Console.WriteLine(a + b);
        }
    }
}
