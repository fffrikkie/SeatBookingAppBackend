using MediatR;
using SeatBookingApp.Domain.Repositories;

namespace SeatBookingApp.Application.Vehicles.UpdateVehicle
{
    public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand, string>
    {
        private readonly IVehiclesRepository _vehiclesRepository;
        private readonly ITripsRepository _tripsRepository;
        public UpdateVehicleCommandHandler(IVehiclesRepository vehiclesRepository, ITripsRepository tripsRepository)
        {
            _vehiclesRepository = vehiclesRepository ?? throw new ArgumentNullException(nameof(vehiclesRepository));
            _tripsRepository = tripsRepository ?? throw new ArgumentNullException(nameof(tripsRepository));
        }

        public async Task<string> Handle(UpdateVehicleCommand command, CancellationToken cancellationToken)
        {
            // Get vehicle to update
            var vehicleToUpdate = await _vehiclesRepository.GetVehicleById(command.Id, cancellationToken);

            if (vehicleToUpdate == null)
            {
                throw new HttpRequestException("Vehicle not found.");
            }

            // Update vehicle details
            vehicleToUpdate.Make = command.Make;
            vehicleToUpdate.Model = command.Model;
            vehicleToUpdate.Variant = command.Variant;
            vehicleToUpdate.RegistrationYear = command.RegistrationYear;
            vehicleToUpdate.NumberOfSeats = command.NumberOfSeats;

            try
            {
                return await _vehiclesRepository.UpdateVehicle(vehicleToUpdate, cancellationToken);
            } catch (Exception ex)
            {
                throw new HttpRequestException("Failed to update vehicle.");
            }
        }
    }
}
