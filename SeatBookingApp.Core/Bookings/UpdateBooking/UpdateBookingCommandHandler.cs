using MediatR;
using SeatBookingApp.Domain.Repositories;

namespace SeatBookingApp.Application.Bookings.UpdateBooking
{
    public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, string>
    {
        private readonly IBookingsRepository _bookingsRepository;
        private readonly ITripsRepository _tripsRepository;
        public UpdateBookingCommandHandler(IBookingsRepository bookingsRepository, ITripsRepository tripsRepository)
        {
            _bookingsRepository = bookingsRepository ?? throw new ArgumentNullException(nameof(bookingsRepository));
            _tripsRepository = tripsRepository ?? throw new ArgumentNullException(nameof(tripsRepository));
        }

        public async Task<string> Handle(UpdateBookingCommand command, CancellationToken cancellationToken)
        {
            // Get booking to by id
            var bookingToUpdate = await _bookingsRepository.GetBookingById(command.Id, cancellationToken);

            if (bookingToUpdate == null)
            {
                throw new HttpRequestException("Booking not found.");
            }
            // Get booking's linked trip
            var trip = await _tripsRepository.GetTripById(bookingToUpdate.TripId, cancellationToken);

            // Check if new requested seat amount exceeds total available seats.
            if (trip.TotalAvailableSeats + bookingToUpdate.NumberOfSeatsBooked - command.NumberOfSeatsBooked < 0)
            {
                throw new HttpRequestException("Number of seats booked exceeds total number of seats");
            }

            // Update trip and booking info
            trip.TotalAvailableSeats += bookingToUpdate.NumberOfSeatsBooked - command.NumberOfSeatsBooked;
            bookingToUpdate.TravellerName = command.TravellerName;
            bookingToUpdate.NumberOfSeatsBooked = command.NumberOfSeatsBooked;

            try
            {
                await _bookingsRepository.SaveChangesAsync(cancellationToken);
                await _tripsRepository.SaveChangesAsync(cancellationToken);

                return bookingToUpdate.Id;
            } catch (Exception ex)
            {
                throw new HttpRequestException("Failed to delete booking");
            }
        }
    }
}
