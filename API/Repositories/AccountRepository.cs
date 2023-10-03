using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{   // class AccountRepository inheritance interface IAccountRepository
    // Isi Method sesuai interface
    public class AccountRepository : GeneralRepository<Account>, IAccountRepository
    {
        public AccountRepository(BookingManagementDbContext context) : base(context) { }
    }
}
