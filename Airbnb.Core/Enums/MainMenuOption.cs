namespace Airbnb.Core.Enums;

public enum MainMenuOption
{
Exit,
ViewReservations,
MakeReservation,
EditReservation,
CancelReservation,
ViewGuests,
ViewHosts,
Settings = 99


}

public static class MenuOptionExtensions
{
    public static string ToLabel(this MainMenuOption option) => option switch
    {
        MainMenuOption.Exit => "Exit",
        MainMenuOption.ViewReservations => "View Reservations",
        MainMenuOption.MakeReservation => "Make Reservation",
        MainMenuOption.EditReservation => "Edit Reservation",
        MainMenuOption.CancelReservation => "Cancel Reservation",
        MainMenuOption.Settings => "Settings",
        MainMenuOption.ViewGuests => "View Guests",
        MainMenuOption.ViewHosts => "View Hosts",
        _ => throw new NotImplementedException()
    };
}

