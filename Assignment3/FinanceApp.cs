using System;
using System.Collections.Generic;

public class FinanceApp
{
    private readonly List<Transaction> _transactions = new();

    public void Run()
    {
        var account = new SavingsAccount("SA-1001", 1000m);

        var t1 = new Transaction(1, DateTime.Now, 150.50m, "Groceries");
        var t2 = new Transaction(2, DateTime.Now, 300m, "Utilities");
        var t3 = new Transaction(3, DateTime.Now, 1200m, "Entertainment"); // purposely larger to test insufficient funds

        ITransactionProcessor p1 = new MobileMoneyProcessor();
        ITransactionProcessor p2 = new BankTransferProcessor();
        ITransactionProcessor p3 = new CryptoWalletProcessor();

        p1.Process(t1);
        account.ApplyTransaction(t1);
        _transactions.Add(t1);

        p2.Process(t2);
        account.ApplyTransaction(t2);
        _transactions.Add(t2);

        p3.Process(t3);
        account.ApplyTransaction(t3);
        _transactions.Add(t3);

        Console.WriteLine("\nAll processed transactions:");
        foreach (var tx in _transactions) Console.WriteLine(tx);
    }
}
