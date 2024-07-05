using MediatR;
using SeatBookingApp.Domain.Entities;

namespace SeatBookingApp.Application.Bookings.GetBookingById
{
    public class GetBookingByIdCommand : IRequest<Booking>
    {
        public string? Id { get; set; }
    }
}
