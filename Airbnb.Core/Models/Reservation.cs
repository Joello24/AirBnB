namespace Airbnb.CORE.Models;

public class Reservation
{
    public int id { get; set; }
    public DateTime startDate { get; set; }
    public DateTime endDate { get; set; }
    public Guest guest { get; set; }
    public Host host { get; set; }
    public Property property { get; set; }
}