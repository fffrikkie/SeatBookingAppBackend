using SeatBookingApp.Domain.Entities;
using SeatBookingApp.Domain.Repositories;
using SeatBookingApp.Infrastructure.Persistance;

namespace SeatBookingApp.Infrastructure.Repositories
{
    public class TripsRepository : ITripsRepository
    {
        private readonly ApplicationDbContext _context;
        public TripsRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Trip> GetTripById(string tripId, CancellationToken cancellationToken = default)
        {
            return _context.Trips.Where(trip => trip.Id == tripId).FirstOrDefault();
        }

        public async Task<List<Trip>> GetTrips(CancellationToken cancellationToken = default)
        {
            return _context.Trips.Where(trip => trip.DepartureDateTime > DateTime.Now).ToList();
        }

        public async Task<List<Trip>> GetVehicleTrips(string vehicleId, CancellationToken cancellationToken = default)
        {
            return _context.Trips.Where(trip => trip.VehicleId == vehicleId).ToList();
        }

        public async Task<string> RegisterVehicleTrip(Trip trip, CancellationToken cancellationToken = default)
        {
            _context.Trips.Add(trip);
            return trip.Id;
        }

        public async Task<string> DeleteTrip(Trip trip, CancellationToken cancellationToken = default)
        {
            _context.Trips.Remove(trip);
            await _context.SaveChangesAsync(cancellationToken);
            return trip.Id;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
