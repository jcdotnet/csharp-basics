using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClassLibrary
{
    public class Character
    {
        public Character(string name, int health, int attack, int defense)
        {
            Name = name;
            Health = health;
            Attack = attack;
            Defense = defense;
        }

        public string Name { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defense {  get; set; }

        public virtual void AttackCharacter(Character target)
        {
            int damage = Attack - target.Defense;
            if (damage > 0)
            {
                target.Health -= damage;
                Console.WriteLine($"{Name} attacks {target.Name} for {damage} damage!");
            } else
            {
                Console.WriteLine($"{Name}'s attack has no effect on {target.Name}.");
            }
        }

        public virtual void Defend()
        {
            Console.WriteLine($"{Name} defends.");
        }

    }

    public class Warrior : Character
    {
        public Warrior(string name, int health, int attack, int defense) : base(name, health, attack, defense)
        {
        }

        public void Charge(Character target) // special attack
        {
            int damage = Attack * 2 - target.Defense;
            if (damage > 0)
            {
                target.Health -= damage;
                Console.WriteLine($"{Name} charges {target.Name} for {damage} damage!");
            }
            else
            {
                Console.WriteLine($"{Name}'s charge has no effect on {target.Name}.");
            }
        }

        public override void Defend()
        {
            Defense += 5;
            Console.WriteLine($"{Name} defends and gains 5 defense.");
        }
    }

    public class Mage : Character
    {
        public Mage(string name, int health, int attack, int defense) : base(name, health, attack, defense)
        {
        }

        public void CastSpell(Character target) // special attack
        {
            int damage = Attack * 3 - target.Defense;
            if (damage > 0)
            {
                target.Health -= damage;
                Console.WriteLine($"{Name} casts a spell on {target.Name} for {damage} damage!");
            }
            else
            {
                Console.WriteLine($"{Name}'s spell has no effect on {target.Name}.");
            }
        }

        public sealed override void Defend()
        {
            Console.WriteLine($"{Name} cannot defend!");
        }
    }

    public class Dragon : Character
    {
        public Dragon(string name, int health, int attack, int defense) : base(name, health, attack, defense)
        {
        }

        public override void AttackCharacter(Character target)
        {
            int damage = Attack * 2 - target.Defense;
            if (damage > 0)
            {
                target.Health -= damage;
                Console.WriteLine($"{Name} attacks {target.Name} for {damage} damage!");
            }
            else
            {
                Console.WriteLine($"{Name} attack has no effect on {target.Name}.");
            }
        }

        public void BreatheFire(Character target)
        {
            int damage = Attack * 4 - target.Defense;
            if (damage > 0)
            {
                target.Health -= damage;
                Console.WriteLine($"{Name} breathes fire on {target.Name} for {damage} damage!");
            }
            else
            {
                Console.WriteLine($"{Name}'s fire has no effect on {target.Name}.");
            }
        }

        public override void Defend()
        {
            Console.WriteLine($"{Name} defends and gains 10 defense.");
            Defense += 10;
        }
    }

}
