using Airbnb.Core;
using Airbnb.CORE.Models;
using Airbnb.CORE.Repositories;

namespace Airbnb.DAL;

public class HostFileRepository : IHostRepo
{
    private string  _filePath;
    private ILogger _logger;
    private const string HEADER = "id,last_name,email,phone,address,city,state,postal_code,standard_rate,weekend_rate";
    
    public HostFileRepository(string filePath, ILogger logger)
    {
        _filePath = filePath;
        _logger = logger;
    }
    
    public Result<List<Host>> FindAll()
    {
        var result = new Result<List<Host>>();
        var hosts = new List<Host>();
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
            Host host = DeserializeHost(columns);
            if(host != null)
            {
                hosts.Add(host);
            }
        }
        result.Value = hosts;
        return result;
    }
    private Host DeserializeHost(string[] columns)
    {
        Host host = new Host();
        host.Id = columns[0];
        host.LastName = columns[1];
        host.Email = columns[2];
        host.Phone = columns[3];
        host.Address = columns[4];
        host.City = columns[5];
        host.State = columns[6];
        host.PostalCode = int.Parse(columns[7]);
        host.weekdayRate = decimal.Parse(columns[8].Contains("$") ? columns[8].Replace("$", "") : columns[8]);
        host.weekendRate = decimal.Parse(columns[9].Contains("$") ? columns[9].Replace("$", "") : columns[9]);
        return host;
    }
    private string SerializeGuest(Host host)
    {   
        return
            $"{host.Id},{host.LastName},{host.Email},{host.Phone},{host.Address},{host.City},{host.State},{host.PostalCode},{host.weekdayRate},{host.weekendRate}";
    }
    public Result<Host> FindByEmail(string email)
    {
        var result = new Result<Host>();
        var hosts = new List<Host>();
        if (!File.Exists(_filePath))
        {
            result.AddMessage("File not found");
            return result;
        }
        try
        {
            hosts = FindAll().Value;
        }
        catch (IOException ex)
        {
            throw new Exception("Error reading file", ex);
        }

        result.Value = hosts.FirstOrDefault(e => e.Email == email);
        if (result.Value == null)
        {
            result.AddMessage("Host not found");
        }
        
        return result;
    }
    private void WriteToFile(List<Host> hosts)
    {
        try
        {
            using (StreamWriter file = new StreamWriter(_filePath))
            {
                if (hosts== null)
                { 
                    return;
                }
                
                file.WriteLine(HEADER);
                foreach (var host in hosts)
                {
                    file.WriteLine(SerializeGuest(host));
                }
            }
        }
        catch (IOException ex)
        {
            throw new Exception("Error writing to file", ex);
        }
    }
    
    public Result<Host> Create(Host host)
    {
        throw new NotImplementedException();
    }
    public Result<Host> Update(Host host)
    {
        throw new NotImplementedException();
    }
    public Result<Host> Delete(Host host)
    {
        throw new NotImplementedException();
    }
}