using MediatR;
using SeatBookingApp.Application.Vehicles;
using SeatBookingApp.Domain.Repositories;

namespace SeatBookingApp.Application.Trips.GetTrips
{
    public class GetTripsCommandHandler : IRequestHandler<GetTripsCommand, List<TripsDTO>>
    {
        private readonly ITripsRepository _tripsRepository;
        public GetTripsCommandHandler(ITripsRepository tripsRepository)
        {
            _tripsRepository = tripsRepository ?? throw new ArgumentNullException(nameof(tripsRepository));
        }

        public async Task<List<TripsDTO>> Handle(GetTripsCommand command, CancellationToken cancellationToken)
        {
            var response = new List<TripsDTO>();
            var result = await _tripsRepository.GetTrips(cancellationToken);
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
