using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassLibrary
{
    [Serializable]
    public class Country
    {
        public string? Name { get; set; }
        public string? CountryCode { get; set; }
        public string? Region {  get; set; }

        public override string ToString()
        {
            return $"{CountryCode} {Name} {Region}";
        }
    }
}
