using Airbnb.CORE.Models;
using Airbnb.CORE.Repositories;

namespace Airbnb.BLL;

public class ListingService
{
    
    private readonly IListingRepo _listingRepository;
    
    public ListingService(IListingRepo listingRepository)
    {
        _listingRepository = listingRepository;
    }
    
    public List<Reservation> GetAllListings()
    {
        return _listingRepository.GetAllReservations();
    }
    
    public List<Reservation> GetListingsByHostId(string hostId)
    {
        return _listingRepository.GetReservationsByHostId(hostId);
    }

    public List<Guest> GetAllGuests()
    {
        return _listingRepository.GetAllGuests();
    }

    public List<Host> GetAllHosts()
    {
        return _listingRepository.GetAllHosts();
    }
    
    


}