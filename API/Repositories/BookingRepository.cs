using API.Contracts;
using API.Data;
using API.Models;
using API.Utilites.Enums;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{   // class BookingRepository inheritance interface IBookingRepository
    public class BookingRepository : GeneralRepository<Booking>, IBookingRepository
    {
        public BookingRepository(BookingManagementDbContext context) : base(context) { }

        public IEnumerable<Booking>? GetBookingByBeetweenStartAndEndDate(DateTime date)
        {   // berdasarkan tanggan diantar enddate dan startdate
            var result = _context.Set<Booking>()
                .Where(b => b.EndDate >= date && b.StartDate <= date)
                .ToList();

            return result;
        }
    }
}
