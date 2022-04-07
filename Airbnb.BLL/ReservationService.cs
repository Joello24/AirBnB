using Airbnb.CORE.Models;
using Airbnb.CORE.Repositories;

namespace Airbnb.BLL;

public class ReservationService
{
    private readonly IReservationRepo _reservationRepository;
    private readonly IHostRepo _hostRepository;
    private readonly IGuestRepo _guestRepository;
    public ReservationService(IReservationRepo reservationRepository, IHostRepo hostRepository, IGuestRepo guestRepository)
    {
        _reservationRepository = reservationRepository;
        _hostRepository = hostRepository;
        _guestRepository = guestRepository;
    }

    public Result<Reservation> CreateReservation(Reservation reservation)
    {
        //reservation.host = PopulateHostReservations(reservation.host);
        Result<Reservation> result = Validate(reservation);
        if (!result.Success)
        {
            return result;
        }

        var hold = _reservationRepository.CreateReservation(reservation);
        result = hold;
        return result;
    }
    public Result<List<Host>> PopulateHostReservations(List<Host> hosts)
    {
        Result<List<Host>> result = new Result<List<Host>>();
        foreach (var host in hosts)
        {
            var hold = _reservationRepository.GetReservationsByHost(host.Id);
            if (hold.Success)
            {
                host.Reservations = hold.Value;
            }
        }
        result.Value = hosts;
        return result;
    }
    public Host PopulateHostReservations(Host host)
    {
        Result<Host> result = new Result<Host>();
        
        var hold = _reservationRepository.GetReservationsByHost(host.Id);
        if (hold.Success)
        {
            host.Reservations = hold.Value;
        }

        result.Value = host;
        return result.Value;
    }
    public Result<Reservation> DeleteReservation(Reservation res)
    {
        Result<Reservation> result = Validate(res);
        if (!result.Success)
        {
            return result;
        }
        // TODO: return populated reservation into hold
        var hold = _reservationRepository.DeleteReservation(res);
        if (hold.Success)
        {
            result.Value = hold.Value;
        }
        else
        {
            result.AddMessage("Error");
        }
        return result;
    }
    public Result<Reservation> UpdateReservation(Reservation reservation, Reservation oldReservation)
    {
        Result<Reservation> result = ValidateEdit(reservation, oldReservation);
        if (!result.Success)
        {
            return result;
        }
        // TODO: Consider moving BuildReservationData() call to controller, separation of concerns!
        var hold = _reservationRepository.UpdateReservation(reservation);
        if (hold.Success)
        {
            result.Value = BuildReservationData(hold.Value);
        }
        else
        {
            result.AddMessage("Error");
        }
        return result;
    }
    public Result<Reservation> GetReservation(int id, string hostId)
    {
        Result<Reservation> result = new Result<Reservation>();
        var res = _reservationRepository.GetReservation(id, hostId);
        if (!res.Success)
        {
            result = res;
            return result;
        }
        // TODO: Consider moving BuildReservationData() call to controller, separation of concerns!
        result.Value = BuildReservationData(res.Value);
        return result;
    }
    public Result<Reservation> GetReservation(int resId, int guestId, string hostId)
    {
        Result<Reservation> result = new Result<Reservation>();
        var res = _reservationRepository.GetReservation(resId,guestId, hostId);
        if (!res.Success)
        {
            result = res;
            return result;
        }
        // TODO: Consider moving BuildReservationData() call to controller, separation of concerns!
        result.Value = BuildReservationData(res.Value);
        return result;
    }
    public Result<List<Reservation>> GetReservations(string hostId)
    { 
        Dictionary<string, Host> hosts = _hostRepository.FindAll().Value.ToDictionary(h => h.Id);
        Dictionary<int, Guest> guests = _guestRepository.FindAll().Value.ToDictionary(g => g.Id);
        
        var res = _reservationRepository.GetReservationsByHost(hostId);
        if(res.Value == null)
        {
            return res;
        }
        
        foreach (var reservation in res.Value)
        {
            reservation.host = hosts[reservation.host.Id];
            reservation.guest = guests[reservation.guest.Id];
            reservation.totalPrice = CalculateTotalCost(reservation.host.weekdayRate, reservation.host.weekendRate, reservation.startDate, reservation.endDate);
        }
        return res;
    }
    public List<Reservation> BuildReservationData(List<Reservation> res)
    {
        Dictionary<string, Host> hosts = _hostRepository.FindAll().Value.ToDictionary(h => h.Id);
        Dictionary<int, Guest> guests = _guestRepository.FindAll().Value.ToDictionary(g => g.Id);
        
        List<Reservation> reservations = _reservationRepository.GetReservationsByHost(res[0].host.Id).Value;

        foreach (var reservation in reservations)
        {
            reservation.host = hosts[reservation.host.Id];
            reservation.guest = guests[reservation.guest.Id];
            reservation.totalPrice = CalculateTotalCost(reservation.host.weekdayRate,reservation.host.weekendRate, reservation.startDate, reservation.endDate);
        }
        return reservations;
    }
    public Reservation BuildReservationData(Reservation res)
    {
        Result<Reservation> result = new Result<Reservation>();
        if (!result.Success)
        {
            return res;
        }
        Dictionary<string, Host> hosts = _hostRepository.FindAll().Value.ToDictionary(h => h.Id);
        Dictionary<int, Guest> guests = _guestRepository.FindAll().Value.ToDictionary(g => g.Id);

        Result<Reservation> hold = ValidateNulls(res);
        if (!hold.Success)
        {
            return null;
        } 
        
        Reservation buildRes = _reservationRepository.GetReservation(res.id,res.guest.Id,res.host.Id).Value;
        buildRes.guest = guests[buildRes.guest.Id];
        buildRes.host = hosts[buildRes.host.Id];
        buildRes.totalPrice = CalculateTotalCost(buildRes.host.weekdayRate,buildRes.host.weekendRate, buildRes.startDate, buildRes.endDate);
        return buildRes;
    }

