using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    // class RoleRepository inheritance interface IRoleRepository
    // Isi Method sesuai interface
    public class RoleRepository : IRoleRepository
    {
        // properti DbContext
        private readonly BookingManagementDbContext _context;
        //inisialisasi dbcontext (Contsructor)
        public RoleRepository(BookingManagementDbContext context)
        {
            _context = context;
        }
        // Mendapatkan semua data Role dari database
        public IEnumerable<Role> GetAll()
        {
            return _context.Set<Role>().ToList();
        }
        // Mendapatkan data berdasarkan Guid Role dari database
        public Role? GetByGuid(Guid guid)
        {
            return _context.Set<Role>().Find(guid);
        }

        public Role? Create(Role role)
        {
           try
            {   // menambhah data role ke database
                _context.Set<Role>().Add(role);
                _context.SaveChanges();
                return role; // return role jika berhasil
            } 
            catch { return null; } // return null jika gagal
        }

        public bool Update(Role role)
        {
            try
            {   // mengupdate perubahan role ke database
                _context.Set<Role>().Update(role);
                _context.SaveChanges();
                return true; // return true jike berhasil
            }
            catch { return false; } // return false jika gagal
        }

        public bool Delete(Role role)
        {
            try
            {
                // menghapus data role di database
                _context.Set<Role>().Remove(role);
                _context.SaveChanges();
                return true; // return true jike berhasil
            }
            catch { return false; } // return false jika gagal
        }
        
    }
}
