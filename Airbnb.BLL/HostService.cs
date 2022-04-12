using Airbnb.CORE.Models;
using Airbnb.CORE.Repositories;

namespace Airbnb.BLL;

public class HostService
{
    
    private readonly IHostRepo _hostRepo;
    public HostService(IHostRepo hostRepo)
    {
        _hostRepo = hostRepo;
    }
    public Result<List<Host>> FindAll()
    {
        var result = _hostRepo.FindAll();
        return result;
    }
    public Result<Host> FindByEmail(string email)
    {
        Result<Host> result = new Result<Host>();
        result.Value = _hostRepo.FindAll().Value.FirstOrDefault(x => x.Email == email);
        if(result.Value == null)
        {
            result.AddMessage("Host not found");
        }
        return result;
    }
    public List<Host> FindByNameSearch(string namePrefix)
    {
        return _hostRepo.FindAll().Value.Where(x => x.LastName.StartsWith(namePrefix)).ToList();
    }
}