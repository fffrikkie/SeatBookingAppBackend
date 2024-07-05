using MediatR;
using SeatBookingApp.Domain.Repositories;

namespace SeatBookingApp.Application.Bookings.DeleteBooking
{
    public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand, string>
    {
        private readonly IBookingsRepository _bookingsRepository;
        private readonly ITripsRepository _tripsRepository;
        public DeleteBookingCommandHandler(IBookingsRepository bookingsRepository, ITripsRepository tripsRepository)
        {
            _bookingsRepository = bookingsRepository ?? throw new ArgumentNullException(nameof(bookingsRepository));
            _tripsRepository = tripsRepository ?? throw new ArgumentNullException(nameof(tripsRepository));
        }

        public async Task<string> Handle(DeleteBookingCommand command, CancellationToken cancellationToken)
        {
            // Get booking document to delete
            var bookingToDelete = await _bookingsRepository.GetBookingById(command.Id, cancellationToken);

            if (bookingToDelete == null)
            {
                throw new HttpRequestException("Booking not found.");
            }

            // Get trip linked to booking
            var trip = await _tripsRepository.GetTripById(bookingToDelete.TripId, cancellationToken);

            try
            {
                // Delete booking and update available seats of trip
                var deletedBookingId = await _bookingsRepository.DeleteBooking(bookingToDelete, cancellationToken);
                trip.TotalAvailableSeats = trip.TotalAvailableSeats + bookingToDelete.NumberOfSeatsBooked;

                await _bookingsRepository.SaveChangesAsync(cancellationToken);
                await _tripsRepository.SaveChangesAsync(cancellationToken);

                return deletedBookingId;
            } catch (Exception ex)
            {
                throw new HttpRequestException("Failed to delete booking");
            }
        }
    }
}
