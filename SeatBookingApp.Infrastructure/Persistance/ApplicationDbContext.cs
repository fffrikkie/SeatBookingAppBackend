using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using SeatBookingApp.Domain.Entities;

namespace SeatBookingApp.Infrastructure.Persistance
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Vehicle>().ToCollection("Vehicles");
            modelBuilder.Entity<Trip>().ToCollection("Trips");
            modelBuilder.Entity<Booking>().ToCollection("Bookings");
        }
    }
}
