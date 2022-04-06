using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Airbnb.BLL;
using Airbnb.Core.Enums;
using Airbnb.CORE.Models;

namespace Airbnb.UI;

public class Controller
{
    private readonly GuestService _guestService;
    private readonly HostService _hostService;
    private readonly ReservationService _reservationService;
    
    public Controller(GuestService guestService, HostService hostService, ReservationService reservationService)
    {
        _guestService = guestService;
        _hostService = hostService;
        _reservationService = reservationService;
    }

    public void Run()
    {
        View.DisplayHeader("Welcome to Air-BnB!");
        RunAppLoop();
        
        // catch(RepositoryException ex)
        // {
        //     view.DisplayException(ex);
        // }
        // view.DisplayHeader("Goodbye.");
    }

    private void RunAppLoop()
    {
        MainMenuOption option;
        do
        {
            option = (MainMenuOption)View.GetMainChoice();
            switch(option)
            {
                case MainMenuOption.ViewReservations:
                    ViewReservations();
                    break;
                case MainMenuOption.MakeReservation:
                    MakeReservation();
                    break;
                case MainMenuOption.EditReservation:
                    EditReservation();
                    break;
                case MainMenuOption.CancelReservation:
                    CancelReservation();
                    break;
                default:
                    throw new Exception("Invalid option");
            }
        } while(option != MainMenuOption.Exit);
    }

    private Result<Guest> SelectGuest()
    {
        int choice = View.GetSearchMethod("Guest");
        string NamePrefix = "";
        string email = "";
        Result<Guest> guest = new Result<Guest>();
        List<Guest> guests = new List<Guest>();
        switch (choice)
        {
            case 1:
                guest = SearchGuestByEmail();
                return guest;
                break;
                    
            case 2:
                guest = SearchGuestByName();
                return guest;
            default:
                throw new Exception("Invalid option");
        }
    }
    
    private void CancelReservation()
    {
        var newReservation = new Reservation();
        View.DisplayHeader("Edit Reservation");
        var guest = GrabGuest();
        var host = GrabHost();
        
        var reservation = _reservationService.GetReservation(guest.Id,host.Id);
        Result<Reservation> result = new Result<Reservation>();
        if (reservation == null)
        {
            View.DisplayRed("No Reservation found!");
            return;
        }
        result = _reservationService.DeleteReservation(reservation.Value);
        View.HandleSingleResult(result);
    }

    private Host GrabHost()
    {
        bool grabbing = true;
        Result<Host> host = new Result<Host>();
        while (grabbing)
        {
            host = SelectHost();
            if (!host.Success)
                View.DisplayResultMessages(host.Messages);
            
            else
                grabbing = false;
        }
    
        View.DisplayGreen(host.Value.ToString());
        View.LineBreak();
        return host.Value;
    }
    private Guest GrabGuest()
    {
        bool grabbing = true;
        Result<Guest> guest = new Result<Guest>();
        while (grabbing)
        {
            guest = SelectGuest();
            if (!guest.Success)
                View.DisplayResultMessages(guest.Messages);
            
            else
                grabbing = false;
        }
    
        View.DisplayGreen(guest.Value.ToString());
        View.LineBreak();
        return guest.Value;
    }

    private void EditReservation()
    {
        var newReservation = new Reservation();
        View.DisplayHeader("Edit Reservation");
        var guest = SelectGuest();
        if (!guest.Success)
        {
            View.DisplayResultMessages(guest.Messages);
            return;
        }
        
        View.DisplayGreen(guest.ToString());

        View.LineBreak();
        
        var host = SelectHost();
        if (host == null)
        {
            return;
        }
        host.Value.ToString();
        guest.Value.ToString();
        var reservation = _reservationService.GetReservation(guest.Value.Id,host.Value.Id);
        if (reservation.Success)
        {
            newReservation.startDate = View.EditReservationDate(reservation.Value.startDate, "Start");
            newReservation.endDate = View.EditReservationDate(reservation.Value.endDate, "End");
            newReservation.id = reservation.Value.id;
            newReservation.guest = reservation.Value.guest;
            newReservation.host = reservation.Value.host;
            AddUpdatedReservation(newReservation);
        }
        else
        {
            foreach (var m in reservation.Messages)
            {
                View.DisplayRed(m);
            }
        }
    }

