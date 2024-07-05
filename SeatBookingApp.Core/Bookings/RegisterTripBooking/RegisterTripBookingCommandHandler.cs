using MediatR;
using MongoDB.Bson;
using SeatBookingApp.Domain.Entities;
using SeatBookingApp.Domain.Repositories;

namespace SeatBookingApp.Application.Bookings.RegisterTripBooking
{
    public class RegisterTripBookingCommandHandler : IRequestHandler<RegisterTripBookingCommand, Booking>
    {
        private readonly IBookingsRepository _bookingsRepository;
        private readonly ITripsRepository _tripsRepository;
        public RegisterTripBookingCommandHandler(IBookingsRepository bookingsRepository, ITripsRepository tripsRepository)
        {
            _bookingsRepository = bookingsRepository ?? throw new ArgumentNullException(nameof(bookingsRepository));
            _tripsRepository = tripsRepository ?? throw new ArgumentNullException(nameof(tripsRepository));
        }

        public async Task<Booking> Handle(RegisterTripBookingCommand command, CancellationToken cancellationToken)
        {
            // Get trip by trip id
            var trip = await _tripsRepository.GetTripById(command.TripId, cancellationToken);
            if (trip == null)
            {
                throw new HttpRequestException($"Trip with id {command.TripId} not found");
            }

            // Check if requested seats will exceed available seats.
            if (trip.TotalAvailableSeats - command.NumberOfSeatsBooked < 0 || trip.TotalAvailableSeats == 0)
            {
                throw new HttpRequestException("Not enough available seats.");
            }

            var newBooking = new Booking
            {
                Id = ObjectId.GenerateNewId().ToString(),
                NumberOfSeatsBooked = command.NumberOfSeatsBooked,
                TravellerName = command.TravellerName,
                TripId = command.TripId,
            };

            // Create booking
            var createdBookingId = await _bookingsRepository.RegisterTripBooking(newBooking, cancellationToken);

            if (createdBookingId == null)
            {
                throw new Exception("Failed to make booking.");
            }

            // Update trip total available seats
            trip.TotalAvailableSeats = trip.TotalAvailableSeats - command.NumberOfSeatsBooked;

            await _bookingsRepository.SaveChangesAsync(cancellationToken);
            await _tripsRepository.SaveChangesAsync(cancellationToken);

            return newBooking;
        }
    }
}
