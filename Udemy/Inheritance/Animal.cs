﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    public class Animal
    {
        public void Eat()
        {
            Console.WriteLine("Animal is eating food!!");
        }
    }
    public class Cat:Animal { 
        public void Mewo()
        {
            Console.WriteLine("My cat is mewoing!!");
        }
    }
    public class Bird : Animal
    {
        public void LayEgg()
        {
            Console.WriteLine("My bird is laying eggs");
        }
    }
    public class Penguin : Bird
    {
        public void Swim()
        {
            Console.WriteLine("My penguin is swimming");
        }
    }
}
