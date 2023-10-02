using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{   // class AccountRepository inheritance interface IAccountRepository
    // Isi Method sesuai interface
    public class AccountRepository : IAccountRepository
    {   // properti DbContext
        private readonly BookingManagementDbContext _context;
        //inisialisasi dbcontext (Contsructor)
        public AccountRepository(BookingManagementDbContext context)
        {
            _context = context;
        }
        // Mendapatkan semua data Account dari database
        public IEnumerable<Account> GetAll()
        {
            return _context.Set<Account>().ToList();
        }
        // Mendapatkan data berdasarkan Guid Account dari database
        public Account? GetByGuid(Guid guid)
        {
            return _context.Set<Account>().Find(guid);
        }
        public Account? Create(Account account)
        {
            try
            {   // menambhah data account ke database
                _context.Set<Account>().Add(account);
                _context.SaveChanges();
                return account; // return account jika berhasil
            }
            catch { return null; } // return null jika gagal
        }
        public bool Update(Account account)
        {
            try
            {   // mengupdate perubahan Account ke database
                _context.Set<Account>().Update(account);
                _context.SaveChanges();
                return true; // return true jike berhasil
            }
            catch { return false; } // return false jika gagal
        }

        public bool Delete(Account account)
        {
            try
            {   // menghapus data account di database
                _context.Set<Account>().Remove(account);
                _context.SaveChanges();
                return true; // return true jike berhasil
            }
            catch { return false; } // return false jika gagal
        }
    }
}
