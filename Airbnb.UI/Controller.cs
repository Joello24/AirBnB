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
                default:
                    throw new Exception("Invalid option");
            }
        } while(option != MainMenuOption.Exit);
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