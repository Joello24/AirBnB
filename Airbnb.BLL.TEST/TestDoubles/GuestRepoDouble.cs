using System.Collections.Generic;
using Airbnb.CORE.Models;
using Airbnb.CORE.Repositories;

namespace Airbnb.BLL.TEST.TestDoubles;

public class GuestRepoDouble : IGuestRepo
{

    public static readonly List<Guest> Guests = new List<Guest>();

    public static readonly Guest Guest1 = new Guest()
    {
        //1,Sullivan,Lomas,slomas0@mediafire.com,(702) 7768761,NV
        
        Id = 50,
        FirstName = "Sullivan",
        LastName = "Lomas",
        Email = "slomas0@mediafire.com",
        Phone = "(702) 7768761",
        State = "NV"
    };
    public static readonly Guest Guest2 = new Guest()
    {
        //2,Olympie,Gecks,ogecks1@dagondesign.com,(202) 2528316,DC
        Id = 2,
        FirstName = "Olympie",
        LastName = "Gecks",
        Email = "ogecks1@dagondesign.com",
        Phone = "(202) 2528316",
        State = "DC"
    };
    public static readonly Guest Guest3 = new Guest()
    {
        //3,Tremain,Carncross,tcarncross2@japanpost.jp,(313) 2245034,MI
        Id = 3,
        FirstName = "Tremain",
        LastName = "Carncross",
        Email = "tcarncross2@japanpost.jp",
        Phone = "(313) 2245034",
        State = "MI"
    };
    public static readonly Guest Guest4 = new Guest()
    {
        Id = 4,
        FirstName = "Leonidas",
        LastName = "Gueny",
        Email = "lgueny3@example.com",
        Phone = "(412) 6493981",
        State = "PA"
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