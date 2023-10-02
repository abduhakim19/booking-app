using API.Models;

namespace API.Contracts
{   // Interface BookingRepository
    public interface IBookingRepository
    {
        IEnumerable<Booking> GetAll(); // return Ienumerable
        Booking? GetByGuid(Guid guid); // return objek  Booking boleh null
        Booking? Create(Booking booking); // return objek  Booking boleh null
        bool Update(Booking booking); // return bool
        bool Delete(Booking booking); // return bool 
    }
}