    public List<Reservation> GetAllReservations()
    {
        List<Host> hosts = _hostRepository.FindAll().Value;
        _reservationRepository.GetAllReservations(hosts);
        return _reservationRepository.GetAllReservations(hosts);
    }

    private decimal CalculateTotalCost(decimal weekdayRate, decimal weekendRate, DateOnly startDate, DateOnly endDate)
    {
        decimal totalCost = 0;
        DateTime start = DateTime.Parse(startDate.ToString());
        DateTime end = DateTime.Parse(endDate.ToString());
        int days = (end - start).Days;
        for (int i = 0; i < days; i++)
        {
            if (startDate.DayOfWeek == DayOfWeek.Saturday || startDate.DayOfWeek == DayOfWeek.Sunday)
            {
                totalCost += weekendRate;
            }
            else
            {
                totalCost += weekdayRate;
            }
            startDate = startDate.AddDays(1);
        }
        return totalCost;
    }
    public void CalculateTotalCost(Reservation reservation, Host host)
    {
        DateOnly startDate = reservation.startDate;
        DateOnly endDate = reservation.endDate;
        decimal weekdayRate = host.weekdayRate;
        decimal weekendRate = host.weekendRate;
        decimal totalCost = 0;
        DateTime start = DateTime.Parse(startDate.ToString());
        DateTime end = DateTime.Parse(endDate.ToString());
        int days = (end - start).Days;
        for (int i = 0; i < days; i++)
        {
            if (startDate.DayOfWeek == DayOfWeek.Saturday || startDate.DayOfWeek == DayOfWeek.Sunday)
            {
                totalCost += weekendRate;
            }
            else
            {
                totalCost += weekdayRate;
            }
            startDate = startDate.AddDays(1);
        }
        reservation.totalPrice = totalCost;
    }
    private Result<Reservation> Validate(Reservation reservation)
    {
        Result<Reservation> result = ValidateNulls(reservation);
        if (!result.Success)
        {
            return result;
        }
        //ValidateDuplicates(reservation, result);
        if (!result.Success)
        {
            return result;
        }
        ValidateFields(reservation, result);
        if (!result.Success)
        {
            return result;
        }
        ValidateChildrenExist(reservation, result);
        if (!result.Success)    
        {
            return result;      
        }

        return result;

    }
    private Result<Reservation> ValidateEdit(Reservation reservation, Reservation oldReservation)
    {
        Result<Reservation> result = ValidateNulls(reservation);
        if (!result.Success)
        {
            return result;
        }
        ValidateEditFields(reservation, result, oldReservation);
        if (!result.Success)
        {
            return result;
        }
        ValidateChildrenExist(reservation, result);
        if (!result.Success)    
        {
            return result;      
        }

        return result;
    }
    private void ValidateChildrenExist(Reservation reservation, Result<Reservation> result)
    {
        if (_guestRepository.FindAll().Value.FirstOrDefault(g => g.Id == reservation.guest.Id) == null)
        {
            result.AddMessage("Guest does not exist");
        }
        
        if (_hostRepository.FindAll().Value.FirstOrDefault(h => h.Id == reservation.host.Id) == null)
        {
            result.AddMessage("Host does not exist");
        }
    }
    private void ValidateFields(Reservation reservation, Result<Reservation> result)
    {
        if (reservation.startDate > reservation.endDate)
        {
            result.AddMessage("Start date must be before end date");
        }

        if (reservation.host.Reservations == null)
        {
            return;
        }
        else if(reservation.host.Reservations.Any(r => r.startDate <= reservation.startDate && r.endDate >= reservation.startDate))
        {
            result.AddMessage("Host is already booked for that time");
        }
        else if (reservation.host.Reservations.Any(r => r.startDate <= reservation.endDate && r.endDate >= reservation.endDate))
        {
            result.AddMessage("Host is already booked for that time");
        }
        else if (reservation.host.Reservations.Any(r => r.startDate >= reservation.startDate && r.endDate <= reservation.endDate))
        {
            result.AddMessage("Host is already booked for that time");
        }
    }
    private void ValidateEditFields(Reservation reservation, Result<Reservation> result, Reservation oldReservation)
    {
        if (reservation.startDate > reservation.endDate)
        {
            result.AddMessage("Start date must be before end date");
        }

        if (reservation.host.Reservations == null)
        {
            return;
        }
        // Check for overlap with other reservations excluding oldReservation dates
        // TODO: TEST THIS STUFF MORE
        if(reservation.host.Reservations.Any(r => r.startDate <= reservation.startDate && r.endDate >= reservation.startDate && r.id != reservation.id))
        {
            result.AddMessage("Host is already booked for that time");
        }
        else if (reservation.host.Reservations.Any(r => r.startDate <= reservation.endDate && r.endDate >= reservation.endDate && r.id != reservation.id))
        {
            result.AddMessage("Host is already booked for that time");
        }
        else if (reservation.host.Reservations.Any(r => r.startDate >= reservation.startDate && r.endDate <= reservation.endDate && r.id != reservation.id))
        {
            result.AddMessage("Host is already booked for that time");
        }
    }
    // private void ValidateDuplicates(Reservation reservation, Result<Reservation> result)
    // {
    //     if (_reservationRepository.GetReservation(reservation.guest.Id, reservation.host.Id).Value != null)
    //     {
    //         result.AddMessage("Reservation already exists");
    //     }
    // }
    private Result<Reservation> ValidateNulls(Reservation reservation)
    {
        var result = new Result<Reservation>();

        if(reservation == null)
        {
            result.AddMessage("Reservation doesn't exist.");
            return result;
        }

        if(reservation.guest == null)
        {
            result.AddMessage("Guest is required.");
        }

        if(reservation.host == null)
        {
            result.AddMessage("Host is required.");
        }

        return result;
    }
}