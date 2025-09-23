using ClassLibrary;

namespace Inheritance
{
    // internal: accessible only within files in the same assembly
    internal class CalculatorPro : Calculator 
    {
        /* 
         * int firstNumber;
         * int secondNumber;
        */
        public CalculatorPro(int firstNumber, int secondNumber)
            :base() //:base(firstNumber, secondNumber)
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
    }
}
