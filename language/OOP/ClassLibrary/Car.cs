using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Car
    {
        private string[] _brands = new string[] { "BMW", "Skoda", "Tesla" };

        // public indexer
        public string this[int index] { get { return _brands[index]; } }

    }
}
