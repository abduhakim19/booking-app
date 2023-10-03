using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{   // class AccountRoleRepository inheritance interface IAccountRoleRepository
    public class AccountRoleRepository : GeneralRepository<AccountRole> ,IAccountRoleRepository
    {
        public AccountRoleRepository(BookingManagementDbContext context) : base(context) { }
    }
}
