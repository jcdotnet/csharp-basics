using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threading
{
    class InsufficientFundsException : Exception
    {
        public int AccountNumber { get; }   // stores the account number associated with the exception.
        public double Amount { get; }       // stores the withdrawal or transfer amount that caused the exception.

        public InsufficientFundsException(int accountNumber, double amount)
        {
            AccountNumber = accountNumber;
            Amount = amount;
        }
    }

    class BankAccount
    {
        private object balanceLock = new object(); // a lock object to ensure thread safety

        public int AccountNumber { get; set; }
        public double Balance { get; private set; }

        public BankAccount(int accountNumber, double initialBalance)
        {
            AccountNumber = accountNumber;
            Balance = initialBalance;
        }

        public void Deposit(double amount)
        {
            lock (balanceLock)
            {
                Balance += amount;
            }

        }

        public void Withdraw(double amount)
        {
            lock (balanceLock)
            {
                if (Balance >= amount)
                {
                    Balance -= amount;
                }
                else
                {
                    throw new InsufficientFundsException(AccountNumber, amount);
                }
            }
        }

        public void TransferTo(BankAccount destination, double amount)
        {
            lock (balanceLock)
            {
                // Check if there's enough balance to perform the transfer.
                if (Balance >= amount)
                {
                    // Safely update the source account's balance by subtracting the transfer amount.
                    Balance -= amount;
                    // Safely update the destination account's balance by adding the transfer amount.
                    destination.Deposit(amount);
                }
                else
                {
                    // Throw an exception for insufficient funds.
                    throw new InsufficientFundsException(AccountNumber, amount);
                }
            }
        }
    }

    class Bank
    {
        private List<BankAccount> accounts = new List<BankAccount>();  // List to store bank accounts
        private object accountsLock = new object();                    //lock object to ensure thread safety

        public void OpenAccount(double initialBalance)
        {
            lock (accountsLock)
            {
                int newAccountNumber = accounts.Count + 1;
                BankAccount newAccount = new BankAccount(newAccountNumber, initialBalance);
                accounts.Add(newAccount);
            }
        }

        public void CloseAccount(int accountNumber)
        {
            lock (accountsLock)
            {
                BankAccount accountToRemove = accounts.Find(acc => acc.AccountNumber == accountNumber);
                if (accountToRemove != null)
                {
                    accounts.Remove(accountToRemove);
                }
            }
        }

        public double GetTotalBalance()
        {
            double totalBalance = 0;
            lock (accountsLock)
            {
                foreach (var account in accounts)
                {
                    totalBalance += account.Balance;
                }
            }
            return totalBalance;
        }
        public void Transfer(int sourceAccountNumber, int destinationAccountNumber, double amount)
        {
            lock (accountsLock)
            {
                BankAccount sourceAccount = accounts.Find(acc => acc.AccountNumber == sourceAccountNumber);
                BankAccount destAccount = accounts.Find(acc => acc.AccountNumber == destinationAccountNumber);

                if (sourceAccount != null && destAccount != null)
                {
                    sourceAccount.TransferTo(destAccount, amount);
                }
                else
                {
                    throw new ArgumentException("Source or destination account not found.");
                }
            }
        }
        public void Withdraw(int accountNumber, double amount)
        {
            lock (accountsLock)
            {
                // Find the account by its account number.
                BankAccount account = accounts.Find(acc => acc.AccountNumber == accountNumber);
                if (account != null)
                {
                    account.Withdraw(amount);
                }
                else
                {
                    // Throw an exception if the account is not found.
                    throw new ArgumentException("Account not found.");
                }
            }
        }

        public double GetAccountBalance(int accountNumber)
        {
            lock (accountsLock)
            {
                // Find the account by its account number and return its balance.
                BankAccount account = accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
                if (account != null)
                {
                    return account.Balance;
                }
                else
                {
                    // Throw an exception if the account is not found.
                    throw new ArgumentException($"Account {accountNumber} not found.");
                }
            }
        }
    }

    public class BankOperations()
    {
        static DateTime startTime;                                      
        static readonly TimeSpan executionTime = TimeSpan.FromSeconds(60);
        
        public static void BankOperationsDemo()
        {

            Bank bank = new Bank();

            for (int i = 1; i <= 5; i++)
            {
                bank.OpenAccount(1000);
            }

            startTime = DateTime.Now;
            Thread depositThread = new Thread(() => PerformDeposits(bank));
            Thread withdrawalThread = new Thread(() => PerformWithdrawals(bank));
            Thread transferThread = new Thread(() => PerformTransfers(bank));

            depositThread.Start();
            withdrawalThread.Start();
            transferThread.Start();

            Thread monitoringThread = new Thread(() => MonitorAccounts(bank));
            monitoringThread.Start();

            // waiting for all threads to finish their tasks
            depositThread.Join();
            withdrawalThread.Join();
            transferThread.Join();

            // signaling the monitoring thread to stop
            monitoringThread.Interrupt();
            monitoringThread.Join();

            Console.WriteLine("All threads have completed their tasks.");
        }

        static void PerformDeposits(Bank bank)
        {
            try
            {
                Random random = new Random();

                while (DateTime.Now - startTime < executionTime)
                {
                    int accountNumber = random.Next(1, 6);
                    double depositAmount = random.Next(1, 101);

                    bank.OpenAccount(depositAmount);

                    Console.WriteLine($"Deposited ${depositAmount} into Account {accountNumber}. " +
                        $"Updated Balance: {bank.GetAccountBalance(accountNumber)}");

                    Thread.Sleep(random.Next(500, 1001));
                }
            }
            catch (ThreadInterruptedException)
            {
                Console.WriteLine("Deposit thread was interrupted.");
            }
        }

        static void PerformWithdrawals(Bank bank)
        {
            try
            {
                Random random = new Random();

                while (DateTime.Now - startTime < executionTime)
                {
                    int accountNumber = random.Next(1, 6);
                    double withdrawalAmount = random.Next(1, 201);

                    try
                    {
                        // Simulate a withdrawal operation.
                        bank.Withdraw(accountNumber, withdrawalAmount);

                        // Display the withdrawal and the updated balance.
                        Console.WriteLine($"Withdrawn ${withdrawalAmount} from Account {accountNumber}. " +
                            $"Updated Balance: {bank.GetAccountBalance(accountNumber)}");
                    }
                    catch (InsufficientFundsException ex)
                    {
                        // Display an insufficient funds message and the updated balance.
                        Console.WriteLine($"Withdrawal from Account {ex.AccountNumber} failed: " +
                            $"Insufficient funds. Updated Balance: " +
                            $"{bank.GetAccountBalance(accountNumber)}");
                    }

                    // Sleep for a random time.
                    Thread.Sleep(random.Next(500, 1001));
                }
            }
            catch (ThreadInterruptedException)
            {
                Console.WriteLine("Withdrawal thread was interrupted.");
            }
        }

        static void PerformTransfers(Bank bank)
        {
            try
            {
                Random random = new Random();

                while (DateTime.Now - startTime < executionTime)
                {
                    int sourceAccountNumber = random.Next(1, 6);
                    int destinationAccountNumber = random.Next(1, 6);
                    double transferAmount = random.Next(1, 101);

                    try
                    {
                        // Simulate a transfer operation.
                        bank.Transfer(sourceAccountNumber, destinationAccountNumber, transferAmount);

                        Console.WriteLine($"Transferred ${transferAmount} from Account {sourceAccountNumber} " +
                            $"to Account {destinationAccountNumber}. Updated Balance in Source Account: " +
                            $"{bank.GetAccountBalance(sourceAccountNumber)}. " +
                            $"Updated Balance in Destination Account: " +
                            $"{bank.GetAccountBalance(destinationAccountNumber)}");
                    }
                    catch (InsufficientFundsException ex)
                    {
                        // Display an insufficient funds message.
                        Console.WriteLine($"Transfer from Account {ex.AccountNumber} failed: " +
                            $"Insufficient funds.");
                    }

                    // Sleep for a random time.
                    Thread.Sleep(random.Next(500, 1001));
                }
            }
            catch (ThreadInterruptedException)
            {
                Console.WriteLine("Transfer thread was interrupted.");
            }
        }

        static void MonitorAccounts(Bank bank)
        {
            try
            {
                while (true)
                {
                    double totalBalance = bank.GetTotalBalance();
                    Console.WriteLine($"Total Balance in the Bank: ${totalBalance}");
                    Thread.Sleep(5000);  // Monitor every 5 seconds.
                }
            }
            catch (ThreadInterruptedException)
            {
                Console.WriteLine("Monitoring thread was interrupted.");
            }
        }
    }
}
