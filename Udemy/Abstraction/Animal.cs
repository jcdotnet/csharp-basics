using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction
{
    public abstract class Animal
    {
        protected string name;
        public void MakeSound()
        {
            Console.WriteLine("The animal is making a sound");
        }

        public abstract void Eat(); // abstract method
        //{
        //    // body is not allowed in asbtract methods
        //    Console.WriteLine("The animal is eating");
        //}
    }

    public class Dog : Animal
    {
        public override void Eat()
        {
            Console.WriteLine("The dog is eating...");
        }
    }

    public class Bird : Animal
    {
        //public override void Eat()
        override public sealed void Eat()
        {
            Console.WriteLine("You are about to feed a bird. What do you want to give it?");
            var food=Console.ReadLine();
            Console.WriteLine($"The bird is eating {food}");
        }
    }

    public sealed class Chicken : Bird
    {
        //public override void Eat()
        //{
        //    Console.WriteLine("The chicken is eating seed!!");
        //}

        public void Walk()
        {
            Console.WriteLine("The chicken is walking...");
        }
    }
    //public class MyChicken:Chicken { } //syntax error

}
