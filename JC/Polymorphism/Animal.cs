using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polymorphism
{
    public class Animal
    {
        //private string name;
        public virtual void Eat()
        {
            Console.WriteLine("Animal is eating food!!");
        }
    }
    public class Cat:Animal { 

        public override void Eat()
        {
            Console.WriteLine("Cat is eating meat.");
        }
        public void Mewo()
        {
            Console.WriteLine("My cat is mewoing!!");
        }
    }
    public class Bird : Animal
    {
        public override void Eat()
        {
            Console.WriteLine("Bird is eating seed.");
        }
        public void LayEgg()
        {
            Console.WriteLine("My bird is laying eggs");
        }
    }
    public class Penguin : Bird
    {
        public void Swim()
        {
            //name = "my penguin";
            Console.WriteLine("My penguin is swimming");
        }
    }
}
