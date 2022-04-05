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
        return $"{id}-{startDate}-{endDate}\n Guest: {guest} \n Host: {host} \n Total:{totalPrice:C}";
    }
}