using Airbnb.CORE.Models;
using Airbnb.CORE.Repositories;
using System.Data.SqlClient;

namespace Airbnb.DAL.Database;

public class ReservationDBRepo : IReservationRepo
{
    private const string datasource = @"localhost\SQLEXPRESS";
    private const string database = "master";
    private const string username = "user";
    private const string password = "pass";
    
    private const string connectionString = @"Data Source=" + datasource + ";Initial Catalog=" + database + "Trusted_Connection=True;";
    //private const string sqlConn = "Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True";
    private SqlConnection conn;
    
    public ReservationDBRepo()
    {
        Setup();
    }

    private void Setup()
    {
        conn = new SqlConnection(connectionString);
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

    public Result<Reservation> GetReservation(int id, string hostId)
    {
        throw new NotImplementedException();
    }

    public Result<Reservation> GetReservation(int resId, int guestId, string hostId)
    {
        throw new NotImplementedException();
    }

    public Result<List<Reservation>> GetReservationsByHost(string hostId)
    {
        throw new NotImplementedException();
    }

    public Result<Reservation> CreateReservation(Reservation reservation)
    {
        throw new NotImplementedException();
    }

    public Result<Reservation> UpdateReservation(Reservation reservation)
    {
        throw new NotImplementedException();
    }

    public Result<Reservation> DeleteReservation(Reservation reservation)
    {
        throw new NotImplementedException();
    }

    public List<Reservation> GetAllReservations(List<Host> hosts)
    {
        throw new NotImplementedException();
    }
}