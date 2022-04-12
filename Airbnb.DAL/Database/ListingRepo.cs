using Airbnb.CORE.Models;
using Airbnb.CORE.Repositories;
using System.Data.SqlClient;
using System.Linq;
namespace Airbnb.DAL.Database;

public class ListingRepo : IListingRepo
{
    // private const string datasource = @"localhost\local-sql-express";
    // private const string database = "AirbnbNewYork";
    // private const string username = "sa";
    // private const string password = "Fhlipoiop122!";
    //
    // private const string connectionString = @"Data Source=" + datasource + ";Initial Catalog=" + database + "Trusted_Connection=True;" + "User ID=" + username + ";Password=" + password;
    // private const string sqlConn = "Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True";
    
    // WORKING CONNECTION STRING
    private const string tester = "Server=localhost;Database=AirbnbNewYork;User Id=sa;Password=Fhlipoiop122!";
    private SqlConnection conn;
    
    public ListingRepo()
    {
        Setup();
    }

    private void Setup()
    {
        conn = new SqlConnection(tester);
        try
        {
            conn.Open();
            Console.WriteLine("Connected to database");
        }catch (Exception e)
        {
            Console.WriteLine("Error connecting to database");
            Console.WriteLine(e.Message);
        }
    }

    public List<Guest> GetAllGuests()
    {
        string sql = "SELECT * FROM Guest";
        List<Guest> ret = new List<Guest>();
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Guest g = new Guest();
            g.Id = (int)reader["guest_id"];
            g.FirstName = (string)reader["first_name"];
            g.LastName = (string)reader["last_name"];
            g.Email = (string)reader["email"];
            g.Phone = (string)reader["phone"];
            g.State = (string)reader["state"];
            ret.Add(g);
        }
        return ret;
    }

    public List<Host> GetAllHosts()
    {
        string sql = "SELECT * FROM Host";
        List<Host> ret = new List<Host>();
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Host host = new Host();
            host.Id = (string)reader["id"];
            host.LastName = (string)reader["last_Name"];
            host.Email = (string)reader["email"];
            host.Phone = (string)reader["phone"];
            host.Address = (string)reader["address"];
            host.City = (string)reader["city"];
            host.State = (string)reader["state"];
            host.PostalCode = (int)reader["postal_code"];
            host.weekdayRate = (int)reader["standard_rate"];
            host.weekendRate = (int)reader["weekend_rate"];
            ret.Add(host);
        }
        return ret;
    }

    public List<Reservation> GetAllReservations()
    {
        string sql = "SELECT * FROM Reservation WHERE 1=1";
        List<Reservation> ret = new List<Reservation>();
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Reservation reservation = new Reservation();
            Guest guest = new Guest();
            Host host = new Host();
            reservation.id = (int)reader["id"];
            DateTime start = (DateTime)reader["start_Date"];
            DateTime end = (DateTime)reader["end_Date"];
            guest.Id = (int)reader["guest_id"];
            reservation.totalPrice = (decimal)reader["total"];
            host.Id = (string)reader["host_id"];
            reservation.guest = guest;
            reservation.host = host;
            reservation.startDate = DateOnly.FromDateTime(start);
            reservation.endDate = DateOnly.FromDateTime(end);
            ret.Add(reservation);
        }
        return ret;
    }

    public List<Reservation> GetReservationsByHostId(string hostId)
    {
        string sql = $"SELECT * FROM Reservation WHERE host_id ={hostId}";
        List<Reservation> ret = new List<Reservation>();
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Reservation reservation = new Reservation();
            Guest guest = new Guest();
            Host host = new Host();
            reservation.id = (int)reader["id"];
            DateTime start = (DateTime)reader["start_Date"];
            DateTime end = (DateTime)reader["end_Date"];
            guest.Id = (int)reader["guest_id"];
            reservation.totalPrice = (decimal)reader["total"];
            host.Id = (string)reader["host_id"];
            reservation.guest = guest;
            reservation.host = host;
            reservation.startDate = DateOnly.FromDateTime(start);
            reservation.endDate = DateOnly.FromDateTime(end);
            ret.Add(reservation);
        }
        return ret;
    }

    // public List<Listing> GetListingsByHostId(int hostId)
    // {
    //     List<Listing> ret = new List<Listing>();
    //     SqlCommand cmd = new SqlCommand("SELECT * FROM Listing WHERE HostID = @hostId", conn);
    //     SqlDataReader reader = cmd.ExecuteReader();
    //     while (reader.Read())
    //     {
    //         Listing listing = new Listing();
    //         listing.Id = (int)reader["Id"];
    //         listing.Name = (string)reader["Name"];
    //         listing.HostID = (int)reader["HostID"];
    //         listing.HostName = (string)reader["HostName"];
    //         listing.NeighbourhoodGroup = (string)reader["NeighbourhoodGroup"];
    //         listing.Neighbourhood = (string)reader["Neighbourhood"];
    //         listing.Latitude = (decimal)reader["Latitude"];
    //         listing.Longitude = (decimal)reader["Longitude"];
    //         listing.RoomType = (string)reader["RoomType"];
    //         listing.Price = (int)reader["Price"];
    //         listing.MinimumNights = (int)reader["MinimumNights"];
    //         listing.NumberOfReviews = (int)reader["NumberOfReviews"];
    //         listing.LastReview = (string)reader["LastReview"];
    //         listing.LastReviewDate = (DateOnly)reader["LastReviewDate"];
    //         listing.ReviewsPerMonth = (double)reader["ReviewsPerMonth"];
    //         listing.CalcHostListingCount = (int)reader["CalcHostListingCount"];
    //         listing.Availability365 = (string)reader["Availability365"];
    //         ret.Add(listing);
    //     }
    //     return ret;
    // }
}