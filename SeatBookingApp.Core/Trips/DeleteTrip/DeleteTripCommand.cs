using MediatR;

namespace SeatBookingApp.Application.Trips.DeleteTrip
{
    public class DeleteTripCommand : IRequest<string>
    {
        public string? Id { get; set; }
    }
}
