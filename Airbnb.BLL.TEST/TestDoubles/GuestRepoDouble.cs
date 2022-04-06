using System.Collections.Generic;
using Airbnb.CORE.Models;
using Airbnb.CORE.Repositories;

namespace Airbnb.BLL.TEST.TestDoubles;

public class GuestRepoDouble : IGuestRepo
{

    public static readonly List<Guest> Guests = new List<Guest>();

    public static readonly Guest Guest1 = new Guest()
    {
        Id = 1,
        FirstName = "John",
        LastName = "Doe",
        Email = "doe@gmail.com",
        Phone = "123-456-7890",
        State = "CA"
    };
    public static readonly Guest Guest2 = new Guest()
    {
        Id = 2,
        FirstName = "John",
        LastName = "Doe",
        Email = "doe@gmail.com",
        Phone = "123-456-7890",
        State = "CA"
    };
    public static readonly Guest Guest3 = new Guest()
    {
        Id = 3,
        FirstName = "John",
        LastName = "Doe",
        Email = "doe@gmail.com",
        Phone = "123-456-7890",
        State = "CA"
    };
    public GuestRepoDouble()
    {
        Guests.Add(Guest1);
        Guests.Add(Guest2);
        Guests.Add(Guest3);
    }
    public Result<List<Guest>> FindAll()
    {
        Result<List<Guest>> result = new Result<List<Guest>>();
        result.Value = Guests;
        return result;
    }

    public Result<Guest> FindByEmail(string email)
    {
        Result<Guest> result = new Result<Guest>();
        result.Value = Guests.Find(g => g.Email == email);
        return result;
    }

    public Result<Guest> Create(Guest guest)
    {
        throw new System.NotImplementedException();
    }

    public Result<Guest> Update(Guest guest)
    {
        throw new System.NotImplementedException();
    }

    public Result<Guest> Delete(int id)
    {
        throw new System.NotImplementedException();
    }
}