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

    [Test]
     public void GetReservation()
     {
         var result = reservationService.GetReservation(0,50, "2e72f86c-b8fe-4265-b4f1-304dea8762db");
         Assert.IsNotNull(result);
         Assert.AreEqual(200m, result.Value.host.weekdayRate);
     }
     
     [Test]
     public void CreateReservation()
     {
         var reservation = reservationService.GetReservation(0,50, "2e72f86c-b8fe-4265-b4f1-304dea8762db");
         
         reservation.Value.guest = GuestRepoDouble.Guest2;
         reservation.Value.host = HostRepoDouble.Host2;
         var result = reservationService.CreateReservation(reservation.Value);
         Assert.IsNotNull(result);
         Assert.AreEqual(reservation.Value.endDate, result.Value.endDate);
         Assert.IsTrue(result.Success);
     }

     [Test]
     public void UpdateReservation()
     {
         var reservation = reservationService.GetReservation(0,50, "2e72f86c-b8fe-4265-b4f1-304dea8762db");
         var oldRes = reservation.Value;
         reservation.Value.endDate = new DateOnly(2020,1,1);
         var actual = reservationService.UpdateReservation(reservation.Value, oldRes);
         Assert.IsNotNull(actual);
         Assert.AreEqual(new DateOnly(2020,1,1), actual.Value.endDate);
         Assert.IsTrue(actual.Success);
     }
     
     [Test]
     public void DeleteReservation()
     {
         var reservation = reservationService.GetReservation(0,50, "2e72f86c-b8fe-4265-b4f1-304dea8762db");
         reservation.Value.id = 99;
         
         
         var actual = reservationService.DeleteReservation(reservation.Value);
         Assert.IsNotNull(actual);
         Assert.AreEqual(reservationService.GetReservation(99,50, "2e72f86c-b8fe-4265-b4f1-304dea8762db").Value, null);
         Assert.IsTrue(actual.Success);
     }
}