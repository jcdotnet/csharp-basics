using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features
{
    public enum Cities
    {
        Vienna,
        Berlin,
        Amsterdam,
        Madrid,
        Budapest,
        Paris,
        London,
        Warsaw,
        Prague,
        Melbourne
    }
    public class City
    {
        public Cities CityName { get; set; }
        public string CountryName { get; set; }

        public City(Cities city, string country)
        {
            CityName = city;
            CountryName = country;
        }
    }

}
