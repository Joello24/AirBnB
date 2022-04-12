using System.Net;
using Airbnb.Core;
using Airbnb.CORE.Models;
using Airbnb.CORE.Repositories;

namespace Airbnb.DAL;

public class ReservationFileRepository : IReservationRepo
{
    private string  _dirPath;
    private ILogger _logger;
    private const string HEADER = "id,start_date,end_date,guest_id,total";
    
    public ReservationFileRepository(ILogger logger, string filePath)
    {
        _logger = logger;
        _dirPath = filePath;
    }
    public Result<Reservation> GetReservation(int id, string hostId)
    {
        Result<Reservation> result = new Result<Reservation>();
        List<Reservation> reservations = new List<Reservation>();
        var grabber = new Result<List<Reservation>>();
        try
        {
            grabber = GetReservationsByHost(hostId);
        }
        catch (Exception e)
        {
            _logger.Log(e.ToString());
            throw;
        }

        if(grabber.Success)
            reservations = grabber.Value;
        else
        {
            result.AddMessage("Host doesn't have any reservations");
            return result;
        }

        result.Value = reservations.Find(r => r.guest.Id == id);
        if (result.Value == null)
        {
            result.AddMessage("Reservation not found");
        }
        return result;
    }
    public Result<Reservation> GetReservation(int reservationId, int guestId, string hostId)
    {
        Result<Reservation> result = new Result<Reservation>();
        List<Reservation> reservations = new List<Reservation>();
        var grabber = new Result<List<Reservation>>();
        try
        {
            grabber = GetReservationsByHost(hostId);
        }
        catch (Exception e)
        {
            _logger.Log(e.ToString());
            throw;
        }

        if(grabber.Success)
            reservations = grabber.Value;
        else
        {
            result.AddMessage("Host doesn't have any reservations");
            return result;
        }

        result.Value = reservations.Find(r => r.guest.Id == guestId && r.id == reservationId);
        if (result.Value == null)
        {
            result.AddMessage("Reservation not found");
        }
        return result;
    }

    public Result<List<Reservation>> GetReservationsByHost(string hostId)
    {
        var result = new Result<List<Reservation>>();
        var reservations = new List<Reservation>();
        var path = Path.Combine(_dirPath, hostId + ".csv");
        if (!File.Exists(path))
        {
            result.AddMessage("Host currently has no reservations.");
            return result;
        }
        
        string[] lines = null;
        try
        {
            lines = File.ReadAllLines(path);
        }
        catch (Exception ex)
        {
            _logger.Log(ex.Message);
            result.Messages.Add(ex.Message);
            return result;
        }
        
        foreach (var line in lines.Skip(1))
        {
            string[] columns = line.Split(',', StringSplitOptions.TrimEntries);
            Reservation reservation = DeserializeReservation(columns, hostId);
            if(reservation != null)
            {
                reservations.Add(reservation);
            }
        }
        result.Value = reservations;
        return result;
    }

    public List<Reservation> GetAllReservations()
    {
        throw new NotImplementedException();
    }

    private Reservation DeserializeReservation(string[] columns, string hostID)
    {
        Reservation ret = new Reservation();
        Guest guest = new Guest();
        Host host = new Host();
        host.Id = hostID;
        ret.id = int.Parse(columns[0]);
        ret.startDate = DateOnly.Parse(columns[1]);
        ret.endDate = DateOnly.Parse(columns[2]);
        guest.Id = int.Parse(columns[3]);
        ret.totalPrice = decimal.Parse(columns[4]);
        ret.guest = guest;
        ret.host = host;
        return ret;
    }
    
    public Result<Reservation> CreateReservation(Reservation reservation)
    {
        int nextId = 0;
        Result<Reservation> result = new Result<Reservation>();
        var path = Path.Combine(_dirPath, reservation.host.Id + ".csv");
        Result<List<Reservation>> current = GetReservationsByHost(reservation.host.Id);
        
        if (!current.Success && !File.Exists(path))
        {
            List<Reservation> newResFile = new List<Reservation>();
            reservation.id = nextId;
            newResFile.Add(reservation);
            Write(newResFile);
            result.Value = reservation;
            return result;
        }
        nextId = (current.Value.Count == 0 ? 0 : current.Value.Max(i => i.id)) + 1;
        reservation.id = nextId;
        current.Value.Add(reservation);
        Write(current.Value);
        result.Value = reservation;
        return result;
    }

    private void Write(List<Reservation> current)
    {
        var path = Path.Combine(_dirPath, current[0].host.Id + ".csv");
        try
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(HEADER);
                foreach (var reservation in current)
                {
                    sw.WriteLine(SerializeReservation(reservation));
                }
            }
        }
        catch (Exception ex)
        {
            _logger.Log(ex.Message);
        }
    }

    private string SerializeReservation(Reservation reservation)
    {
        return $"{reservation.id},{reservation.startDate},{reservation.endDate},{reservation.guest.Id},{reservation.totalPrice}";
    }

    public Result<Reservation> UpdateReservation(Reservation reservation)
    {
        var reservations = GetReservationsByHost(reservation.host.Id).Value;
        Result<Reservation> result = new Result<Reservation>();
        Reservation res = reservations.Find(r => r.id == reservation.id);
        if (res == null)
        {
            result.AddMessage("Reservation not found");
            return result;
        }
        res.startDate = reservation.startDate;
        res.endDate = reservation.endDate;
        res.guest = reservation.guest;
        res.totalPrice = reservation.totalPrice;
        Write(reservations);
        result.Value = res;
        return result;
    }
    public Result<Reservation> DeleteReservation(Reservation reservation)
    {
        var reservations = GetReservationsByHost(reservation.host.Id).Value;
        Result<Reservation> result = new Result<Reservation>();
        Reservation res = reservations.Find(r => r.id == reservation.id);
        if (res == null)
        {
            result.AddMessage("Reservation not found");
            return result;
        }
        reservations.Remove(res);
        if (reservations.Count == 0)
        {
            File.Delete(Path.Combine(_dirPath, reservation.host.Id + ".csv"));
        }
        else
        {
            Write(reservations);
        }
        result.Value = reservation;
        return result;
    }
    
    
}