using MediatR;

namespace SeatBookingApp.Application.Trips.UpdateTrip
{
    public class UpdateTripCommand : IRequest<string>
    {
        public string? Id { get; set; }
        public string? StartDestination { get; set; }
        public string? EndDestination { get; set; }
        public DateTimeOffset DepartureDateTime { get; set; }
    }
}
