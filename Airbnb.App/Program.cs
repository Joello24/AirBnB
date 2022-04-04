namespace Airbnb.App;

public static class Program
{
    public static void Main()
    {
        Startup.Run();
    }
}



/*
 * AUTHOR: ROBERT LILLYBLAD
 * DEV10 - C# - AIRBNB - MASTERY ASSESSMENT
 *
 * 
 * TODO: HIGH LEVEL OVERVIEW
 *
   ENTRY: The administrator may view existing reservations for a host.
   ENTRY: The administrator may create a reservation for a guest with a host.
   ENTRY: The administrator may edit existing reservations.
   ENTRY: The administrator may cancel a future reservation.
 *
 * LIST: MODELS/DTO's
 * 
 * ENTRY: GUEST
 *       A customer. Someone who wants to book a place to stay. Guest data is provided via a zip download.
 * ENTRY: HOST
 *      The accommodation provider. Someone who has a property to rent per night. Host data is provided.
 * ENTRY: PROPERTY
 *      A rental property. In Don't Wreck My House, Location and Host are combined.The application enforces
 *             a limit on one Location per Host, so we can think of a Host and Location as a single thing.
 * ENTRY: RESERVATION
 *     One or more days where a Guest has exclusive access to a Location (or Host). Reservation data is provided.
 *
 * LIST: REQUIREMENTS
 *
 * ENTRY View Reservations for Host
Display all reservations for a host.

The user may enter a value that uniquely identifies a host or they can search for a host and pick one out of a list.
If the host is not found, display a message.
If the host has no reservations, display a message.
Show all reservations for that host.
Show useful information for each reservation: the guest, dates, totals, etc.
Sort reservations in a meaningful way.

 * ENTRY Make a Reservation
Books accommodations for a guest at a host.

The user may enter a value that uniquely identifies a guest or they can search for a guest and pick one out of a list.
The user may enter a value that uniquely identifies a host or they can search for a host and pick one out of a list.
Show all future reservations for that host so the administrator can choose available dates.
Enter a start and end date for the reservation.
Calculate the total, display a summary, and ask the user to confirm. The reservation total is based on the host's standard rate and weekend rate. For each day in the reservation, determine if it is a weekday or a weekend. If it's a weekday, the standard rate applies. If it's a weekend, the weekend rate applies.
On confirmation, save the reservation.
Validation

Guest, host, and start and end dates are required.
The guest and host must already exist in the "database". Guests and hosts cannot be created.
The start date must come before the end date.
The reservation may never overlap existing reservation dates.
The start date must be in the future.

 * ENTRY Edit a Reservation
Edits an existing reservation.

Find a reservation.
Start and end date can be edited. No other data can be edited.
Recalculate the total, display a summary, and ask the user to confirm.
Validation

Guest, host, and start and end dates are required.
The guest and host must already exist in the "database". Guests and hosts cannot be created.
The start date must come before the end date.
The reservation may never overlap existing reservation dates.

 * ENTRY Cancel a future reservation.

Find a reservation.
Only future reservations are shown.
On success, display a message.
Validation

You cannot cancel a reservation that's in the past.

 * LIST: TECHNICAL REQUIREMENTS
 * 
 * ENTRY: Must be a console project.
   ENTRY: Dependency injection configured.
   ENTRY: All financial math must use decimal.
   ENTRY: Dates must be DateTime, never strings.
   ENTRY: All file data must be represented in models in the application.
   ENTRY: Reservation identifiers are unique per host, not unique across the entire application. 
          Effectively, the combination of a reservation identifier and a host identifier is required to uniquely identify a reservation.
          
 * LIST: File Format
 * 
The data file format is bad, but it's required. You may not change the file formats or the file delimiters. You must use the files provided. Guests are stored in their own comma-delimited file, guests.csv, with a header row. Hosts are stored in their own comma-delimited file, hosts.csv, with a header row.

Reservations are stored across many files, one for each host. A host reservation file name has the format: {host-identifier}.csv.

 * ENTRY: Example 
Reservations for host c6567347-6c57-4658-a2c7-50040eeeb80f are stored in c6567347-6c57-4658-a2c7-50040eeeb80f.csv
 *
 * LIST: Testing
All data components must be thoroughly tested. Tests must always run successfully regardless of the data starting point, so it's important to establish known good state. Never test with "production" data.

All domain components must be thoroughly tested using test doubles. Do not use file repositories for domain testing.

User interface testing is not required.

 * LIST: Deliverables
   ENTRY: A schedule of concrete tasks. Tasks should contain time estimates and should never be more than 4 hours.
   ENTRY: Class diagram (informal is okay)
   ENTRY: Sequence diagrams or flow charts (optional, but encouraged)
   ENTRY: A C# solution with a console application and supporting layered projects that contains everything needed to run without error
   ENTRY: Test suite
 *
 * TODO: ***PLAN***
 *
 * LIST: Class Diagram/Sequence Diagram:
 * 
 * LIST:     Entry Point + Setup (.App) -> CONTROLLER(.UI) -> SERVICES(.BLL) -> DATA(.DAL)
 * ENTRY:       Controller -> GuestService -> GuestRepository
 * ENTRY:       Controller -> HostService -> HostRepository
 * ENTRY:       Controller -> ReservationService -> ReservationRepository
 *
 * 
 */