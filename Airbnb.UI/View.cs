using Airbnb.CORE;
using Airbnb.CORE.Models;

namespace Airbnb.UI;

public static class View
{

  public static bool boringColorsOn = false;
  public static bool customColorOn = false;
  public static ConsoleColor customColor = ConsoleColor.White;
  public static int GetLoggingMode()
  {
    DisplayGreen("\n\nINITIAL CONIFIGURATION");
    return (int)Validation.PromptUser4Num(@"What logging mode do you want to use?
1. No logging
2. Log to console
3. Log to file
Select Mode: ", 1, 3);
  }
  
  public static int GetColorScheme()
  {
    DisplayGreen("\n\nINITIAL CONIFIGURATION");
    return (int)Validation.PromptUser4Num(@"What color scheme do you want to use?
1. Colorful
2. Light
3. Dark
Select Mode: ", 1, 3);
  }

  internal static bool Confirm(string message = "Are you sure?")
  {
    Console.Write(message);
    var input = Console.ReadLine() ?? string.Empty;

    return input.ToLower().StartsWith("y");
  }

  internal static void Display(string message)
  {
    Console.WriteLine(message);
  }

  public static void DisplayHeader(string header)
  {
    Random rnd = new Random();
    var consoleColors = Enum.GetValues(typeof(ConsoleColor));
  
    Console.ForegroundColor = ConsoleColor.Red;
    Console.BackgroundColor = (ConsoleColor)consoleColors.GetValue(rnd.Next(consoleColors.Length));
    Console.WriteLine();
    foreach (var _ in Enumerable.Range(0, header.Length))
    {
      DisplayInLine("-");
    }
    Console.WriteLine();
    Display(header);
    foreach (var _ in Enumerable.Range(0, header.Length))
    {
      DisplayInLine("-");
    }
    Console.WriteLine();
    Console.ResetColor();
  }

  public static int GetApplicationMode()
  {
    DisplayHeader("Welcome to the AIR-BnB. Please select an option:");
    return (int)Validation.PromptUser4Num(@"What mode would you like to run in?
1. Live
2. Test
Select mode: ", 1, 2);
  }

  internal static int GetMainChoice()
  {
    DisplayHeader("Main Menu");

    return (int)Validation.PromptUser4Num($@"
0. Exit
1. View Reservations for Host
2. Make a Reservation
3. Edit a Reservation
4. Cancel a Reservation
99. Settings
Enter Choice: ", 0, 6, 99);
  }
  
  internal static int GetSettingsChoice()
  {
    DisplayHeader("Settings Menu");

    return (int)Validation.PromptUser4Num($@"
0. Exit
1. Color Scheme
2. Delay Timer
Enter Choice: ", 0, 6);
  }

  public static void Tilde()
  {
    Console.ForegroundColor = ConsoleColor.Green;
    DisplayInLine("~ ");
  }

  public static Guest GetGuest()
  {
    var guest = new Guest();

    guest.FirstName = Validation.PromptRequired("Enter your first name: ");
    guest.LastName = Validation.PromptRequired("Enter your last name: ");
    guest.Email = Validation.PromptRequired("Enter your email: ");
    return guest;
  }

  public static void DisplayRed(string required)
  {
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(required);
    Console.ResetColor();
  }

  public static void LineBreak()
  {
    Console.WriteLine();
  }

  public static void DisplayInLine(string message)
  {
    Console.Write(message);
  }

  public static string GetGuestPrefix()
  {
    throw new NotImplementedException();
  }
  public static int GetSearchMethod(string type)
  {
    Console.ForegroundColor = ConsoleColor.Blue;
    Display($"\nSelecting {type}, Choose a Search Method");
    Console.ResetColor();
    Console.ForegroundColor = ConsoleColor.Red;
    Display("0. Exit");
    Console.ForegroundColor = ConsoleColor.Green;
    Display("1. Email");
    Display("2. Name");
    Console.ResetColor();
    return (int)Validation.PromptUser4Num("Enter your choice [0-2]: ", 0, 2);
  }
  public static string GetNamePrefix()
  {
    bool isValid = false;
    string ret = "";
    while (!isValid)
    {
      ret = Validation.ReadRequiredString("Name starts with: ");
      if (ret.Contains(","))
      {
        Display("Name cannot contain a comma.");
      }
      else
      {
        isValid = true;
      }
    }
    return ret;
  }

  public static void DisplayGuests(List<Guest> guests)
  {
    if(guests == null || guests.Count == 0)
    {
      Display("No guests found");
    }
    int index = 1;
    foreach(Guest guest in guests.Take(25))
    { 
      Display($"{index++}: {guest.FirstName} {guest.LastName}");
    }

    if(guests.Count > 25)
    {
      Display("More than 25 guests found. Showing first 25. Please refine your search.");
    }
  }

  public static void DisplayHosts(List<Host> hosts)
  {
    if(hosts == null || hosts.Count == 0)
    {
      Display("No hosts found");
    }
    int index = 1;
    string filePopIndicator = "";
    foreach(Host host in hosts.Take(25))
    {
      filePopIndicator = host.Reservations == null ? "Empty" : host.Reservations.Count.ToString() + " Reservations";
      if (filePopIndicator == "Empty")
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Display($"{index++}: {host.LastName}- {filePopIndicator}");
      }
      else
      {
        Console.ForegroundColor = ConsoleColor.Green;
        Display($"{index++}: {host.LastName}- {filePopIndicator}");
      }
       Console.ResetColor();
    }

    if(hosts.Count > 25)
    {
      Display("More than 25 hosts found. Showing first 25. Please refine your search.");
    }
  }

  public static DateOnly GetDate(string message)
  {
    return Validation.PromptUser4Date(message);
  }

  public static DateOnly EditReservationDate(DateOnly date, string description)
  {
    return Validation.PromptUser4Date($"{description} ({date}): ", date);
  }

  public static void DisplayGreen(string message)
  {
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine(message);
    Console.ResetColor();
  }

  public static void DisplayResultMessages(List<string> resultMessages)
  {
    foreach (var m in resultMessages)
    {
      DisplayRed(m);
    }
  }
  public static void HandleListResult(Result<List<Reservation>> result)
  {
    if (result.Success)
    {
      foreach (var i in result.Value)
      {
        if (i.GetType() == typeof(Reservation))
        {
          i.Print();
        }
        else
        {
          View.Display($"{i}");
          View.LineBreak();
        }
      }
    }
    else
    {
      View.DisplayResultMessages(result.Messages);
    }
  }
  public static void HandleListResult(Result<List<Reservation>> result, string successMessage)
  {
    if (result.Success)
    {
      DisplayGreen(successMessage);
      foreach (var reservation in result.Value)
      {
        View.Display($"{reservation}");
        View.LineBreak();
      }
    }
    else
    {
      View.DisplayResultMessages(result.Messages);
    }
  }
  public static void HandleSingleResult(Result<Reservation> result, string successMessage)
  {
    if (result.Success)
    {
      Console.WriteLine();
      DisplayGreen(successMessage);
      result.Value.Print();
    }
    else
    {
      View.DisplayResultMessages(result.Messages);
    }
  }
  public static void HandleSingleResult(Result<Reservation> result)
  {
    if (result.Success)
    {
      result.Value.Print();
    }
    else
    {
      View.DisplayResultMessages(result.Messages);
    }
  }

  public static Reservation GetReservationNumber(List<Reservation> options)
  { 
    foreach (var r in options)
    {
      r.Print();
    }
    int choice = Validation.PromptForResID("Enter reservation number: ",options);
    return options.Find(r=>r.id == choice);
  }
}
