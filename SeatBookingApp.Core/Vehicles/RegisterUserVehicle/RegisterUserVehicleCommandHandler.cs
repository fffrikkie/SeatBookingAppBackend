using MediatR;
using MongoDB.Bson;
using SeatBookingApp.Domain.Entities;
using SeatBookingApp.Domain.Repositories;

namespace SeatBookingApp.Application.Vehicles.RegisterUserVehicle
{
    public class RegisterUserVehicleCommandHandler : IRequestHandler<RegisterUserVehicleCommand, Vehicle>
    {
        private readonly IVehiclesRepository _vehiclesRepository;
        public RegisterUserVehicleCommandHandler(IVehiclesRepository vehiclesRepository)
        {
            _vehiclesRepository = vehiclesRepository ?? throw new ArgumentNullException(nameof(vehiclesRepository));
        }

        public async Task<Vehicle> Handle(RegisterUserVehicleCommand command, CancellationToken cancellationToken)
        {
            // Create new vehicle object
            var newVehicle = new Vehicle
            {
                Id = ObjectId.GenerateNewId().ToString(),
                OwnerName = command.OwnerName,
                Make = command.Make,
                Model = command.Model,
                Variant = command.Variant,
                NumberOfSeats = command.NumberOfSeats,
                RegistrationYear = command.RegistrationYear
            };
            // Register new vehicle under owner's name.
            var result = await _vehiclesRepository.RegisterUserVehicle(newVehicle, cancellationToken);
            
            return result;
        }
    }
}
