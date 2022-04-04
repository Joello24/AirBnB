using Airbnb.CORE;

namespace Airbnb.UI;

public static class View
{

  public static int GetLoggingMode()
  {
    return (int)Validation.PromptUser4Num(@"What logging mode do you want to use?
1. No logging
2. Log to console
3. Log to file
", 1, 3);
  }

  internal static bool Confirm(string message = "Are you sure?")
  {
    Console.Write(message + " (y/n)");
    var input = Console.ReadLine() ?? string.Empty;

    return input.ToLower().StartsWith("y");
  }

  internal static void Display(string message)
  {
    Console.WriteLine(message);
  }

  public static void DisplayHeader(string header)
  {
    Display(header);
    Display("------------------------------------------");
  }

  public static int GetApplicationMode()
  {
    DisplayHeader("Welcome to the AIR-BnB. Please select an option:");
    return (int)Validation.PromptUser4Num(@"What mode would you like to run in?
1. Live
2. Test
Select mode: 
", 1, 2);
  }

  internal static int GetMainChoice()
  {
    DisplayHeader("Main Menu");

    return (int)Validation.PromptUser4Num(@"
0. Exit
1. View Reservations for Host
2. Make a Reservation
3. Edit a Reservation
4. Cancel a Reservation
Enter Choice: 
", 0, 4);
  }
  
}
