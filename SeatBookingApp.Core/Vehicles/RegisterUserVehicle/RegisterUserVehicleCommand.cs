using System.Windows.Input;
using MediatR;
using SeatBookingApp.Domain.Entities;

namespace SeatBookingApp.Application.Vehicles.RegisterUserVehicle
{
    public class RegisterUserVehicleCommand : IRequest<Vehicle>
    {
        // What gets passed in
        public string? OwnerName { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public string? Variant { get; set; }
        public int? RegistrationYear { get; set; }
        public int? NumberOfSeats { get; set; }
    }
}
