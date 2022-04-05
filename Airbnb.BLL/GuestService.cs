using Airbnb.CORE.Models;
using Airbnb.CORE.Repositories;
using Airbnb.DAL;

namespace Airbnb.BLL;

public class GuestService
{
    private readonly IGuestRepo _guestRepository;
    public GuestService(IGuestRepo guestRepository)
    {
        _guestRepository = guestRepository;
    }
    public Result<List<Guest>> GetAll()
    {
        return _guestRepository.FindAll();
    }
    public List<Guest> FindByNameSearch(string namePrefix)
    {
        return _guestRepository.FindAll().Value
            .Where(x => x.LastName.StartsWith(namePrefix)).ToList();
    }
    public Result<Guest> FindByEmail(string email)
    {
        var ret = new Result<Guest>
        {
            Value = _guestRepository.FindAll().Value.FirstOrDefault(x => x.Email == email)
        };
        if (ret.Value == null)
        {
            ret.AddMessage("Guest not found");
        }
        return ret;
    }
    public Result<Guest> FindById(int id)
    {
        var ret = new Result<Guest>
        {
            Value = _guestRepository.FindAll().Value.FirstOrDefault(x => x.Id == id)
        };
        return ret;
    }
    public Result<Guest> Create(Guest guest)
    {
        var ret = new Result<Guest>();
        ret = _guestRepository.Create(guest);
        return ret;
    }
    public Result<Guest> Update(Guest guest)
    {
        var ret = new Result<Guest>();
        ret = _guestRepository.Update(guest);
        return ret;
    }
    public Result<Guest> Delete(int id)
    {
        var ret = new Result<Guest>();
        ret = _guestRepository.Delete(id);
        return ret;
    }
    
    
}