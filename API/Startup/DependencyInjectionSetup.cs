using API.Contracts;
using API.Repositories;
using API.Utilities.Handlers;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

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

            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    // Response Error Validation 
                    options.InvalidModelStateResponseFactory = context =>
                    {
                    var errors = context.ModelState.Values
                                        .SelectMany(v => v.Errors)
                                        .Select(v => v.ErrorMessage);
                    return new BadRequestObjectResult(new ResponseValidatorHandler(errors));
                    };
                });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            // inisialisai untuk menambahkan fluentvalidator
            services.AddFluentValidationAutoValidation()
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
