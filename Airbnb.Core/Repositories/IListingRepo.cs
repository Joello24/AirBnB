using Airbnb.CORE.Models;

namespace Airbnb.CORE.Repositories;

public interface IListingRepo
{
    List<Reservation> GetAllReservations();
    List<Reservation> GetReservationsByHostId(string hostId);
    
    List<Guest> GetAllGuests();
    List<Host> GetAllHosts();
}