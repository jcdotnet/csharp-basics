using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Employee : ICloneable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }

        public object Clone() 
        {
            return new Employee() { Id = this.Id, Name = this.Name, Position = this.Position };
        }
        public override string? ToString()
        {
            return $"Id: {Id}. Name: {Name}. Position: {Position}";
        }
    }
}
