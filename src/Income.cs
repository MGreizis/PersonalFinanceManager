using System;
using System.Text.Json;

namespace PersonalFinanceManager
{
  public class Income
  {
    public required string Id { get; set; }
    public required decimal Amount { get; set; }
    public required string Source { get; set; }
    public required string Date { get; set; }

    private static string GetIncomeDataFilePath() {
      string projectRoot = AppDomain.CurrentDomain.BaseDirectory!;
      string relativePath = Path.Combine(projectRoot, @"..\..\..\src\incomeData.json");
      return Path.GetFullPath(relativePath);
    }

    public static void ViewIncomeMenu() {
      Console.WriteLine("Choose an action: ");
      Console.WriteLine("1. Add income" + "\n2. Update income" + "\n3. Delete income");
      string input = Console.ReadLine()!;

      if (input == "1")
      {
        AddIncome();
      }
      else if (input == "2")
      {
        UpdateIncome();
      }
      else if (input == "3")
      {
        DeleteIncome();
      }
      else
      {
        Console.WriteLine("Invalid input. Please try again.");
        ViewIncomeMenu();
      }
    }

    public static void AddIncome()
    {
      string id = Guid.NewGuid().ToString();
      string date = DateTime.Now.ToString("dd-MM-yyyy");

      Console.Write("Enter Amount: ");
      decimal amount = decimal.Parse(Console.ReadLine()!);

      Console.Write("Enter Source: ");
      string source = Console.ReadLine()!;

      Income newIncome = new Income 
      {
        Id = id,
        Amount = amount,
        Source = source,
        Date = date
      };

      List<Income> incomes = LoadIncomes();
      incomes.Add(newIncome);

      SaveIncomes(incomes);
      Console.WriteLine("Income added successfully!\n");
      MainMenu.DrawMainMenu();
    }

    public static void UpdateIncome()
    {
      Console.Write("Enter the ID of the income you want to update: ");
      string id = Console.ReadLine()!;

      List<Income> incomes = LoadIncomes();
      Income? incomeToUpdate = incomes.Find(income => income.Id == id);

      if (incomeToUpdate == null)
      {
        Console.WriteLine("Income not found.");
        return;
      }

      Console.WriteLine("Transaction found. Enter new amount: ");
      incomeToUpdate.Amount = decimal.Parse(Console.ReadLine()!);

      Console.WriteLine("Enter new source: ");
      incomeToUpdate.Source = Console.ReadLine()!;

      incomeToUpdate.Date = DateTime.Now.ToString("dd-MM-yyyy");
      
      SaveIncomes(incomes);
      Console.WriteLine("Income updated successfully!\n");
      MainMenu.DrawMainMenu();
    }

    public static void DeleteIncome()
    {
      Console.Write("Enter the ID of the income you want to delete: ");
      string id = Console.ReadLine()!;

      List<Income> incomes = LoadIncomes();
      Income? incomeToDelete = incomes.Find(income => income.Id == id);

      if (incomeToDelete == null)
      {
        Console.WriteLine("Income not found.");
        return;
      }

      incomes.Remove(incomeToDelete);

      SaveIncomes(incomes);
      Console.WriteLine("Income deleted successfully!\n");
      MainMenu.DrawMainMenu();
    }

    private static void SaveIncomes(List<Income> incomes)
    {
      string jsonString = JsonSerializer.Serialize(incomes, new JsonSerializerOptions { WriteIndented = true });
      string filePath = GetIncomeDataFilePath();
      File.WriteAllText(filePath, jsonString);
    }

    private static List<Income> LoadIncomes()
    {
      string filePath = GetIncomeDataFilePath();

      if (!File.Exists(filePath))
      {
        return new List<Income>();
      }

      string jsonString = File.ReadAllText(filePath);
      if (string.IsNullOrWhiteSpace(jsonString))
      {
        return new List<Income>();
      }

      return JsonSerializer.Deserialize<List<Income>>(jsonString) ?? new List<Income>();
    }
  }
}