using System.Windows.Input;
using MediatR;

namespace SeatBookingApp.Application.Trips.GetVehicleTrips
{
    public class GetVehicleTripsCommand : IRequest<List<TripsDTO>>
    {
        // What gets passed in
        public string? VehicleId { get; set; }
    }
}
