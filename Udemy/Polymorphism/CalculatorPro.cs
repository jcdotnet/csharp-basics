using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polymorphism
{
    internal class CalculatorPro : Calculator
    {
        /* int firstNumber;
         int secondNumber;*/
        public CalculatorPro(int firstNumber, int secondNumber)
            : base() //new Calculator()
                     //:base(firstNumber, secondNumber)
        {

            this.firstNumber = firstNumber;
            this.secondNumber = secondNumber;
        }

        public double Power2() //2*2, 4*4
        {
            //return firstNumber * firstNumber;

            return Math.Pow(firstNumber, 2);
        }

        public double PowerX()
        {
            return Math.Pow(firstNumber, secondNumber);
        }

        public override string DoAllOperations()
        {
            return
                //$"Addition is: {Add()}\n" +
                //$"Subtraction is {Subtract()}\n" +
                //base.DoAllOperations() +
                $"\nPower2 is {Power2()}\n" +
                $"PowerX is {PowerX()}\n" +
                $"{base.DoAllOperations()}";
        }
    }
}
