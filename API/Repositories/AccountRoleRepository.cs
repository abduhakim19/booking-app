using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{   // class AccountRoleRepository inheritance interface IAccountRoleRepository
    // Isi Method sesuai interface
    public class AccountRoleRepository : IGeneralRepository<AccountRole> ,IAccountRoleRepository
    {   // properti DbContext
        private readonly BookingManagementDbContext _context;
        //inisialisasi dbcontext (Contsructor)
        public AccountRoleRepository(BookingManagementDbContext context)
        {
            _context = context;
        }
        // Mendapatkan semua data AccountRole dari database
        public IEnumerable<AccountRole> GetAll()
        {
            return _context.Set<AccountRole>().ToList();
        }
        // Mendapatkan data berdasarkan Guid AccountRole dari database
        public AccountRole? GetByGuid(Guid guid)
        {
            return _context.Set<AccountRole>().Find(guid);
        }


        public AccountRole? Create(AccountRole accountRole)
        {
            try
            {   // menambah data AccountRole ke database
                _context.Set<AccountRole>().Add(accountRole);
                _context.SaveChanges();
                return accountRole; // return role jika berhasil
            }
            catch { return null; } // return null jika gagal 
        }

        public bool Update(AccountRole accountRole)
        {
            try
            {   // mengupdate perubahan AccountRole ke database
                _context.Set<AccountRole>().Update(accountRole);
                _context.SaveChanges();
                return true; // return true jike berhasil
            } catch { return false; } // return false jika gagal
        }

        public bool Delete(AccountRole accountRole)
        {
            try
            {   // menghapus data AccountRole di database
                _context.Set<AccountRole>().Remove(accountRole);
                _context.SaveChanges();
                return true; // return true jike berhasil
            }
            catch { return false; } // return false jika gagal
        }
    }
}
