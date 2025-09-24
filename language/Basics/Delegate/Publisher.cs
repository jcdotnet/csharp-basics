using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate
{

    // public delegate void MyDelegateType(int a, int b); // Func is applicable
    public class Publisher // real world example: button in a GUI
    {
        // private MyDelegateType myDelegate; // auto-implemented event (creates a private delegate)

        public event Func<int, int, int> myEvent; // real world example: click event
        /*
        public event MyDelegateType myEvent; // Func
        public event MyDelegateType myEvent // auto-implemented event
        {
            add
            {
                myDelegate += value;
            }
            remove
            {
                myDelegate -= value;
            }
        }
        */

        public int RaiseEvent(int a, int b)
        {
            if (this.myEvent != null)
            {
                return this.myEvent(a, b);
            }
            return 0;
        }

        /* auto-implemented event
        public void RaiseEvent(int a, int b)
        {
            if (this.myDelegate != null)
            {
                this.myDelegate(a, b);
            }
        }
        */
    }

    public class Test
    {
        public void DoWork()
        {
            PublisherEventHandler publisher = new PublisherEventHandler();

            //handle the event (or) subscribe to event
            publisher.myEvent += (sender, e) =>
            {
                int c = e.a + e.b;
                Console.WriteLine(c);
            };

            //invoke the event
            publisher.RaiseEvent(this, 10, 50);
            publisher.RaiseEvent(this, -5, 30);
            publisher.RaiseEvent(this, 14, 10);
        }
    }
}
