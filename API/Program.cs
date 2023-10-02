using API.Data;
using API.Startup;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Koneksi
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BookingManagementDbContext>(option => option.UseSqlServer(connectionString));

// Memanggil service yang ada di file DependencyInjection.cs
builder.Services.RegisterServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.ConfigureSwagger();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
