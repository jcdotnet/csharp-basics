using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
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

    public class Dog : Animal // Dog is an Animal
    {
        public override void Eat()
        {
            Console.WriteLine("The dog is eating...");
        }
    }
    public class Cat:Animal { // Cat is an Animal
        public new void MakeSound()
        {
            Console.WriteLine("My cat is mewoing!!");
        }
        public override void Eat()
        {
            Console.WriteLine("The cat is eating...");
        }
    }
    public class Bird : Animal // Bird is an Animal
    {
        public void LayEgg()
        {
            Console.WriteLine("My bird is laying eggs");
        }

        public override sealed void Eat()
        {
            Console.WriteLine("You are about to feed a bird. What do you want to give it?");
            var food = Console.ReadLine();
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
    public sealed class Penguin : Bird // Penguin is a Bird (eats, lay eggs and swim)
    {
        public void Swim()
        {
            Console.WriteLine("My penguin is swimming");
        }
    }
    //public class MyChicken:Chicken { } //syntax error
    //public class MyPenguin:Chicken { } //syntax error
}
