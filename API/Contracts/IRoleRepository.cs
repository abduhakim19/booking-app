using API.Models;

namespace API.Contracts
{   // Interface RoleRepository
    public interface IRoleRepository
    {
        IEnumerable<Role> GetAll(); // return Ienumerable
        Role? GetByGuid(Guid guid); // return objek Role boleh null
        Role? Create(Role role); // return objek Role boleh null
        bool Update(Role role); // return bool
        bool Delete(Role role); // return bool 
    }
}
