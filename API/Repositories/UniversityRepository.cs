using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{   // class UniversityRepository inheritance interface IUniversityRepository
    // Isi Method sesuai interface
    public class UniversityRepository : IUniversityRepository
    {   // properti DbContext 
        private readonly BookingManagementDbContext _context;
        //inisialisasi dbcontext (Contsructor)
        public UniversityRepository(BookingManagementDbContext context)
        {
            _context = context;
        }
        // Mendapatkan semua data University dari database
        public IEnumerable<University> GetAll()
        {
            return _context.Set<University>().ToList();
        }
        // Mendapatkan data berdasarkan Gui University dari database
        public University? GetByGuid(Guid guid)
        {
            return _context.Set<University>().Find(guid);
        }
        
        public University? Create(University university)
        {
            try
            {   // menambhah data university
                _context.Set<University>().Add(university);
                _context.SaveChanges(); // menyimpan perubahan
                return university;
            }
            catch
            {
                return null; // return null jika gagal
            }
        }

        public bool Update(University university)
        {
            try
            {   // mengupdate perubahan university
                _context.Set<University>().Update(university);
                _context.SaveChanges(); // menyimpan perubahan
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(University university)
        {
            try
            {   // menghapus data university
                _context.Set<University>().Remove(university);
                _context.SaveChanges(); // menyimpan perubahan
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
