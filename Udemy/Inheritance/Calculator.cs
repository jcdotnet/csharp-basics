﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    public class Calculator
    {
        protected int firstNumber;
        protected int secondNumber;

        public Calculator() { }

        public Calculator(int firstNumber, int secondNumber)
        {
            this.firstNumber = firstNumber;
            this.secondNumber = secondNumber;
        }
        public int Add() => firstNumber + secondNumber;
        public int Subtract() { return firstNumber - secondNumber; }


    }
}
