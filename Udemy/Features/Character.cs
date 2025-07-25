using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math; // C#6: importing static members
using static Features.Type; // C#6: importing static members

namespace Features
{
    public enum Type
    {
        Elf,
        Ork
    }

    public class Weapon
    {
        public Weapon(string name)
        {
            Name = name; 
        }

        public string Name { get; }
    }


    public class Character : INotifyPropertyChanged
    {
        
        private string name;

        // C#6: automatic properties
        public int MyProperty { get; set; }

        // C#6: immutable (getter only) properties 
        public int Armor { get; } = 100; // C#6: initialization of properties

        public Type Type { get; }

        public int Wear { get; private set; } = 15;
        public int Health { get; private set; } = 100;
        public int Defense => Wear >= Armor ? 0 : Armor - Wear; // C#6: expression-bodied members
        /*
        public int Defense
        {
            get { return Wear >= Armor ? 0 : Armor - Wear }
        }
        */
        public Weapon Weapon { get;  }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                // OnPropertyChanged("Name");
                OnPropertyChanged(nameof(Name)); // C#6: nameOf keyword
            }
        }

        public Character(Type type)
        {
            Type = type;
            Armor = 90; // ctor has preference in property initialization
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Damage(int damage) => Health -= damage - Defense; // C#6: expression-bodied members
        //public void Damage(int damage)
        //{
        //    Health -= damage - Defense;
        //}

        public void ImportStaticExample()
        {
            switch (Type)
            {
                /*
                case Type.Elf: Console.WriteLine("Elf"); break;
                case Type.Ork: Console.WriteLine("Elf"); break;
                */
                case Elf: Console.WriteLine("Elf"); break; // C#6: importing static members
                case Ork: Console.WriteLine("Elf"); break;
            }
            Console.WriteLine(Pow(10, 10)); // C#6: importing static members
        }

        public override string ToString()
        {
            // string oldWay = string.Format("Name: {0}, Health: {1}", Name, Health);
            return $"Name: {Name}, Health: {Health}"; // C#6: string interpolation
        }
    }
}
