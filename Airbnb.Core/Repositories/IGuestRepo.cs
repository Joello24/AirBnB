using Airbnb.CORE.Models;

namespace Airbnb.CORE.Repositories;

public interface IGuestRepo
{
    Result<List<Guest>> FindAll();
    //Result<Guest> FindByEmail(string email);
    // Result<Guest> Create(Guest guest);
    // Result<Guest> Update(Guest guest);
    // Result<Guest> Delete(int id);
}