using API.Contracts;
using API.Data;
using API.DTOs.Accounts;
using API.DTOs.Universities;
using API.Models;
using API.Utilities.Handlers;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace API.Repositories
{
    public class GeneralRepository<TEntity> : IGeneralRepository<TEntity> where
    TEntity : class
    {
        // properti DbContext
        private readonly BookingManagementDbContext _context;
        //inisialisasi dbcontext (Contsructor)
        public GeneralRepository(BookingManagementDbContext context)
        {
            _context = context;
        }
        // Mendapatkan semua data dari database
        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }
        // Mendapatkan data berdasarkan Guid dari database
        public TEntity? GetByGuid(Guid guid)
        {
            var entity =  _context.Set<TEntity>().Find(guid);
            _context.ChangeTracker.Clear();
            return entity;
        }

        public TEntity? Create(TEntity data)
        {
            try
            {   // menambhah data ke database
                _context.Set<TEntity>().Add(data);
                _context.SaveChanges();
                return data; // return jika berhasil
            }
            catch (Exception ex)
            {
                throw new ExceptionHandler(ex.InnerException?.Message ?? ex.Message);
            } 
        }

        public bool Update(TEntity data)
        {
            try
            {   // mengupdate perubahan ke database
                _context.Set<TEntity>().Update(data);
                _context.SaveChanges();
                return true; // return true jike berhasil
            }
            catch (Exception ex)
            {
                throw new ExceptionHandler(ex.InnerException?.Message ?? ex.Message);
            } 

        }

        public bool Delete(TEntity data)
        {
            try
            {
                // menghapus data di database
                _context.Set<TEntity>().Remove(data);
                _context.SaveChanges();
                return true; // return true jike berhasil
            }
            catch (Exception ex)
            {
                throw new ExceptionHandler(ex.InnerException?.Message ?? ex.Message);
            } 
        }
    }
}
