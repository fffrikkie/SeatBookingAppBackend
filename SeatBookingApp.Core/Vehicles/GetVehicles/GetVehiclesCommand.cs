using System.Windows.Input;
using MediatR;

namespace SeatBookingApp.Application.Vehicles.GetVehicles
{
    public class GetVehiclesCommand : IRequest<List<VehiclesDTO>>
    {
        // What gets passed in
    }
}
