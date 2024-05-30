using System;

namespace PersonalFinanceManager
{
  public class Income
  {
    public required int Id { get; set; }
    public required decimal Amount { get; set; }
    public required string Source { get; set; }
    public required DateTime Date { get; set; }

    public static void ViewIncomeMenu() {
      Console.WriteLine("Choose an action: ");
      Console.WriteLine("1. Add income" + "\n2. Update income" + "\n3. Delete income");
      string input = Console.ReadLine();

      if (input == "1")
      {
        Console.WriteLine("Add income");
      }
      else if (input == "2")
      {
        Console.WriteLine("Update income");
      }
      else if (input == "3")
      {
        Console.WriteLine("Delete income");
      }
      else
      {
        Console.WriteLine("Invalid input. Please try again.");
        ViewIncomeMenu();
      }
    }

    public static void AddIncome(Income income)
    {
      // todo
    }

    public static void UpdateIncome(Income income)
    {
      // todo
    }

    public static void DeleteIncome(int id)
    {
      // todo
    }
  }
}