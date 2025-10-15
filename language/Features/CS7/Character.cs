using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS7
{
    public readonly struct Character
    {
        private readonly string _name;

        public string Name { get { return _name; } }

        // public Character() { // this is allowed in C# 10
        // _name = "Unknown hero";
        // } 
        public Character(string name)
        {
            // initializes the character name
            _name = name;
        }
        public void PrintCharacterName()
        {
            // this.Name = "Superman"; // compile-time error
            Console.WriteLine(Name);
        }
    }
}
