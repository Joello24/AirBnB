namespace Airbnb.Core.Enums;

public enum MainMenuOption
{
Exit,
ViewReservations,
MakeReservation,
EditReservation,
CancelReservation
}

public static class MenuOptionExtensions
{
    public static string ToLabel(this MainMenuOption option) => option switch
    {
        MainMenuOption.Exit => "Exit",
        MainMenuOption.ViewReservations => "View Forages By Date",
        MainMenuOption.MakeReservation => "View Items",
        MainMenuOption.EditReservation => "View Foragers",
        MainMenuOption.CancelReservation => "Add a Forage",
        _ => throw new NotImplementedException()
    };
}

