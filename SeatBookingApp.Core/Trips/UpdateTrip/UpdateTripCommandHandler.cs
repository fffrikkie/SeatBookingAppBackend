using MediatR;
using SeatBookingApp.Domain.Repositories;

namespace SeatBookingApp.Application.Trips.UpdateTrip
{
    public class UpdateTripCommandHandler : IRequestHandler<UpdateTripCommand, string>
    {
        private readonly ITripsRepository _tripsRepository;
        private readonly IBookingsRepository _bookingsRepository;
        public UpdateTripCommandHandler(ITripsRepository tripsRepository, IBookingsRepository bookingsRepository)
        {
            _bookingsRepository = bookingsRepository ?? throw new ArgumentNullException(nameof(bookingsRepository));
            _tripsRepository = tripsRepository ?? throw new ArgumentNullException(nameof(tripsRepository));
        }

        public async Task<string> Handle(UpdateTripCommand command, CancellationToken cancellationToken)
        {
            // Get trip by id
            var tripToUpdate= await _tripsRepository.GetTripById(command.Id, cancellationToken);

            if (tripToUpdate == null)
            {
                throw new HttpRequestException("Trip not found.");
            }

            // Update trip details
            tripToUpdate.StartDestination = command.StartDestination;
            tripToUpdate.EndDestination = command.EndDestination;
            tripToUpdate.DepartureDateTime = command.DepartureDateTime;

            await _tripsRepository.SaveChangesAsync(cancellationToken);
            return tripToUpdate.Id;
        }
    }
}
