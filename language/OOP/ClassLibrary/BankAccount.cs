using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class BankAccount
    {
        float balance;

        // encapsulation
        public float Balance
        {
            get { return balance; }
        }
        public BankAccount(float initialBalance)
        {
            this.balance = initialBalance;
        }

        public void Deposit(float amount)
        {
            balance += amount;
        }
        public void Withdraw(float amount)
        {
            if (amount > balance)
                Console.WriteLine("The balance is less than requested amount!!");
            else
                balance -= amount;
        }
    }

    // we can create the ChekingAccount, SavingsAccount and other specific accounts derived classes
}
