namespace Airbnb.CORE.Models;

public class Listing
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int HostID { get; set; }
    public string HostName { get; set; }
    public string NeighbourhoodGroup { get; set; }
    public string Neighbourhood { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string RoomType { get; set; }
    public decimal Price { get; set; }
    public int MinimumNights { get; set; }
    public int NumberOfReviews { get; set; }
    public DateTime LastReview { get; set; }
    public int ReviewsPerMonth { get; set; }
    public int CalcHostListingCount { get; set; }
    public int Availability365 { get; set; }
}