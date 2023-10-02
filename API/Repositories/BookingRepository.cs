using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{   // class BookingRepository inheritance interface IBookingRepository
    // Isi Method sesuai interface
    public class BookingRepository : IBookingRepository
    {   // properti DbContext
        private readonly BookingManagementDbContext _context;
        //inisialisasi dbcontext (Contsructor)
        public BookingRepository(BookingManagementDbContext context)
        {
            _context = context;
        }
        // Mendapatkan semua data Booking dari database
        public IEnumerable<Booking> GetAll()
        {
            return _context.Set<Booking>().ToList();
        }
        // Mendapatkan data berdasarkan Guid Booking dari database
        public Booking? GetByGuid(Guid guid)
        {
            return _context.Set<Booking>().Find(guid);
        }

        public Booking? Create(Booking booking)
        {
            try
            {   // menambhah data booking ke database
                _context.Set<Booking>().Add(booking);
                _context.SaveChanges();
                return booking; // return booking jika berhasil
            }
            catch
            {
                return null; // return null jika gagal
            }
        }

        public bool Update(Booking booking)
        {
            try
            {   // mengupdate perubahan booking ke database
                _context.Set<Booking>().Update(booking);
                _context.SaveChanges();
                return true; // return true jike berhasil
            }
            catch { return false; } // return false jika gagal
        }

        public bool Delete(Booking booking)
        {
            try
            {   // menghapus data booking di database
                _context.Set<Booking>().Remove(booking);
                _context.SaveChanges();
                return true; // return true jike berhasil
            }
            catch { return false; } // return false jika gagal
        }
    }
}
