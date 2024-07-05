using System.Windows.Input;
using MediatR;
using SeatBookingApp.Domain.Entities;

namespace SeatBookingApp.Application.Trips.RegisterVehicleTrip
{
    public class RegisterVehicleTripCommand : IRequest<Trip>
    {
        // What gets passed in
        public string? VehicleId { get; set; }
        public string? StartDestination { get; set; }
        public string? EndDestination { get; set; }
        public int TotalSeats { get; set; }
        public int TotalAvailableSeats { get; set; }
        public DateTimeOffset DepartureDateTime { get; set; }
    }
}
