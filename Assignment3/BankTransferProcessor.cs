using System;

public class BankTransferProcessor : ITransactionProcessor
{
    public void Process(Transaction transaction)
    {
        Console.WriteLine($"[BankTransfer] Processing {transaction.Category} of {transaction.Amount:C} on {transaction.Date:d}");
    }
}
