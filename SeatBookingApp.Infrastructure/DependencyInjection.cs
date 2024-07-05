using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SeatBookingApp.Domain.Repositories;
using SeatBookingApp.Infrastructure.Persistance;
using SeatBookingApp.Infrastructure.Repositories;

namespace SeatBookingApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMongoDB<ApplicationDbContext>(configuration["ConnectionStrings:DefaultConnection"], configuration.GetSection("DatabaseName").Value);
            services.AddTransient<IVehiclesRepository, VehiclesRepository>();
            services.AddTransient<ITripsRepository, TripsRepository>();
            services.AddTransient<IBookingsRepository, BookingsRepository>();

            return services;
        }
    }
}
