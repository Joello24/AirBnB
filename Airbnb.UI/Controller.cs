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

    private Guest SelectGuest()
    {
        int choice = View.GetSearchMethod("Guest");
        string NamePrefix = "";
        string email = "";
        Result<Guest> guest = new Result<Guest>();
        List<Guest> guests = new List<Guest>();
        switch (choice)
        {
            case 1:
                email = Validation.ReadRequiredString("Enter Email:");
                guest = _guestService.FindByEmail(email);
                return guest.Value;
                break;
                    
            case 2:
                NamePrefix = View.GetNamePrefix();
                guests = _guestService.FindByNameSearch(NamePrefix);
                if (guests.Count == 0)
                {
                    View.DisplayRed("No Guests found!");
                    return null;
                }
                else
                {
                    View.DisplayGuests(guests);
                    int index = (int)Validation.PromptUser4Num("Enter guest number:", 1, guests.Count);
                    return guests[index - 1];
                    // TODO: ***
                    // TODO: POSSIBLE BUG WITH INDEX, MIGHT NEED TO FIX ^^^
                    // TODO: ***
                }
            default:
                throw new Exception("Invalid option");
        }
    }

    private void FindGuestsByPrefixSearch()
    {
        List<Guest> guests = _guestService.FindByNameSearch(View.GetGuestPrefix());
    }

    private void ViewHosts()
    {
        Result<List<Host>> result = _hostService.FindAll();
        if (result.Success)
        {
            foreach (var host in result.Value)
            {
                View.Display($"{host}");
                View.LineBreak();
            }
        }
        else
        {
            View.DisplayRed(result.Messages.ToString());
        }   
    }

    private void UpdateGuest()
    {
        ViewAllGuests();
        int selection = (int)Validation.PromptUser4Num("Enter ID of guest to update: ");
        // get new info for guest.
        //_guestService.Update(guest.id, guest);
    }

    private void ViewAllGuests()
    {
        Result<List<Guest>> result = _guestService.GetAll();
        
        if (result.Success)
        {
            foreach (var guest in result.Value)
            {
                View.Display($"{guest}");
                View.LineBreak();
            }
        }
        else
        {
            View.DisplayRed(result.Messages.ToString());
        }
    }
    
    private void CancelReservation()
    {
        var newReservation = new Reservation();
        View.DisplayHeader("Edit Reservation");
        var guest = SelectGuest();
        if (guest == null)
        {
            View.DisplayRed("No Guest found!");
            return;
        }
        View.DisplayRed(guest.ToString());

        View.LineBreak();
        int choice = View.GetSearchMethod("Host");
        var host = SelectHost(choice);
        if (host == null)
        {
            View.DisplayRed("No Host found!");
            return;
        }
        host.ToString();
        guest.ToString();
        var reservation = _reservationService.GetReservation(guest.Id,host.Id);
        Result<Reservation> result = new Result<Reservation>();
        if (reservation == null)
        {
            View.DisplayRed("No Reservation found!");
            return;
        }
        result = _reservationService.DeleteReservation(reservation.Value);
        if(result.Success)
        {
            View.DisplayGreen($"Reservation Cancelled! \n {result.Value}");
        }
        else
        {
            foreach (var m in result.Messages)
            {
                View.DisplayRed(m);
            }
        }
    }
    private void EditReservation()
    {
        var newReservation = new Reservation();
        View.DisplayHeader("Edit Reservation");
        var guest = SelectGuest();
        if (guest == null)
        {
            return;
        }
        View.DisplayRed(guest.ToString());

        View.LineBreak();
        int choice = View.GetSearchMethod("Host");
        var host = SelectHost(choice);
        if (host == null)
        {
            return;
        }
        host.ToString();
        guest.ToString();
        var reservation = _reservationService.GetReservation(guest.Id,host.Id);
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
        if (result.Success)
        {
            View.DisplayGreen($"Reservation Updated!\n {result.Value}");
        }
        else
        {
            foreach (var m in result.Messages)
            {
                View.DisplayRed(m);
            }
        }
    }

    private void MakeReservation()
    {
        View.DisplayHeader("Make Reservation");
        var guest = SelectGuest();
        if (guest == null)
        {
            return;
        }
        View.DisplayRed(guest.ToString());

        View.LineBreak();
        int choice = View.GetSearchMethod("Host");
        var host = SelectHost(choice);
        if (host == null)
        {
            return;
        }
        View.DisplayRed(host.ToString());
        Reservation reservation = new Reservation();
        reservation.guest = guest;
        reservation.host = host;
        ViewReservations(host);
        reservation.startDate = View.GetDate("Enter Start Date:");
        reservation.endDate = View.GetDate("Enter End Date:"); 
        Result<Reservation> result = _reservationService.CreateReservation(reservation);
        if (result.Success)
        {
            View.Display($"{result.Value}");
            View.LineBreak();
        }
        else
        {
            foreach (var m in result.Messages)
            {
                View.DisplayRed(m);
            }
        }
    }
    
    private Host SelectHost(int Choice)
    {
        int choice = Choice;
        Result<Host> host = new Result<Host>();
        List<Host> hosts = new List<Host>();
        switch (choice)
        {
            case 1:
                host.Value = SearchByEmail();
                return host.Value;
                break;
                    
            case 2:
                host.Value = SearchByName();
                return host.Value;
            default:
                throw new Exception("Invalid option");
        }
    }

    private Host SearchByName()
    {
        string NamePrefix = "";
        List<Host> hosts = new List<Host>();
        Host host = new Host();
        NamePrefix = View.GetNamePrefix();
        hosts = _hostService.FindByNameSearch(NamePrefix);
        if (hosts.Count == 0)
        {
            View.DisplayRed("No Guests found!");
            return null;
        }
        
        hosts = _reservationService.PopulateHostReservations(hosts).Value;
        View.DisplayHosts(hosts);
        int index = (int)Validation.PromptUser4Num("Enter host number:", 1, hosts.Count);
        host = hosts[index - 1];
        return host;
        
    }

    private Host SearchByEmail()
    {
        string email = "";
        Result<Host> host = new Result<Host>();
        email = Validation.ReadRequiredString("Enter Email:");
        host = _hostService.FindByEmail(email);
        if (host.Success)
        {
            return host.Value;
        }
        else
        {
            foreach (var m in host.Messages)
            {
                View.DisplayRed(m);
            }
            return null;
        }
    }

    private void ViewReservations()
    {
        Result<List<Reservation>> result = new Result<List<Reservation>>();
        int choice = View.GetSearchMethod("Host");
        Host host = SelectHost(choice);
        if (host == null)
            return;
        
        result = _reservationService.GetReservations(host.Id);
        if (result.Success)
        {
            foreach (var reservation in result.Value)
            {
                View.Display($"{reservation}");
                View.LineBreak();
            }
        }
        else
        {
            foreach (var m in result.Messages)
            {
                View.DisplayRed(m);
            }
        }
    }

    // private Host GetHost()
    // {
    //     
    // }
    //
    // private Guest GetGuest()
    // {
    //     
    // }

    private void ViewReservations(Host host)
    {
        Result<List<Reservation>> result = new Result<List<Reservation>>();
        result = _reservationService.GetReservations(host.Id);
        if (result.Success)
        {
            foreach (var reservation in result.Value)
            {
                View.Display($"{reservation}");
                View.LineBreak();
            }
        }
        else
        {
            foreach (var m in result.Messages)
            {
                View.DisplayRed(m);
            }
        }
    }
}   