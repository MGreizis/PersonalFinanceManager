using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace PersonalFinanceManager
{
  public class User
  {
    public required string Username { get; set; }
    public required string Password { get; set; }

    private static string GetUserDataFilePath()
    {
      string projectRoot = AppDomain.CurrentDomain.BaseDirectory!;
      string relativePath = Path.Combine(projectRoot, @"..\..\..\src\userdata.json"); // todo: different file based on who signs up
      return Path.GetFullPath(relativePath);
    }

    public static void SignUp()
    {
      Console.WriteLine("Enter a username: ");
      string username = Console.ReadLine()!;

      Console.WriteLine("Enter a password: ");
      string password = Console.ReadLine()!;

      User newUser = new User
      {
        Username = username,
        Password = password
      };

      SaveUser(newUser);
      Console.WriteLine("User created successfully!");

      MainMenu.DrawMainMenu();
    }

    private static void SaveUser(User user)
    {
      List<User> users = LoadUsers();
      users.Add(user);

      string jsonString = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
      string filePath = GetUserDataFilePath();
      File.WriteAllText(filePath, jsonString);
    }

    private static List<User> LoadUsers()
    {
      string filePath = GetUserDataFilePath();

      if (!File.Exists(filePath))
      {
        return new List<User>();
      }

      string jsonString = File.ReadAllText(filePath);
      if (string.IsNullOrWhiteSpace(jsonString))
      {
        return new List<User>();
      }
      return JsonSerializer.Deserialize<List<User>>(jsonString) ?? new List<User>();
    }

    public static bool Login()
    {
      Console.WriteLine("Enter your username: ");
      string username = Console.ReadLine()!;

      Console.WriteLine("Enter your password: ");
      string password = Console.ReadLine()!;

      List<User> users = LoadUsers();
      foreach (User user in users)
      {
        if (user.Username == username && user.Password == password)
        {
          Console.WriteLine("Login successful!");
          MainMenu.DrawMainMenu();
          return true;
        }
      }

      Console.WriteLine("Invalid username or password. Please try again.");
      return false;
    }
  }
}