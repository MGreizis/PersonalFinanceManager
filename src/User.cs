using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace PersonalFinanceManager
{
  public class User
  {
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
    public required string Salt { get; set; }

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

      string salt = GenerateSalt();

      string passwordHash = HashPassword(password, salt);

      User newUser = new User
      {
        Username = username,
        PasswordHash = passwordHash,
        Salt = salt
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

      var options = new JsonSerializerOptions
      {
        PropertyNameCaseInsensitive = true
      };

      var users = JsonSerializer.Deserialize<List<NullableUser>>(jsonString, options);

      var finalUsers = new List<User>();
      foreach (var nullableUser in users)
      {
        finalUsers.Add(new User
        {
          Username = nullableUser.Username ?? string.Empty,
          PasswordHash = nullableUser.PasswordHash ?? string.Empty,
          Salt = nullableUser.Salt ?? string.Empty
        });
      }

      return finalUsers;
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
        if (user.Username == username)
        {
          string passwordHash = HashPassword(password, user.Salt);
          if (user.PasswordHash == passwordHash)
          {
            Console.WriteLine("Login successful!");
            MainMenu.DrawMainMenu();
            return true;
          }
          break;
        }
      }

      Console.WriteLine("Invalid username or password. Please try again.");
      return false;
    }

    private static string GenerateSalt()
    {
      byte[] saltBytes = new byte[16];
      using (var rng = RandomNumberGenerator.Create())
      {
        rng.GetBytes(saltBytes);
      }
      return Convert.ToBase64String(saltBytes);
    }

    private static string HashPassword(string password, string salt)
    {
      byte[] saltBytes = Convert.FromBase64String(salt);
      byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
      byte[] passwordWithSaltBytes = new byte[saltBytes.Length + passwordBytes.Length];

      Buffer.BlockCopy(saltBytes, 0, passwordWithSaltBytes, 0, saltBytes.Length);
      Buffer.BlockCopy(passwordBytes, 0, passwordWithSaltBytes, saltBytes.Length, passwordBytes.Length);

      using (var sha256 = SHA256.Create())
      {
        byte[] hashBytes = sha256.ComputeHash(passwordWithSaltBytes);
        return Convert.ToBase64String(hashBytes);
      }
    }

    private class NullableUser
    {
      public string? Username { get; set; }
      public string? PasswordHash { get; set; }
      public string? Salt { get; set; }
    }
  }
}