using System;

namespace PersonalFinanceManager {
  class MainMenu {
    public static void DrawMainMenu() {
      Console.WriteLine("Choose an action: ");
      Console.WriteLine("1. Income" + "\n2. Expense" + "\n3. Balance" + "\n4. Savings" + "\n5. Report generator");
      string input = Console.ReadLine()!;

      switch (input) {
        case "1":
          Income.ViewIncomeMenu();
          break;
        case "2":
          Expense.ViewExpenseMenu();
          break;
        case "3":
          Balance.ViewBalanceMenu();
          break;
        case "4":
          Savings.ViewSavingsMenu();
          break;
        case "5":
          ReportGenerator.ViewReportGeneratorMenu();
          break;
        default:
          Console.WriteLine("Invalid input. Please try again.");
          DrawMainMenu();
          break;
      }
    }
  }
}