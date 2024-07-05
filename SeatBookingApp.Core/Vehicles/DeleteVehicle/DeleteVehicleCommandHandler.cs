using MediatR;
using SeatBookingApp.Domain.Repositories;

namespace SeatBookingApp.Application.Vehicles.DeleteVehicle
{
    public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand, string>
    {
        private readonly IVehiclesRepository _vehiclesRepository;
        private readonly ITripsRepository _tripsRepository;
        public DeleteVehicleCommandHandler(IVehiclesRepository vehiclesRepository, ITripsRepository tripsRepository)
        {
            _vehiclesRepository = vehiclesRepository ?? throw new ArgumentNullException(nameof(vehiclesRepository));
            _tripsRepository = tripsRepository ?? throw new ArgumentNullException(nameof(tripsRepository));
        }

        public async Task<string> Handle(DeleteVehicleCommand command, CancellationToken cancellationToken)
        {
            // Get vehicle by id
            var vehicleToDelete = await _vehiclesRepository.GetVehicleById(command.Id, cancellationToken);

            if (vehicleToDelete == null)
            {
                throw new HttpRequestException("Vehicle not found.");
            }

            // Check if vehicle still has registered trips
            var trips = await _tripsRepository.GetVehicleTrips(command.Id, cancellationToken);

            if (trips != null && trips.Count > 0)
            {
                throw new HttpRequestException($"Vehicle still has registered trips.");
            }


            try
            {
                return await _vehiclesRepository.DeleteVehicle(vehicleToDelete, cancellationToken);
            } catch (Exception ex)
            {
                throw new HttpRequestException("Failed to delete vehicle.");
            }
        }
    }
}
