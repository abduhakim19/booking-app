using API.Contracts;
using API.Data;
using API.Startup;
using API.Utilities.Handlers;
using Microsoft.EntityFrameworkCore;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
app.ConfigureSwagger();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
