using System;
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
}
public class ReservationTests
{
    const string testDir = @"../Data/reservations";
    private static ILogger _logger = new NullLogger();
    
    ReservationFileRepository repo = new ReservationFileRepository(_logger, testDir);
    
    [SetUp]
    public void Setup()
    {
        //File.Copy(seedDir + guestFile, testDir + guestFile, true);
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
}