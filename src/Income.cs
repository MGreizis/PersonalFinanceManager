using System;

namespace PersonalFinanceManager
{
  public class Income
  {
    public required int Id { get; set; }
    public required decimal Amount { get; set; }
    public required string Source { get; set; }
    public required DateTime Date { get; set; }

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