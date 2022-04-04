using Airbnb.Core;
using Airbnb.CORE.Models;
using Airbnb.CORE.Repositories;

namespace Airbnb.DAL;

public class HostFileRepository : IHostRepo
{
    private string  _filePath;
    private ILogger _logger;
    
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
        host.Email = columns[1];
        host.City = columns[2];
        host.Name = columns[3];
        if (columns[4].Contains("$"))
        {
            host.weekdayRate = decimal.Parse(columns[4].Replace("$", ""));
        }
        else
        {
            host.weekdayRate = decimal.Parse(columns[4]);
        }
        if(columns[5].Contains("$"))    
        {
            host.weekendRate = decimal.Parse(columns[5].Replace("$", ""));
        }
        else
        {
            host.weekendRate = decimal.Parse(columns[5]);
        }
        return host;
    }
    private string SerializeGuest(Host host)
    {
        return string.Format("{0},{1},{2},{3:C},{4:C}",
            host.Id,
            host.Name,
            host.Email,
            host.weekdayRate,
            host.weekendRate);
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