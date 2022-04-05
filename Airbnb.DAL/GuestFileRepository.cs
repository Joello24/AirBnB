using System.Net;
using System.Security.Cryptography;
using Airbnb.Core;
using Airbnb.CORE.Models;
using Airbnb.CORE.Repositories;

namespace Airbnb.DAL;

public class GuestFileRepository : IGuestRepo
{
    private readonly string  _filePath;
    private ILogger _logger;
    private const string HEADER = "guest_id,first_name,last_name,email,phone,state";

    public GuestFileRepository(string filePath, ILogger logger)
    {
        _filePath = filePath;
        _logger = logger;
    }
    
    public Result<List<Guest>> FindAll()
    {
        var result = new Result<List<Guest>>();
        var guests = new List<Guest>();
        if (!File.Exists(_filePath))
        {
            result.AddMessage("File not found");
            return result;
        }

        string[] lines = null;
        try
        {
            lines = File.ReadAllLines(_filePath);
        }
        catch (IOException ex)
        {
            throw new Exception("Error reading file", ex);
        }

        foreach (var line in lines.Skip(1))
        {
            string[] columns = line.Split(',', StringSplitOptions.TrimEntries);
            Guest guest = DeserializeGuest(columns);
            if(guest != null)
            {
                guests.Add(guest);
            }
        }
        result.Value = guests;
        return result;
    }

    public Result<Guest> FindByEmail(string email)
    {
        Result<Guest> ret = new Result<Guest>();
        List<Guest> guests = new List<Guest>();
        try
        {
            guests = FindAll().Value;
        }
        catch (Exception ex)
        {
            _logger.Log($"\nError reading file in FindByEmail() GuestFileRepository, Line 64!\n {ex.Message}\n");
            return null;
        }
        ret.Value = guests.FirstOrDefault(g => g.Email == email);
        if(ret.Value == null)
        {
            ret.AddMessage("Guest not found");
        }
        return ret;
    }

    public Result<Guest> Create(Guest guest)
    {
        Result<Guest> ret = new Result<Guest>();
        List<Guest> guests = FindAll().Value;
        
        int id = (guests.Count == 0 ? 0 : guests.Max(i => i.Id)) + 1;
        guest.Id = id;
        guests.Add(guest);
        WriteToFile(guests);
        ret.Value = guest;
        return ret;
    }

    private void WriteToFile(List<Guest> guests)
    {
        try
        {
            using (StreamWriter file = new StreamWriter(_filePath))
            {
                if (guests== null)
                { 
                    return;
                }
                foreach (var guest in guests)
                {
                    file.WriteLine(SerializeGuest(guest));
                }
            }
        }
        catch (IOException ex)
        {
            throw new Exception("Error writing to file", ex);
        }
    }   

    private string SerializeGuest(Guest guest)
    {
        return $"{guest.Id},{guest.FirstName},{guest.LastName},{guest.Email},{guest.Phone},{guest.State}";
    }

    public Result<Guest> Update(Guest guest)
    {
        Result<Guest> ret = new Result<Guest>();
        List<Guest> guests = FindAll().Value;
        Guest guestToUpdate = guests.FirstOrDefault(g => g.Id == guest.Id);
        if (guestToUpdate == null)
        {
            ret.AddMessage("Guest not found");
            return ret;
        }   
        guestToUpdate.Name = guest.Name;
        guestToUpdate.LastName = guest.LastName;
        guestToUpdate.Email = guest.Email;
        WriteToFile(guests);
        ret.Value = guestToUpdate;
        return ret;
    }

    public Result<Guest> Delete(int id)
    {
        var ret = new Result<Guest>();
        var guests = FindAll();
        
        if (!guests.Success)
        {
            ret.AddMessage("Error reading file");
        }
        if(guests.Value.RemoveAll(g => g.Id == id) != 1)
        {
            ret.AddMessage($"Guest not found with id {id}");
        }
        else
        {
            WriteToFile(guests.Value);
        }
        return ret;
    }
    private Guest DeserializeGuest(string[] columns)
    {
        Guest guest = new Guest();
        if (columns.Length != 6)
        {
            _logger.Log("Invalid guest record");
            return null;
        }
        
        guest.Id = int.Parse(columns[0]);
        guest.FirstName = columns[1];
        guest.LastName = columns[2];
        guest.Email = columns[3];
        guest.Phone = columns[4];
        guest.State = columns[5];
        return guest;
    }
}