using System.Data.SqlClient;
using Airbnb.CORE.Models;
using Airbnb.CORE.Repositories;

namespace Airbnb.DAL.Database;

public class ReservationDatabaseRepository : IReservationRepo
{
    private SqlConnection _conn;
    public ReservationDatabaseRepository(SqlConnection conn)
    {
        _conn = conn;
        
    }
    public Result<Reservation> GetReservation(int id, string hostId)
    {
        Result<Reservation> result = new Result<Reservation>();
        List<Reservation> reservations = new List<Reservation>();
        reservations = GetAllReservations();
        Reservation reservation = reservations.Find(r => r.id == id && r.host.Id == hostId);
        if (reservation == null)
        {
            result.AddMessage("Reservation not found");
        }
        result.Value = reservation;
        return result;
    }

    public Result<Reservation> GetReservation(int resId, int guestId, string hostId)
    {
        Result<Reservation> result = new Result<Reservation>();
        List<Reservation> reservations = new List<Reservation>();
        reservations = GetAllReservations();
        Reservation reservation = reservations.Find(r => r.id == resId && r.host.Id == hostId && r.guest.Id == guestId);
        if (reservation == null)
        {
            result.AddMessage("Reservation not found");
        }
        result.Value = reservation;
        return result;
    }

    public Result<List<Reservation>> GetReservationsByHost(string hostId)
    {
        Result<List<Reservation>> result = new Result<List<Reservation>>();
        List<Reservation> reservations = new List<Reservation>();
        reservations = GetAllReservations();
        List<Reservation> hostReservations = reservations.FindAll(r => r.host.Id == hostId);
        if (hostReservations.Count == 0)
        {
            result.AddMessage("No reservations found");
        }
        result.Value = hostReservations;
        return result;
    }

    public Result<Reservation> CreateReservation(Reservation reservation)
    {
        
        Result<Reservation> result = new Result<Reservation>();
        string sql = $"INSERT INTO Reservation VALUES ({reservation.id},{reservation.startDate},{reservation.endDate},{reservation.guest.Id},{reservation.totalPrice},{reservation.host.Id})";
        List<Reservation> ret = new List<Reservation>();
        SqlCommand cmd = new SqlCommand(sql, _conn);
        try
        {   
            _conn.Open();
            cmd.ExecuteNonQuery();
            result.Value = reservation;
        }
        catch (Exception e)
        {
            result.AddMessage(e.Message);
        }
        finally
        {
            _conn.Close();
        }
        return result;
    }

    public Result<Reservation> UpdateReservation(Reservation reservation)
    {
        throw new NotImplementedException();
    }

    public Result<Reservation> DeleteReservation(Reservation reservation)
    {
        throw new NotImplementedException();
    }
    
    public List<Reservation> GetAllReservations()
    { 
        string sql = "SELECT * FROM Reservation WHERE 1=1";
        List<Reservation> ret = new List<Reservation>();
        SqlCommand cmd = new SqlCommand(sql, _conn);
        SqlDataReader reader = cmd.ExecuteReader();
        try
        {
            _conn.Open();
            while (reader.Read())
            {
                Reservation reservation = new Reservation();
                Guest guest = new Guest();
                Host host = new Host();
                reservation.id = (int) reader["id"];
                DateTime start = (DateTime) reader["start_Date"];
                DateTime end = (DateTime) reader["end_Date"];
                guest.Id = (int) reader["guest_id"];
                reservation.totalPrice = (decimal) reader["total"];
                host.Id = (string) reader["host_id"];
                reservation.guest = guest;
                reservation.host = host;
                reservation.startDate = DateOnly.FromDateTime(start);
                reservation.endDate = DateOnly.FromDateTime(end);
                ret.Add(reservation);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            reader.Close();
            _conn.Close();
        }
        return ret;
    }
}