using System;
using System.Collections.Generic;
using System.IO;
using Airbnb.Core;
using Airbnb.CORE.Models;
using NUnit.Framework;

namespace Airbnb.DAL.TEST;

public class GuestTests
{
    const string guestFile = @"../Data/guests.csv";
    private static ILogger _logger = new NullLogger();
    
    GuestFileRepository guestFileRepository = new GuestFileRepository(guestFile, _logger);
    
    [SetUp]
    public void Setup()
    {
        //File.Copy(seedDir + guestFile, testDir + guestFile, true);
    }

    [Test]
    public void FindAllGuests_ReturnCountOfGuests()
    {
        //Arrange
        var expected = 1000;
        //Act
        var actual = guestFileRepository.FindAll().Value;
        //Assert
        Assert.AreEqual(expected, actual.Count);
    }

    [Test]
    public void FindGuestByEmail()
    {
        //Arrange
        Guest expected = new Guest()
        {
            Id = 1,
            FirstName = "Sullivan",
            LastName = "Lomas",
            Email = "slomas0@mediafire.com",
            Phone = "(702) 7768761",
            State = "NV",
        };
        //Act
        var actual = guestFileRepository.FindByEmail("slomas0@mediafire.com");
        //Assert
        Assert.AreEqual(expected.ToString(), actual.Value.ToString());
    }
    }
public class HostTests
{
    const string hostFile = @"../Data/hosts.csv";
    private static ILogger _logger = new NullLogger();
    
    HostFileRepository repo = new HostFileRepository(hostFile, _logger);
    
    [SetUp]
    public void Setup()
    {
        //File.Copy(seedDir + hostFile, testDir + guestFile, true);
    }
    
    [Test]
    public void FindAllHosts()
    {
        //Arrange
        var expected = 1000;
        //Act
        var actual = repo.FindAll().Value;
        //Assert
        Assert.AreEqual(expected, actual.Count);
    }
    
    [Test]
    public void FindHostByEmail()
    {
        //Arrange
        Host expected = new Host()
        {
            Id = "b6ddb844-b990-471a-8c0a-519d0777eb9b",
            LastName = "Harley",
            Email = "charley4@apple.com",
            Phone = "(954) 7895760",
            Address = "1 Maple Wood Terrace",
            City = "Orlando",
            State = "FL",
            PostalCode = 32825,
            weekdayRate = 176,
            weekendRate = 220
        };
        //Act
        var actual = repo.FindByEmail("charley4@apple.com");
        //Assert
        Assert.AreEqual(expected.ToString(), actual.Value.ToString());
    }
}
public class ReservationTests
{
    //const string testDir = @"../Data/reservations";
    const string testDir = @"../../../../Airbnb.DAL.TEST/bin/Debug/data/reservations";
    const string seedDir = @"../../../../Airbnb.DAL.TEST/bin/Debug/SEED_Data/reservations";
    const string testfile1 = @"/293508da-e367-437a-9178-1ebaa7d83015.csv";
    private static ILogger _logger = new NullLogger();
    
    ReservationFileRepository repo = new ReservationFileRepository(_logger, testDir);
    
    [SetUp]
    public void Setup()
    {
        File.Copy(seedDir + testfile1, testDir + testfile1, true);
    }
    
    [Test]
    public void GetReservation()
    {
        //Arrange
        Reservation expected = new Reservation();
        Guest guest = new Guest();
        guest.Id = 899;
        Host host = new Host();
        host.Id = "582de161-b2eb-4ea6-8f28-b2e4be5f5ca0";
        expected.guest = guest;
        expected.host = host;
        //Act
        var actual = repo.GetReservation(expected.guest.Id, expected.host.Id).Value;
        //Assert
        Assert.AreEqual(expected.guest.Id, actual.guest.Id);
    }
    
    //GetReservationsByHost
    [Test]
    public void GetReservationsByHost()
    {
        /*
         * id,start_date,end_date,guest_id,total
            1,2022-02-11,2022-02-13,845,1022.50
            2,2021-11-18,2021-11-22,89,1840.50
            3,2022-01-08,2022-01-13,839,2147.25
            4,2021-03-27,2021-03-30,72,1329.25
            5,2021-05-30,2021-06-04,990,2045
            6,2021-05-12,2021-05-16,637,1840.50
            7,2022-02-16,2022-02-21,390,2249.50
            8,2021-08-15,2021-08-17,231,818
            9,2021-12-08,2021-12-12,436,1840.50
            10,2021-04-02,2021-04-03,246,511.25
            11,2021-05-17,2021-05-21,88,1636
            12,2021-11-05,2021-11-09,843,1840.50
            13,2021-12-16,2021-12-20,208,1840.50
         */
        //Arrange
        var host = new Host()
        {
            Id = "293508da-e367-437a-9178-1ebaa7d83015"
        };
        var expected = new List<Reservation>();
        expected.Add(new Reservation()
        {
            id = 1,
            startDate = new DateOnly(2022, 2, 11),
            endDate = new DateOnly(2022, 2, 13),
            guest = new Guest() { Id = 845 },
            totalPrice = 1022.50m,
            host = host
        });
        expected.Add(new Reservation()
        {
            id = 2,
            startDate = new DateOnly(2021, 11, 18),
            endDate = new DateOnly(2021, 11, 22),
            guest = new Guest() { Id = 89 },
            totalPrice = 1840.50m,
            host = host
        });
        
        //Act
        var actual = repo.GetReservationsByHost(host.Id).Value;
        
        //Assert
        Assert.AreEqual(13, actual.Count);
        Assert.AreEqual(expected[0].id, actual[0].id);
        Assert.AreEqual(expected[1].endDate, actual[1].endDate);
        Assert.AreEqual(expected[1].host.Id, actual[1].host.Id);
    }

    //CreateReservation
    [Test]
    public void CreateReservation()
    {
        //Arrange
        Host host = new Host()
        {
            Id = "293508da-e367-437a-9178-1ebaa7d83015"
        };
        Reservation expected = new Reservation();
        expected.host = host;
        expected.id = 14;
        expected.guest = new Guest() { Id = 859 };
        
        //Act
        var actual = repo.CreateReservation(expected).Value;
        var actual2 = repo.GetReservation(expected.guest.Id, expected.host.Id).Value;
        
        //Assert
        Assert.AreEqual(expected.id, actual.id);
        Assert.AreEqual(expected.id, actual2.id);
    }
    //UpdateReservation
    [Test]
    public void UpdateReservation()
    {
        //Arrange
        Reservation expected = new Reservation();
        Guest guest = new Guest();
        guest.Id = 899;
        Host host = new Host();
        host.Id = "582de161-b2eb-4ea6-8f28-b2e4be5f5ca0";
        expected.guest = guest;
        expected.host = host;
        
        //Act
        repo.CreateReservation(expected);
        expected.totalPrice = 1022.50m;
        var actual = repo.UpdateReservation(expected).Value;
        
        //Assert
        Assert.AreEqual(expected.totalPrice, actual.totalPrice);
        Assert.AreEqual(1022.50m, actual.totalPrice);
    }
}