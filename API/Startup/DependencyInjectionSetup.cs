using API.Contracts;
using API.Repositories;

namespace API.Startup
{
    public static class DependencyInjectionSetup
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            // Add services to the container.

            // Menginstance UniversityRepository dan IUniversityRepository
            services.AddScoped<IUniversityRepository, UniversityRepository>();
            // Menginstance AccountRepository dan IAccountRepository
            services.AddScoped<IAccountRepository, AccountRepository>();
            // Menginstance AccountRoleRepository dan IAccountRoleRepository
            services.AddScoped<IAccountRoleRepository, AccountRoleRepository>();
            // Menginstance BookingRepository dan IBookingRepository
            services.AddScoped<IBookingRepository, BookingRepository>();
            // Menginstance EducationRepository dan IEducationRepository
            services.AddScoped<IEducationRepository, EducationRepository>();
            // Menginstance RoleRepository dan IRoleRepository
            services.AddScoped<IRoleRepository, RoleRepository>();
            // Menginstance RoomRepository dan IRoomRepository
            services.AddScoped<IRoomRepository, RoomRepository>();
            // Menginstance EmployeeRepository dan IEmployeeRepository
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}
