namespace Airbnb.CORE.Models;

public class Host
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<Reservation> Reservations { get; set; }
    public string Email { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public Property Property { get; set; }
}