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
}