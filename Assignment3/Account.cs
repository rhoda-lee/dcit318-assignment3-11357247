using System;

public class Account
{
    public string AccountNumber { get; }
    public decimal Balance { get; protected set; }

    public Account(string accountNumber, decimal initialBalance)
    {
        AccountNumber = accountNumber;
        Balance = initialBalance;
    }

    public virtual void ApplyTransaction(Transaction transaction)
    {
        // Default behaviour: deduct amount
        Balance -= transaction.Amount;
        Console.WriteLine($"Applied {transaction.Amount:C}. New balance: {Balance:C}");
    }
}
