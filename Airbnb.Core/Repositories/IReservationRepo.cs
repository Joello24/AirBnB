using Airbnb.CORE.Models;

namespace Airbnb.CORE.Repositories;

public interface IReservationRepo
{
    Result<Reservation> GetReservation(int id, string hostId);
    Result<List<Reservation>> GetReservationsByHost(string hostId);
    
    Result<Reservation> CreateReservation(Reservation reservation);
    Result<Reservation> UpdateReservation(Reservation reservation);
    Result<Reservation> DeleteReservation(Reservation reservation);

}