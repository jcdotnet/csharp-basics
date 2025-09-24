using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate
{
    public class CustomEventArgs : EventArgs
    {
        public int a { get; set; }
        public int b { get; set; }
    }

    // used in Window Forms, WPF, ASP.NET web forms (rarely used in ASP.NET MVC / ASP.NET Core) 
    public class PublisherEventHandler
    {
        public event EventHandler<CustomEventArgs> myEvent;

        public void RaiseEvent(object sender, int a, int b)
        {
            if (this.myEvent != null)
            {
                CustomEventArgs customEventArgs = new CustomEventArgs() { a = a, b = b };
                this.myEvent(sender, customEventArgs);
            }
        }
    }
    
}
