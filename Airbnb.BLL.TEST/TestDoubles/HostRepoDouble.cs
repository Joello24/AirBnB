using System.Collections.Generic;
using Airbnb.CORE.Models;
using Airbnb.CORE.Repositories;

namespace Airbnb.BLL.TEST.TestDoubles;

public class HostRepoDouble : IHostRepo
{
    public static readonly List<Host> Hosts = new List<Host>();
    public static readonly Host Host1 = new Host()
    {
        Id = "2e72f86c-b8fe-4265-b4f1-304dea8762db",
        LastName = "de Clerk",
        Email = "kdeclerkdc@sitemeter.com",
        Phone = "(208) 9496329",
        Address = "2 Debra Way",
        City = "Boise",
        State = "ID",
        PostalCode = 83757,
        //weekdayRate = 200m,
        weekendRate = 250m
    };
    public static readonly Host Host2 = new Host()
    {
        Id = "fakeid2",
        LastName = "Smith",
        Email = "fakeemail2.com",
        Phone = "1234567890",
        Address = "123 Fake St",
        City = "Fakeville",
        State = "CA",
        PostalCode = 12345,
        weekdayRate = 100,
        weekendRate = 50
    };
    public static readonly Host Host3 = new Host()
    {
        Id = "fakeid3",
        LastName = "Smith",
        Email = "fakeemail3.com",
        Phone = "1234567890",
        Address = "123 Fake St",
        City = "Fakeville",
        State = "CA",
        PostalCode = 12345,
        weekdayRate = 100,
        weekendRate = 50
    };
    public HostRepoDouble()
    {
        Hosts.Add(Host1);
        Hosts.Add(Host2);
        Hosts.Add(Host3);
    }

    public Result<List<Host>> FindAll()
    {
        Result<List<Host>> result = new Result<List<Host>>();
        result.Value = Hosts;
        return result;
    }

    public Result<Host> FindByEmail(string email)
    {
        Result<Host> result = new Result<Host>();
        result.Value = Hosts.Find(i => i.Email == email);
        return result;
    }

    public Result<Host> Create(Host host)
    {
        throw new System.NotImplementedException();
    }

    public Result<Host> Update(Host host)
    {
        throw new System.NotImplementedException();
    }

    public Result<Host> Delete(Host host)
    {
        throw new System.NotImplementedException();
    }
}