using MediatR;
using SeatBookingApp.Domain.Entities;
using SeatBookingApp.Domain.Repositories;

namespace SeatBookingApp.Application.Vehicles.GetVehicleById
{
    public class GetVehicleByIdCommandHandler : IRequestHandler<GetVehicleByIdCommand, Vehicle>
    {
        private readonly IVehiclesRepository _vehiclesRepository;
        public GetVehicleByIdCommandHandler(IVehiclesRepository vehiclesRepository)
        {
            _vehiclesRepository = vehiclesRepository ?? throw new ArgumentNullException(nameof(vehiclesRepository));
        }

        public async Task<Vehicle> Handle(GetVehicleByIdCommand command, CancellationToken cancellationToken)
        {
            return await _vehiclesRepository.GetVehicleById(command.VehicleId, cancellationToken);
        }
    }
}
