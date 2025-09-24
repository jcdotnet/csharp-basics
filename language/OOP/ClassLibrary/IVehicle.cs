using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public interface IVehicle
    {
        public void move();
        public void stop();
    }

    public class Car : IVehicle
    {
        private string[] _brands = new string[] { "BMW", "Skoda", "Tesla" };

        // public indexer
        public string this[int index] { get { return _brands[index]; } }

        public void move()
        {
            throw new NotImplementedException();
        }

        public void stop()
        {
            throw new NotImplementedException();
        }
    }
}
