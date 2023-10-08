using API.Models;

namespace API.Contracts
{   // Interface BookingRepository
    public interface IBookingRepository : IGeneralRepository<Booking>
    {
        IEnumerable<Booking>? GetBookingByBeetweenStartAndEndDate(DateTime date);

    }
}
