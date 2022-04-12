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
    
    public Listing listing { get; set; }
    public int weekdayRate { get; set; }
    public decimal weekendRate { get; set; }
    public string Address { get; set; }
    public int PostalCode { get; set; }

    public override string ToString()
    {
        string ret = $"ID: {Id}:\nEmail: {Email}\nLastname: {LastName}\nAddress: {Address}, {City} {State}, {PostalCode}.\nStandard Rate: {weekdayRate:C}, Weekend Rate: {weekendRate:C}";
        return ret;
    }
}
