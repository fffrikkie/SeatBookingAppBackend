using MediatR;
using SeatBookingApp.Domain.Repositories;

namespace SeatBookingApp.Application.Trips.DeleteTrip
{
    public class DeleteTripCommandHandler : IRequestHandler<DeleteTripCommand, string>
    {
        private readonly ITripsRepository _tripsRepository;
        private readonly IBookingsRepository _bookingsRepository;
        public DeleteTripCommandHandler(ITripsRepository tripsRepository, IBookingsRepository bookingsRepository)
        {
            _bookingsRepository = bookingsRepository ?? throw new ArgumentNullException(nameof(bookingsRepository));
            _tripsRepository = tripsRepository ?? throw new ArgumentNullException(nameof(tripsRepository));
        }

        public async Task<string> Handle(DeleteTripCommand command, CancellationToken cancellationToken)
        {
            // Get trip by id
            var tripToDelete = await _tripsRepository.GetTripById(command.Id, cancellationToken);

            if (tripToDelete == null)
            {
                throw new HttpRequestException("Trip not found.");
            }

            // Check if trip still has registered bookings
            var bookings = await _bookingsRepository.GetTripBookings(command.Id, cancellationToken);

            if (bookings != null && bookings.Count > 0)
            {
                throw new HttpRequestException($"Trip still has registered bookings.");
            }

            try
            {
                return await _tripsRepository.DeleteTrip(tripToDelete, cancellationToken);
            } catch (Exception ex)
            {
                throw new HttpRequestException("Failed to delete trip.");
            }
        }
    }
}
