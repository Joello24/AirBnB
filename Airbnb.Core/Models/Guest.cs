namespace Airbnb.CORE.Models;

public class Guest
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string State { get; set; }

    public override string ToString()
    {
        string ret = $"ID: {Id}, Name: {FirstName} {LastName}, Email: {Email}, Phone: {Phone}, State: {State}";
        return ret;
    }
}