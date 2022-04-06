using System;
using System.Collections.Generic;
using Airbnb.CORE.Models;
using Airbnb.CORE.Repositories;

namespace Airbnb.BLL.TEST.TestDoubles;

public class ResRepoDouble : IReservationRepo
{
    private readonly List<Reservation> _reservations = new List<Reservation>();

    public ResRepoDouble()
    {
        Reservation res1 = new Reservation();
        res1.id = 50;
        res1.startDate = new DateOnly(2019, 1, 1);
        res1.endDate = new DateOnly(2019, 1, 2);
        res1.guest = GuestRepoDouble.Guest1;
        res1.host = HostRepoDouble.Host1;
        _reservations.Add(res1);
        
        Reservation res2 = new Reservation();
        res2.id = 2;
        res2.startDate = new DateOnly(2019, 1, 1);
        res2.endDate = new DateOnly(2019, 1, 2);
        res2.guest = GuestRepoDouble.Guest2;
        res2.host = HostRepoDouble.Host2;
        _reservations.Add(res2);
        
        Reservation res3 = new Reservation();
        res3.id = 3;
        res3.startDate = new DateOnly(2019, 1, 1);
        res3.endDate = new DateOnly(2019, 1, 2);
        res3.guest = GuestRepoDouble.Guest3;
        res3.host = HostRepoDouble.Host3;
        _reservations.Add(res3);
    }

    public Result<Reservation> GetReservation(int id, string hostId)
    {
        var result = _reservations.Find(r => r.id == id && r.host.Id == hostId);
        
        return new Result<Reservation>()
        {
            Value = result
        };
        
    }

    public Result<List<Reservation>> GetReservationsByHost(string hostId)
    {
        Result<List<Reservation>> result = new Result<List<Reservation>>();
        result.Value = _reservations.FindAll(r => r.host.Id == hostId);
        if (result.Value == null)
        {
            result.AddMessage("Cant find");
        }
        return result;
    }

    public Result<Reservation> CreateReservation(Reservation reservation)
    {
        _reservations.Add(reservation);
        Result<Reservation> result = new Result<Reservation>();
        result.Value = reservation;
        return result;
    }
    public Result<Reservation> UpdateReservation(Reservation reservation)
    {
        throw new System.NotImplementedException();
    }
    public Result<Reservation> DeleteReservation(Reservation reservation)
    {
        throw new System.NotImplementedException();
    }
}