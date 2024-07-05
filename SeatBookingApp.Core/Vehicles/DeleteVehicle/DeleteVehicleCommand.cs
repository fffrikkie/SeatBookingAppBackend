using MediatR;

namespace SeatBookingApp.Application.Vehicles.DeleteVehicle
{
    public class DeleteVehicleCommand : IRequest<string>
    {
        public string? Id { get; set; }
    }
}
