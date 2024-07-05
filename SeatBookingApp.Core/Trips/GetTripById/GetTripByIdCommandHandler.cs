using MediatR;
using SeatBookingApp.Domain.Entities;
using SeatBookingApp.Domain.Repositories;

namespace SeatBookingApp.Application.Trips.GetTripById
{
    public class GetTripByIdCommandHandler : IRequestHandler<GetTripByIdCommand, Trip>
    {
        private readonly ITripsRepository _tripsRepository;
        public GetTripByIdCommandHandler(ITripsRepository tripsRepository)
        {
            _tripsRepository = tripsRepository ?? throw new ArgumentNullException(nameof(tripsRepository));
        }

        public async Task<Trip> Handle(GetTripByIdCommand command, CancellationToken cancellationToken)
        {
            return await _tripsRepository.GetTripById(command.TripId, cancellationToken);
        }
    }
}
