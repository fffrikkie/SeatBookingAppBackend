using MediatR;

namespace SeatBookingApp.Application.Bookings.UpdateBooking
{
    public class UpdateBookingCommand : IRequest<string>
    {
        public string? Id { get; set; }
        public string? TravellerName { get; set; }
        public int NumberOfSeatsBooked { get; set; }
    }
}
