using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{   // class EducationRepository inheritance interface IEducationRepository
    // Isi Method sesuai interface
    public class EducationRepository : IGeneralRepository<Education> ,IEducationRepository
    {   // properti DbContext
        private readonly BookingManagementDbContext _context;
        //inisialisasi dbcontext (Contsructor)
        public EducationRepository(BookingManagementDbContext context)
        {
            _context = context;
        }
        // Mendapatkan semua data Education dari database
        public IEnumerable<Education> GetAll()
        {
            return _context.Set<Education>().ToList();
        }
        // Mendapatkan data berdasarkan Guid Employee dari database
        public Education? GetByGuid(Guid guid)
        {
            return _context.Set<Education>().Find(guid);
        }

        public Education? Create(Education education)
        {
            try
            {   // menambhah data employee ke database
                _context.Set<Education>().Add(education);
                _context.SaveChanges();
                return education; // return employee jika berhasil
            }
            catch
            {
                return null; // return null jika gagal
            }
        }

        public bool Update(Education education)
        {
            try
            {   // mengupdate perubahan employee ke database
                _context.Set<Education>().Update(education);
                _context.SaveChanges();
                return true; // return true jike berhasil
            }
            catch { return false; }  // return false jika gagal
        }

        public bool Delete(Education education)
        {
            try
            {   // menghapus data role di database
                _context.Set<Education>().Remove(education);
                _context.SaveChanges();
                return true; // return true jike berhasil
            }
            catch { return false; } // return false jika gagal
        }
    }
}
