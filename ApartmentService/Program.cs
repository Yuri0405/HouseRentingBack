using ApartmentService.Interfaces;
using ApartmentService.Models;
using ApartmentService.Models.DataContext;
using ApartmentService.Serivces;
using Microsoft.EntityFrameworkCore;

namespace ApartmentService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApartmentDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<ApartmentDataService>();
            builder.Services.AddScoped<IRepository<Apartment>,ApartmentRepository>();

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
