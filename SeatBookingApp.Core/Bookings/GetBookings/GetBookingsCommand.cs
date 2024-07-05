using MediatR;
using SeatBookingApp.Domain.Entities;

namespace SeatBookingApp.Application.Bookings.GetBookings
{
    public class GetBookingsCommand : IRequest<List<Booking>>
    {
        // What gets passed in
    }
}
