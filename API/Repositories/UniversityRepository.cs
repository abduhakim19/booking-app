using API.Contracts;
using API.Data;
using API.DTOs.Accounts;
using API.Models;
using System;

namespace API.Repositories
{   // class UniversityRepository inheritance interface IUniversityRepository
    public class UniversityRepository : GeneralRepository<University>, IUniversityRepository
    {
        public UniversityRepository(BookingManagementDbContext context) : base(context) { }

        public University? GetUniversityByCode(string code)
        {
            var entity = _context.Set<University>().Where(x => x.Code == code).FirstOrDefault();
            _context.ChangeTracker.Clear();

            return entity;
        }
    }
}
