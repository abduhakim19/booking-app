using API.Contracts;
using API.Data;
using API.Startup;
using API.Utilities.Handlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Koneksi
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BookingManagementDbContext>(option => option.UseSqlServer(connectionString));
// Inisialiasi Service untuk email
builder.Services.AddTransient<IEmailHandler, EmailHandler>(_ => new EmailHandler
            (
                builder.Configuration["SmtpService:Server"],
                int.Parse(builder.Configuration["SmtpService:Port"]),
                builder.Configuration["SmtpService:FromEmailAddress"]
            ));

// Memanggil service yang ada di file DependencyInjection.cs
builder.Services.RegisterServices();
// Authentication JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false; // for development only
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = builder.Configuration["JWTService:Issuer"],
                ValidateAudience = true,
                ValidAudience = builder.Configuration["JWTService:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTService:SecretKey"])),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });
// cors 
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
        policy.WithMethods("GET", "POST", "PUT", "DELETE");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.ConfigureSwagger();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
