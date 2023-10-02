using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    // class EmployeeRepository inheritance interface IEmployeeRepository
    // Isi Method sesuai interface
    public class EmployeeRepository : IEmployeeRepository
    {   // properti DbContext
        private readonly BookingManagementDbContext _context;
        //inisialisasi dbcontext (Contsructor)
        public EmployeeRepository(BookingManagementDbContext context)
        {
            _context = context;
        }
        // Mendapatkan semua data Employee dari database
        public IEnumerable<Employee> GetAll()
        {
            return _context.Set<Employee>().ToList();
        }
        // Mendapatkan data berdasarkan Guid Employee di database
        public Employee? GetByGuid(Guid guid)
        {
            return _context.Set<Employee>().Find(guid);
        }

        public Employee? Create(Employee employee)
        {
            try
            {   // menambah data employee ke database
                _context.Set<Employee>().Add(employee);
                _context.SaveChanges();
                return employee; // return employee jika berhasil
            }
            catch
            {
                return null; // return null jika gagal
            }
        }

        public bool Update(Employee employee)
        {
            try
            {   // mengupdate perubahan role ke database
                _context.Set<Employee>().Update(employee);
                _context.SaveChanges();
                return true; // return true jike berhasil
            }
            catch { return false; } // return false jika gagal
        }

        public bool Delete(Employee employee)
        {
            try
            {
                // menghapus data employee ke database
                _context.Set<Employee>().Remove(employee);
                _context.SaveChanges();
                return true; // return true jike berhasil
            }
            catch { return false; } // return false jika gagal
        }
    }
}
