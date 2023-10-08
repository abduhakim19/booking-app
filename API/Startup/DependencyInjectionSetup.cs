using API.Contracts;
using API.Repositories;
using API.Utilities.Handlers;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
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
            services.AddScoped<ITokenHandler, TokenHandler>();

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
            services.AddSwaggerGen(x => {
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Metrodata Coding Camp",
                    Description = "ASP.NET Core API 6.0"
                });
                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
                }
            });
            });
            // inisialisai untuk menambahkan fluentvalidator
            services.AddFluentValidationAutoValidation()
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
