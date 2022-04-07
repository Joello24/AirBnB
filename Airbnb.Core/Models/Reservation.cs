using Airbnb.Core.Enums;

namespace Airbnb.CORE.Models;

public class Reservation
{
    public int id { get; set; }
    public DateOnly startDate { get; set; }
    public DateOnly endDate { get; set; }
    public Guest guest { get; set; }
    public Host host { get; set; }
    public Property property { get; set; }
    public decimal totalPrice { get; set; }

    public ColorScheme colorScheme { get; set; }
    public override string ToString()
    {
        return 
               $"Reservation: {id}-{startDate}-{endDate}\n" + 
               $"Guest: {guest}\n" +
               $"Host: {host}\n" +
               $"\nTotal:{totalPrice:C}";
    }
    public void Print()
    {
        switch (colorScheme)
        {
            case ColorScheme.Colorful:
                PrintColorful();
                break;
            case ColorScheme.Light:
                PrintLight();
                break;
            case ColorScheme.Dark:
                PrintDark();
                break;
            default:
                PrintDefault();
                break;
        }
    }

    private void PrintDefault()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"\n***Reservation***\n#{id} for host {host.Id}");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"From {startDate} to {endDate}");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"***GUEST***");
        Console.WriteLine($"{guest}");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"***HOST***");
        Console.WriteLine($"{host}");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Total: {totalPrice:C}\n");
        Console.ResetColor();
    }

    public void PrintColorful()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.BackgroundColor = ConsoleColor.White;
        Console.WriteLine($"\n***Reservation***\n#{id} for host {host.Id}");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.BackgroundColor = ConsoleColor.Green;
        Console.WriteLine($"From {startDate} to {endDate}");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.BackgroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"***GUEST***");
        Console.WriteLine($"{guest}");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"***HOST***");
        Console.WriteLine($"{host}");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.BackgroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"Total: {totalPrice:C}\n");
        Console.ResetColor();
    }
    public void PrintLight()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.BackgroundColor = ConsoleColor.White;
        Console.WriteLine($"\n***Reservation***\n#{id} for host {host.Id}");
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"From {startDate} to {endDate}");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.BackgroundColor = ConsoleColor.White;
        Console.WriteLine($"***GUEST***");
        Console.WriteLine($"{guest}");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.BackgroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"***HOST***");
        Console.WriteLine($"{host}");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.BackgroundColor = ConsoleColor.White;
        Console.WriteLine($"Total: {totalPrice:C}\n");
        Console.ResetColor();
    }
    public void PrintDark()
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine($"\n***Reservation***\n#{id} for host {host.Id}");
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine($"From {startDate} to {endDate}");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"***GUEST***");
        Console.WriteLine($"{guest}");
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine($"***HOST***");
        Console.WriteLine($"{host}");
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"Total: {totalPrice:C}\n");
        Console.ResetColor();
    }
    
}