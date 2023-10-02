using API.Models;

namespace API.Contracts
{   // Interface untuk AccountRepository
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAll(); // return Ienumerable
        Account? GetByGuid(Guid guid); // return objek Account boleh null
        Account? Create(Account account); // return objek Account boleh null
        bool Update(Account account); // return bool
        bool Delete(Account account); // return bool 
    }
}
