using MediatR;

namespace SeatBookingApp.Application.Bookings.DeleteBooking
{
    public class DeleteBookingCommand : IRequest<string>
    {
        public string? Id { get; set; }
    }
}
