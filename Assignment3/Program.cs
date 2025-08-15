using System;
class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nSelect question to run (1-5) or 0 to exit:");
            Console.WriteLine("1. Finance system");
            Console.WriteLine("2. Health system");
            Console.WriteLine("3. Warehouse inventory");
            Console.WriteLine("4. Student file processor");
            Console.WriteLine("5. Inventory logger");
            Console.Write("Choice: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    new FinanceApp().Run();
                    break;
                case "2":
                    var hs = new HealthSystemApp();
                    hs.SeedData();
                    hs.BuildPrescriptionMap();
                    hs.PrintAllPatients();
                    Console.Write("Enter patient id to view prescriptions: ");
                    if (int.TryParse(Console.ReadLine(), out var pid)) hs.PrintPrescriptionsForPatient(pid);
                    break;
                case "3":
                    var wm = new WareHouseManager();
                    wm.SeedData();
                    Console.WriteLine("\nGroceries:");
                    wm.PrintAllItems(new InventoryRepository<GroceryItem>()); // but our repo inside manager had data; demonstrate using manager:
                    // For quick demo, use manager's methods:
                    Console.WriteLine("All Electronics and Groceries from manager:");
                    wm.PrintAllItems(new InventoryRepository<ElectronicItem>()); // note: if you want to show seed, adapt to use manager's internal repos or expose them.
                    wm.DemoExceptions();
                    break;
                case "4":
                    var processor = new StudentResultProcessor();
                    try
                    {
                        Console.Write("Input file path: ");
                        var input = Console.ReadLine() ?? "students.txt";
                        var students = processor.ReadStudentsFromFile(input);
                        Console.Write("Output file path: ");
                        var outp = Console.ReadLine() ?? "report.txt";
                        processor.WriteReportToFile(students, outp);
                        Console.WriteLine($"Report written to {outp}");
                    }
                    catch (System.IO.FileNotFoundException ex)
                    {
                        Console.WriteLine("File not found: " + ex.Message);
                    }
                    catch (InvalidScoreFormatException isf)
                    {
                        Console.WriteLine("Invalid score format: " + isf.Message);
                    }
                    catch (MissingFieldException mf)
                    {
                        Console.WriteLine("Missing field: " + mf.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                    break;
                case "5":
                    var app = new InventoryApp("inventory.json");
                    app.SeedSampleData();
                    app.SaveData();
                    Console.WriteLine("Saved. Clearing and reloading...");
                    app = new InventoryApp("inventory.json");
                    app.LoadData();
                    app.PrintAllItems();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid");
                    break;
            }
        }
    }
}
