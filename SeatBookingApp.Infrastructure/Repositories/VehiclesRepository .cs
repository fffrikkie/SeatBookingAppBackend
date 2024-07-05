using Microsoft.EntityFrameworkCore;
using SeatBookingApp.Domain.Entities;
using SeatBookingApp.Domain.Repositories;
using SeatBookingApp.Infrastructure.Persistance;

namespace SeatBookingApp.Infrastructure.Repositories
{
    public class VehiclesRepository : IVehiclesRepository
    {
        private readonly ApplicationDbContext _context;
        public VehiclesRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<Vehicle>> GetVehicles(CancellationToken cancellationToken)
        {
            return await _context.Vehicles.ToListAsync(cancellationToken);
        }

        public async Task<List<Vehicle>> GetUserVehicles(string ownerName, CancellationToken cancellationToken)
        {
            return await _context.Vehicles.Where(vehicle => vehicle.OwnerName == ownerName).ToListAsync(cancellationToken);
        }

        public async Task<Vehicle> GetVehicleById(string vehicleId, CancellationToken cancellationToken = default)
        {
            return await _context.Vehicles.Where(vehicle => vehicle.Id == vehicleId).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Vehicle> RegisterUserVehicle(Vehicle vehicleDetails, CancellationToken cancellationToken)
        {
            _context.Vehicles.Add(vehicleDetails);
            await _context.SaveChangesAsync(cancellationToken);
            return vehicleDetails;
        }

        public async Task<string> DeleteVehicle(Vehicle vehicle, CancellationToken cancellationToken = default)
        {
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync(cancellationToken);
            return vehicle.Id;
        }

        public async Task<string> UpdateVehicle(Vehicle vehicle, CancellationToken cancellationToken = default)
        {
            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync(cancellationToken);
            return vehicle.Id;
        }
    }
}
