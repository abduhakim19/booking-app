using Microsoft.EntityFrameworkCore;
using API.Models;


namespace API.Data
{
    public class BookingManagementDbContext : DbContext
    {
        public BookingManagementDbContext(DbContextOptions<BookingManagementDbContext> options) : base(options) 
        {
            //this.Database.EnsureCreated();
        }

        public DbSet<Account> Accounts { get;set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<University> universities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().HasIndex(e => new {
                e.Nik,
                e.Email,
                e.PhoneNumber
            }).IsUnique();

            // One University has many Educations
            modelBuilder.Entity<University>()
                        .HasMany(e => e.Educations)
                        .WithOne(u => u.University)
                        .HasForeignKey(u => u.UniversityGuid);

            // One Education has one Employee
            modelBuilder.Entity<Education>()
                    .HasOne(e => e.Employee)
                    .WithOne(e => e.Education)
                    .HasForeignKey<Education>(e => e.Guid);

            // One Employee has Many Booking
            modelBuilder.Entity<Employee>()
                        .HasMany(b => b.Bookings)
                        .WithOne(e => e.Employee)
                        .HasForeignKey(e => e.EmployeeGuid);

            // One room has Many Booking
            modelBuilder.Entity<Room>()
                        .HasMany(b => b.Bookings)
                        .WithOne(r => r.Room)
                        .HasForeignKey(r => r.RoomGuid);

            // One Employee has One Account
            modelBuilder.Entity<Employee>()
                        .HasOne(e => e.Account)
                        .WithOne(e => e.Employee)
                        .HasForeignKey<Employee>(e => e.Guid);

            // One Account has many Account Role
            modelBuilder.Entity<Account>()
                        .HasMany(a => a.AccountRoles)
                        .WithOne(c => c.Account)
                        .HasForeignKey(c => c.AccountGuid);

            // One Role has many Account Role
            modelBuilder.Entity<Role>()
                        .HasMany(a => a.AccountRoles)
                        .WithOne(r => r.Role)
                        .HasForeignKey(r => r.RoleGuid);


        }

        

    }
}
