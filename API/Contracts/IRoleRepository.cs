using API.Models;

namespace API.Contracts
{   // Interface RoleRepository
    public interface IRoleRepository : IGeneralRepository<Role>
    {
        Role? GetByName(string name);
    }
}
