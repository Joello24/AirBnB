using System;
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
        Assert.AreEqual(2, result.Count);
    }
}
public class ReservationServiceTests
{
    ReservationService reservationService  = new ReservationService(new ResRepoDouble(), new HostRepoDouble(), new GuestRepoDouble());
    [SetUp]
    public void Setup()
    {
    }

    // [Test]
    //  public void FindByHostandGuestID()
    //  {
    //      var result = reservationService.GetReservation(50, "2e72f86c-b8fe-4265-b4f1-304dea8762db");
    //      Assert.IsNotNull(result);
    //      //Assert.AreEqual(3, result.Value.id);
    //  }
    //
    //  [Test]
    //  public void CreateReservation()
    //  {
    //      var reservation = reservationService.GetReservation(GuestRepoDouble.Guest1.Id, HostRepoDouble.Host1.Id).Value;
    //      
    //      reservation.guest.Id = GuestRepoDouble.Guest2.Id;
    //      var result = reservationService.CreateReservation(reservation);
    //      Assert.IsNotNull(result);
    //      Assert.AreEqual(reservation.endDate, result.Value.endDate);
    //  }
}