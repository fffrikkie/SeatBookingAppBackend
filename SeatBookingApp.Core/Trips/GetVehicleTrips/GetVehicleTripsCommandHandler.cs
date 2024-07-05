using MediatR;
using SeatBookingApp.Application.Vehicles;
using SeatBookingApp.Domain.Repositories;

namespace SeatBookingApp.Application.Trips.GetVehicleTrips
{
    public class GetVehicleTripsCommandHandler : IRequestHandler<GetVehicleTripsCommand, List<TripsDTO>>
    {
        private readonly ITripsRepository _tripsRepository;
        public GetVehicleTripsCommandHandler(ITripsRepository tripsRepository)
        {
            _tripsRepository = tripsRepository ?? throw new ArgumentNullException(nameof(tripsRepository));
        }

        public async Task<List<TripsDTO>> Handle(GetVehicleTripsCommand command, CancellationToken cancellationToken)
        {
            var response = new List<TripsDTO>();
            // Get trips by vehicle id
            var result = await _tripsRepository.GetVehicleTrips(command.VehicleId, cancellationToken);
            foreach (var trip in result)
            {
                response.Add(new TripsDTO
                {
                    Id = trip.Id,
                    VehicleId = trip.VehicleId,
                    StartDestination = trip.StartDestination,
                    EndDestination = trip.EndDestination,
                    TotalSeats = trip.TotalSeats,
                    TotalAvailableSeats = trip.TotalAvailableSeats,
                    DepartureDateTime = trip.DepartureDateTime,
                });
            }
            return response;
        }
    }
}
