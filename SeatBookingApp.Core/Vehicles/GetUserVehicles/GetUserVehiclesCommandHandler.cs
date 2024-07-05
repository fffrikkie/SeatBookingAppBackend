using MediatR;
using SeatBookingApp.Domain.Repositories;

namespace SeatBookingApp.Application.Vehicles.GetUserVehicles
{
    public class GetUserVehiclesCommandHandler : IRequestHandler<GetUserVehiclesCommand, List<VehiclesDTO>>
    {
        private readonly IVehiclesRepository _vehiclesRepository;
        public GetUserVehiclesCommandHandler(IVehiclesRepository vehiclesRepository)
        {
            _vehiclesRepository = vehiclesRepository ?? throw new ArgumentNullException(nameof(vehiclesRepository));
        }

        public async Task<List<VehiclesDTO>> Handle(GetUserVehiclesCommand command, CancellationToken cancellationToken)
        {
            var response = new List<VehiclesDTO>();
            // Get vehicles by owner name
            var result = await _vehiclesRepository.GetUserVehicles(command.OwnerName, cancellationToken);
            foreach (var vehicle in result)
            {
                response.Add(new VehiclesDTO
                {
                    Id = vehicle.Id,
                    OwnerName = vehicle.OwnerName,
                    DisplayName = $"{vehicle.RegistrationYear} {vehicle.Make} {vehicle.Model} {vehicle.Variant}",
                    Make = vehicle.Make,
                    Model = vehicle.Model,
                    Variant = vehicle.Variant,
                    RegistrationYear = vehicle.RegistrationYear,
                    NumberOfSeats = vehicle.NumberOfSeats,
                });
            }
            return response;
        }
    }
}
