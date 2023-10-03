using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{   // class BookingRepository inheritance interface IBookingRepository
    public class BookingRepository : GeneralRepository<Booking>, IBookingRepository
    {
        public BookingRepository(BookingManagementDbContext context) : base(context) { }
    }
}
