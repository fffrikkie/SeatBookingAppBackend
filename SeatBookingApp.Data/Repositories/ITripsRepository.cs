
using SeatBookingApp.Domain.Entities;

namespace SeatBookingApp.Domain.Repositories
{
    public interface ITripsRepository
    {
        Task<List<Trip>> GetTrips(CancellationToken cancellationToken = default);
        Task<List<Trip>> GetVehicleTrips(string vehicleId, CancellationToken cancellationToken = default);
        Task<Trip> GetTripById(string tripId, CancellationToken cancellationToken = default);
        Task<string> RegisterVehicleTrip(Trip trip, CancellationToken cancellationToken = default);
        Task<string> DeleteTrip(Trip trip, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
