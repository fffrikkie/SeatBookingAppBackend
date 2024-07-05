using MediatR;

namespace SeatBookingApp.Application.Bookings.GetTripBookings
{
    public class GetTripBookingsCommand : IRequest<TripBookingsDTO>
    {
        public string? TripId { get; set; }
        // What gets passed in
    }
}
