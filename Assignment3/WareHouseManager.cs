using System;

public class WareHouseManager
{
    private readonly InventoryRepository<ElectronicItem> _electronics = new();
    private readonly InventoryRepository<GroceryItem> _groceries = new();

    public void SeedData()
    {
        try
        {
            _electronics.AddItem(new ElectronicItem(1, "Smartphone", 10, "BrandX", 24));
            _electronics.AddItem(new ElectronicItem(2, "Laptop", 5, "CompPro", 12));
            _electronics.AddItem(new ElectronicItem(3, "Headphones", 20, "Soundz", 6));

            _groceries.AddItem(new GroceryItem(101, "Rice", 50, DateTime.Now.AddMonths(12)));
            _groceries.AddItem(new GroceryItem(102, "Milk", 30, DateTime.Now.AddDays(10)));
            _groceries.AddItem(new GroceryItem(103, "Beans", 40, DateTime.Now.AddMonths(6)));
        }
        catch (Exception ex)
        {
            Console.WriteLine("Seed error: " + ex.Message);
        }
    }

    public void PrintAllItems<T>(InventoryRepository<T> repo) where T : IInventoryItem
    {
        var items = repo.GetAllItems();
        foreach (var it in items) Console.WriteLine(it);
    }

    public void IncreaseStock<T>(InventoryRepository<T> repo, int id, int quantity) where T : IInventoryItem
    {
        try
        {
            var item = repo.GetItemById(id);
            repo.UpdateQuantity(id, item.Quantity + quantity);
            Console.WriteLine($"Increased item {id} by {quantity}. New qty: {repo.GetItemById(id).Quantity}");
        }
        catch (Exception ex) when (ex is ItemNotFoundException || ex is InvalidQuantityException)
        {
            Console.WriteLine($"Error increasing stock: {ex.Message}");
        }
    }

    public void RemoveItemById<T>(InventoryRepository<T> repo, int id) where T : IInventoryItem
    {
        try
        {
            repo.RemoveItem(id);
            Console.WriteLine($"Removed item {id}");
        }
        catch (ItemNotFoundException ex)
        {
            Console.WriteLine($"Remove error: {ex.Message}");
        }
    }

    // Methods to demonstrate exceptions
    public void DemoExceptions()
    {
        // Add duplicate
        try
        {
            _electronics.AddItem(new ElectronicItem(1, "DuplicatePhone", 1, "DupBrand", 6));
        }
        catch (DuplicateItemException dex)
        {
            Console.WriteLine($"Caught duplicate add: {dex.Message}");
        }

        // Remove non-existent
        try
        {
            _groceries.RemoveItem(999);
        }
        catch (ItemNotFoundException inf)
        {
            Console.WriteLine($"Caught remove missing: {inf.Message}");
        }

        // Invalid quantity
        try
        {
            _electronics.UpdateQuantity(2, -5);
        }
        catch (InvalidQuantityException iq)
        {
            Console.WriteLine($"Caught invalid quantity: {iq.Message}");
        }
    }
}
