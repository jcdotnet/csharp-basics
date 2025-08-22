using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encapsulation
{
    public class BankAccount
    {
        float balance;

        public float Balance
        {
            get { return balance; }
        }
        public BankAccount(float initialBalance) {
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
                balance-=amount;
        }
    }
}
