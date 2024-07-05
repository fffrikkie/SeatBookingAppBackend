
using SeatBookingApp.Domain.Entities;

namespace SeatBookingApp.Domain.Repositories
{
    public interface IVehiclesRepository
    {
        Task<List<Vehicle>> GetVehicles(CancellationToken cancellationToken = default);
        Task<List<Vehicle>> GetUserVehicles(string ownerName, CancellationToken cancellationToken = default);
        Task<Vehicle> GetVehicleById(string vehicleId, CancellationToken cancellationToken = default);
        Task<Vehicle> RegisterUserVehicle(Vehicle vehicleDetails, CancellationToken cancellationToken = default);
        Task<string> DeleteVehicle(Vehicle vehicle, CancellationToken cancellationToken = default);
        Task<string> UpdateVehicle(Vehicle vehicle, CancellationToken cancellationToken = default);
    }
}
