using System;

namespace PersonalFinanceManager
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Welcome to your personal finance manager!");
      ChooseInitialAction();
    }

    static void ChooseInitialAction()
    {
      Console.WriteLine("Choose an action: ");
      Console.WriteLine("1. Sign up");
      Console.WriteLine("2. Login");
      string input = Console.ReadLine()!;

      if (input == "1")
      {
        User.SignUp();
      }
      else if (input == "2")
      {
        User.Login();
      }
      else
      {
        Console.WriteLine("Invalid input. Please try again.");
        ChooseInitialAction();
      }
    }
  }
}