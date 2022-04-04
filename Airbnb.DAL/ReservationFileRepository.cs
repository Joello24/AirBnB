using Airbnb.Core;
using Airbnb.CORE.Models;
using Airbnb.CORE.Repositories;

namespace Airbnb.DAL;

public class ReservationFileRepository : IReservationRepo
{
    private string  _filePath;
    private ILogger _logger;
    
    public ReservationFileRepository(ILogger logger, string filePath)
    {
        _logger = logger;
        _filePath = filePath;
    }
    public Result<Reservation> GetReservation(int id, string hostId)
    {
        throw new NotImplementedException();
    }

    public Result<List<Reservation>> GetReservationsByHost(int id, string hostId)
    {
        throw new NotImplementedException();
    }

    public Result<List<Reservation>> GetReservationsByGuest(int id, string guestId)
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

    public Result<Reservation> DeleteReservation(int id, string hostId)
    {
        throw new NotImplementedException();
    }

    public Result<List<Reservation>> GetAllReservations()
    {
        throw new NotImplementedException();
    }
}