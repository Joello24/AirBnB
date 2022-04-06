namespace Airbnb.CORE.Models;

public class Host
{
    public string Phone;
    public string Id { get; set; }
    public string LastName { get; set; }
    public List<Reservation> Reservations { get; set; }
    public string Email { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    
    public Property Property { get; set; }
    public decimal weekdayRate { get; set; }
    public decimal weekendRate { get; set; }
    public string Address { get; set; }
    public int PostalCode { get; set; }

    public override string ToString()
    {
        string ret = $"{Id}: {Email}-{LastName}\nAddress:{Address}, {City} {State}, {PostalCode}.\n Rates: {weekdayRate}-{weekendRate}";
        return ret;
    }
}