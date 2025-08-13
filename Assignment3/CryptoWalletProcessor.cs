using System;

public class CryptoWalletProcessor : ITransactionProcessor
{
    public void Process(Transaction transaction)
    {
        Console.WriteLine($"[CryptoWallet] Processing {transaction.Category} of {transaction.Amount:C} on {transaction.Date:d}");
    }
}
