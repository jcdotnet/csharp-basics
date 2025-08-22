using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features
{
    public class Customer
    {
        public string GetFullInfo(string fullString) {
            return GetFullName(fullString).firstName + GetFullName(fullString).lastName;

            (string firstName, string lastName) GetFullName(string str) // C#7: local functions
            {
                return (str.Split()[0], str.Split()[1]);
            }
        }

        public string GetLastName(string fullname)
        {
            var names = fullname.Split('.');
            return names.Length > 1 ? names[1] : throw new ArgumentException(); // C#7 : throwing expressions
        }

        public ref int Modifier(int n, int[] numbers) // C#7: returning by reference 
        {
            for (int i=0; i<numbers.Length; i++)
            {
                if (numbers[i] == n)
                {
                    return ref numbers[i];
                }
                
            }
            throw new ArgumentException();
        }

    }
}
