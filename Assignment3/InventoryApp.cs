using System;

public class InventoryApp
{
    private InventoryLogger<InventoryItem> _logger;

    public InventoryApp(string filePath)
    {
        _logger = new InventoryLogger<InventoryItem>(filePath);
    }

    public void SeedSampleData()
    {
        _logger.Add(new InventoryItem(1, "Screwdriver", 10, DateTime.Now));
        _logger.Add(new InventoryItem(2, "Hammer", 5, DateTime.Now.AddDays(-1)));
        _logger.Add(new InventoryItem(3, "Nails Pack", 200, DateTime.Now.AddDays(-10)));
    }

    public void SaveData() => _logger.SaveToFile();

    public void LoadData() => _logger.LoadFromFile();

    public void PrintAllItems()
    {
        var items = _logger.GetAll();
        foreach (var it in items) Console.WriteLine(it);
    }
}
