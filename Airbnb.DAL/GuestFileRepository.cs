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
    private List<Guest> _guests;
    
    public GuestFileRepository(string filePath, ILogger logger)
    {
        _filePath = filePath;
        _logger = logger;
    }
    
    public Result<List<Guest>> FindAll()
    {
        var result = new Result<List<Guest>>();
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

        foreach (var line in lines)
        {
            string[] columns = line.Split(',', StringSplitOptions.TrimEntries);
            Guest guest = DeserializeGuest(columns);
            if(guest != null)
            {
                _guests.Add(guest);
            }
        }
        result.Value = _guests;
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
            _logger.Log("Error reading file");
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
        throw new NotImplementedException();
    }

    public Result<Guest> Update(Guest guest)
    {
        throw new NotImplementedException();
    }

    public Result<Guest> Delete(string id)
    {
        throw new NotImplementedException();
    }
    private Guest DeserializeGuest(string[] columns)
    {
        // id,first,last, email
        Guest guest = new Guest();
        if (columns.Length != 4)
        {
            _logger.Log("Invalid guest record");
            return null;
        }
        guest.Id = columns[0];
        guest.FirstName = columns[1];
        guest.LastName = columns[2];
        guest.Email = columns[3];
        return guest;
    }
}