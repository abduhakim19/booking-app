﻿using API.Models;

namespace API.Contracts
{   // Interface AccountRoleRepository
    public interface IAccountRoleRepository : IGeneralRepository<AccountRole>
    {
        IEnumerable<Guid> GetRoleGuidByAccountGuid(Guid accountGuid);
    }
}
