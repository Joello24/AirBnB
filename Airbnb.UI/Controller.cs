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
                case MainMenuOption.ViewGuests:
                    ViewAllGuests();
                    break;
                case MainMenuOption.ViewHosts:
                    ViewHosts();
                    break;
                default:
                    throw new Exception("Invalid option");
            }
        } while(option != MainMenuOption.Exit);
    }

    private Guest SelectGuest()
    {
        int choice = View.GetGuestSearchMethod();
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
                View.Display($"{host.Id.ToString()}: {host.Name} - {host.Email} - {host.City} - {host.weekdayRate:C} - {host.weekendRate:C}");                View.LineBreak();
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
                View.Display($"{guest.Id.ToString()}: {guest.Name} - {guest.Email}");
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
        throw new NotImplementedException();
    }
    private void EditReservation()
    {
        throw new NotImplementedException();
    }
    private void MakeReservation()
    {
        throw new NotImplementedException();
    }
    private void ViewReservations()
    {
        throw new NotImplementedException();
    }
}   