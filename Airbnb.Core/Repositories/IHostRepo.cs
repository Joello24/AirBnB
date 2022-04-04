using Airbnb.CORE.Models;

namespace Airbnb.CORE.Repositories;

public interface IHostRepo
{
    Result<List<Host>> FindAll();
    Result<Host> FindByEmail(string email);
    Result<Host> Create(Host host);
    Result<Host> Update(Host host);
    Result<Host> Delete(Host host);
}