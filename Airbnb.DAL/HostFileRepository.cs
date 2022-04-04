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
        throw new NotImplementedException();
    }

    public Result<Host> FindByEmail(string email)
    {
        throw new NotImplementedException();
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