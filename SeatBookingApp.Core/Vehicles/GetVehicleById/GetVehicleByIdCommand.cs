using MediatR;
using SeatBookingApp.Domain.Entities;

namespace SeatBookingApp.Application.Vehicles.GetVehicleById
{
    public class GetVehicleByIdCommand : IRequest<Vehicle>
    {
        public string? VehicleId { get; set; }
    }
}