    private void AddUpdatedReservation(Reservation newReservation)
    {
        Result<Reservation> result = new Result<Reservation>();
        result = _reservationService.UpdateReservation(newReservation);
        View.HandleSingleResult(result, "Update Successful!");
    }

    private void MakeReservation()
    {
        View.DisplayHeader("Make Reservation");
        var guest = SelectGuest();
        if (!guest.Success)
        {
            View.DisplayResultMessages(guest.Messages);
            return;
        }
        View.Display("Guest found!");
        View.DisplayGreen(guest.Value.ToString());
        
        View.LineBreak();
        
        var host = SelectHost();
        if (!host.Success)
        {
            return;
        }
        View.DisplayGreen(host.Value.ToString());
        Reservation reservation = new Reservation();
        reservation.guest = guest.Value;
        reservation.host = host.Value;
        
        ViewReservations(host.Value);
        reservation.startDate = View.GetDate("Enter Start Date:");
        reservation.endDate = View.GetDate("Enter End Date:"); 
        
        Result<Reservation> result = _reservationService.CreateReservation(reservation);
        //View.HandleSingleResult(result, "Reservation Created!");
        result.Value.Print();
    }

    private Result<Host> SelectHost()
    {
        int choice = View.GetSearchMethod("Host");
        Result<Host> host = new Result<Host>();
        List<Host> hosts = new List<Host>();
        switch (choice)
        {
            case 1:
                host = SearchHostByEmail();
                return host;
                break;
                    
            case 2:
                host = SearchHostByName();
                return host;
            default:
                throw new Exception("Invalid option");
        }
    }

    private Result<Host> SearchHostByName()
    {
        string NamePrefix = "";
        List<Host> hosts = new List<Host>();
        Result<Host> host = new Result<Host>();
        NamePrefix = View.GetNamePrefix();
        hosts = _hostService.FindByNameSearch(NamePrefix);
        if (hosts.Count == 0)
        {
            host.AddMessage("No hosts found!");
            return host;
        }
        hosts = _reservationService.PopulateHostReservations(hosts).Value;
        View.DisplayHosts(hosts);
        int index = (int)Validation.PromptUser4Num("Enter host number:", 1, hosts.Count);
        host.Value = hosts[index - 1];
        return host;
    }
    private Result<Guest> SearchGuestByName()
    {
        string NamePrefix = "";
        List<Guest> guests = new List<Guest>();
        Result<Guest> guest = new Result<Guest>();
        NamePrefix = View.GetNamePrefix();
        guests = _guestService.FindByNameSearch(NamePrefix);
        if (guests.Count == 0)
        {
            guest.AddMessage("No guests found!");
            return guest;
        }
        View.DisplayGuests(guests);
        int index = (int)Validation.PromptUser4Num("Enter guest number:", 1, guests.Count);
        guest.Value = guests[index - 1];
        return guest;
    }

    private Result<Host> SearchHostByEmail()
    {
        string email = "";
        Result<Host> host = new Result<Host>();
        email = Validation.ReadRequiredString("Enter Email:");
        host = _hostService.FindByEmail(email);
        return host;
    }
    private Result<Guest> SearchGuestByEmail()
    {
        string email = "";
        Result<Guest> guest = new Result<Guest>();
        email = Validation.ReadRequiredString("Enter Email:");
        guest = _guestService.FindByEmail(email);
        return guest;
    }

    private void ViewReservations()
    {
        Result<List<Reservation>> result = new Result<List<Reservation>>();
        Result<Host> host = SelectHost();
        
        if (!host.Success)
        {
            View.DisplayResultMessages(host.Messages);
            return;
        }

        result = _reservationService.GetReservations(host.Value.Id);
        View.HandleListResult(result);
    }

    private void ViewReservations(Host host)
    {
        Result<List<Reservation>> result = new Result<List<Reservation>>();
        result = _reservationService.GetReservations(host.Id);
        View.HandleListResult(result);
    }
}   