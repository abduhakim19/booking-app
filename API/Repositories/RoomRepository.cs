using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{   // class RoomRepository inheritance interface IRoomRepository
    // Isi Method sesuai interface
    public class RoomRepository : IGeneralRepository<Room>,IRoomRepository
    {   // properti DbContext
        private readonly BookingManagementDbContext _context;
        //inisialisasi dbcontext (Contsructor)
        public RoomRepository(BookingManagementDbContext context)
        {
            _context = context;
        }
        // Mendapatkan semua data Room dari database
        public IEnumerable<Room> GetAll()
        {
            return _context.Set<Room>().ToList();
        }
        // Mendapatkan data berdasarkan Guid Room di database
        public Room? GetByGuid(Guid guid)
        {
            return _context.Set<Room>().Find(guid);
        }

        public Room? Create(Room room)
        {
            try
            {   // menambhah data room ke database
                _context.Set<Room>().Add(room);
                _context.SaveChanges();
                return room; // return room jika berhasil
            }
            catch { return null; } // return null jika gagal
        }

        public bool Update(Room room)
        {
            try
            {   // mengupdate perubahan room ke database
                _context.Set<Room>().Update(room);
                _context.SaveChanges(); // menyimpan perubahan
                return true; // return true jike berhasil
            }
            catch { return false; } // return false jika gagal
        }

        public bool Delete(Room room)
        {
            try
            {   // menghapus data room ke database
                _context.Set<Room>().Remove(room);
                _context.SaveChanges();
                return true; // return true jike berhasil
            }
            catch { return false; } // return false jika gagal
        }

    }
}
