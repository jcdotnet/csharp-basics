using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Constructors
{
    internal class DestructorExample
    {

        internal DestructorExample()
        {
            Console.WriteLine("File/DB is opened");
        }

        internal void DisplayData()
        {
            Console.WriteLine("Displaying data from file/DB");
        }

        // we use the destructor to close the unmanaged resources
        ~DestructorExample()
        {
            Console.WriteLine("File/DB is Closed");
        }

    }

    internal class IDisposableExample : IDisposable
    {
        internal IDisposableExample()
        {
            Console.WriteLine("File/DB is opened");
        }
        internal void DisplayData()
        {
            Console.WriteLine("Displaying data from file/DB");
        }

        // this method is used to close the unmanaged resources
        public void Dispose()
        {
            Console.WriteLine("File/DB is Closed");
        }
    }
}
