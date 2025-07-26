using Encapsulation;

var bankAccount = new BankAccount(150.0F);
Console.WriteLine($"Bank account balance is: {bankAccount.Balance}");
bankAccount.Deposit(100);
Console.WriteLine($"Bank account balance is: {bankAccount.Balance}");
bankAccount.Withdraw(50);
Console.WriteLine($"Bank account balance is: {bankAccount.Balance}");
bankAccount.Withdraw(300);
Console.WriteLine($"Bank account balance is: {bankAccount.Balance}");