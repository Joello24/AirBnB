namespace Airbnb.UI;

public static class Validation
{
  private static void Prompt2Continue()
  {
    Console.WriteLine("=====================================");
    Console.WriteLine("Press any key to continue...");

    Console.ReadLine();
  }

  internal static string PromptRequired(string message)
  {
    var res = PromptUser(message);
    while (string.IsNullOrEmpty(res))
    {
      Console.WriteLine("Input required❗");
      res = PromptUser(message);
    }

    return res;
  }
  public static string ReadRequiredString(string prompt)
  {
    while(true)
    {
      string result = ReadString(prompt);
      if(!string.IsNullOrWhiteSpace(result))
      {
        return result;
      }
      View.DisplayRed("REQUIRED");
    }
  }

  public static string ReadString(string prompt)
  {
    View.DisplayInLine(prompt);
    return Console.ReadLine();
  }
  internal static string PromptUser(string message)
  {
    Console.Write(message);
    return Console.ReadLine() ?? string.Empty;
  }

  internal static decimal PromptUser4Num(string message)
  {
    decimal result;
    while (!decimal.TryParse(PromptUser(message), out result))
    {
      PromptUser("Invalid Input. Press Enter to Continue");
    }
    return result;
  }

  internal static decimal PromptUser4Num(string message, decimal min, decimal max)
  {
    decimal result;
    while (!decimal.TryParse(PromptUser(message), out result) || result < min || result > max)
    {
      PromptUser($@"Invalid Input, must be between {min} and {max}
Press Enter to Continue");
    }
    return result;
  }

  // default here means it takes the absolute minimum value for a DateOnly
  internal static DateOnly PromptUser4Date(string message)
  {
    DateOnly result;
    while (!(DateOnly.TryParse(PromptUser(message), out result)))
    {
      PromptUser($"Invalid Input.");
      Prompt2Continue();
    }
    return result;
  }
}
