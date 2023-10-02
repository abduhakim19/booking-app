using API.Models;

namespace API.Contracts
{   // Interface AccountRoleRepository
    public interface IAccountRoleRepository
    {
        IEnumerable<AccountRole> GetAll(); // return Ienumerable
        AccountRole? GetByGuid(Guid guid); // return objek AccountRole boleh null
        AccountRole? Create(AccountRole accountRole); // return objek AccountRole boleh null
        bool Update(AccountRole accountRole); // return bool
        bool Delete(AccountRole accountRole); // return bool 
    }
}
