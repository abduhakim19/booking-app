using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace API.Repositories
{
    // class RoleRepository inheritance interface IRoleRepository
    public class RoleRepository : GeneralRepository<Role> ,IRoleRepository
    { 
        public RoleRepository(BookingManagementDbContext context) : base(context) { }

        public Role? GetByName(string name)
        {   // cari nama role
            var entity = _context.Set<Role>().FirstOrDefault(r => r.Name == name);
            _context.ChangeTracker.Clear();

            return entity;
        }
    }
}
