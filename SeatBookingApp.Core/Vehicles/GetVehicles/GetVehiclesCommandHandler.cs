using MediatR;
using SeatBookingApp.Domain.Repositories;

namespace SeatBookingApp.Application.Vehicles.GetVehicles
{
    public class GetVehiclesCommandHandler : IRequestHandler<GetVehiclesCommand, List<VehiclesDTO>>
    {
        private readonly IVehiclesRepository _vehiclesRepository;
        public GetVehiclesCommandHandler(IVehiclesRepository vehiclesRepository)
        {
            _vehiclesRepository = vehiclesRepository ?? throw new ArgumentNullException(nameof(vehiclesRepository));
        }

        public async Task<List<VehiclesDTO>> Handle(GetVehiclesCommand command, CancellationToken cancellationToken)
        {
            var response = new List<VehiclesDTO>();
            var result = await _vehiclesRepository.GetVehicles(cancellationToken);
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
