using MediatR;
using SeatBookingApp.Domain.Entities;

namespace SeatBookingApp.Application.Trips.GetTripById
{
    public class GetTripByIdCommand : IRequest<Trip>
    {
        public string? TripId { get; set; }
    }
}
