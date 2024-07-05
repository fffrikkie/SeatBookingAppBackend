using MediatR;
using SeatBookingApp.Domain.Entities;

namespace SeatBookingApp.Application.Bookings.RegisterTripBooking
{
    public class RegisterTripBookingCommand : IRequest<Booking>
    {
        public string? TripId { get; set; }
        public string? TravellerName { get; set; }
        public int NumberOfSeatsBooked { get; set; }
    }
}
