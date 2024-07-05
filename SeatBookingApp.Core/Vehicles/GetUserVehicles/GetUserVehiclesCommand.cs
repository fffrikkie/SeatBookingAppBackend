using System.Windows.Input;
using MediatR;

namespace SeatBookingApp.Application.Vehicles.GetUserVehicles
{
    public class GetUserVehiclesCommand : IRequest<List<VehiclesDTO>>
    {
        // What gets passed in
        public string? OwnerName { get; set; }
    }
}
