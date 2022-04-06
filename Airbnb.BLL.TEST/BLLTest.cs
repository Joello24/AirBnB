using Airbnb.BLL.TEST.TestDoubles;
using Airbnb.CORE.Models;
using NUnit.Framework;

namespace Airbnb.BLL.TEST;

public class GuestServiceTests
{
    GuestService guestService  = new GuestService(new GuestRepoDouble());
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void GetAllGuests()
    {
        var result = guestService.GetAll();
        Assert.IsNotNull(result);
        Assert.AreEqual(3, result.Value.Count);
    }
}
public class HostServiceTests
{
    HostService hostService  = new HostService(new HostRepoDouble());
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void FindByEmail()
    {
        var result = hostService.FindByEmail("fakeemail3.com");
        
        Assert.IsNotNull(result);
        Assert.AreEqual("fakeid3", result.Value.Id);
    }
    [Test]
    public void FindAll()
    {
        var result = hostService.FindAll();
        Assert.IsNotNull(result);
        Assert.AreEqual(3, result.Value.Count);
    }
    [Test]
    public void FindById()
    {
        var result = hostService.FindByNameSearch("Smith");
        Assert.IsNotNull(result);
        Assert.AreEqual(3, result.Count);
    }
}
public class ReservationServiceTests
{
    ReservationService reservationService  = new ReservationService(new ResRepoDouble(), new HostRepoDouble(), new GuestRepoDouble());
    [SetUp]
    public void Setup()
    {
        
    }

    //[Test]
    // public void FindByHostandGuestID()
    // {
    //     var result = reservationService.GetReservation(2, "fakeid2");
    //     Assert.IsNotNull(result);
    //     Assert.AreEqual(3, result.Value.id);
    // }
    // [Test]
    // public void FindByHostId()
    // {
    //     var result = reservationService.GetReservations("fakeid2");
    //     Assert.IsNotNull(result);
    //     Assert.AreEqual(2, result.Value.Count);
    // }
}