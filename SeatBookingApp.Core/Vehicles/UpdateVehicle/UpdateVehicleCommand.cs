using MediatR;

namespace SeatBookingApp.Application.Vehicles.UpdateVehicle
{
    public class UpdateVehicleCommand : IRequest<string>
    {
        public string? Id { get; set; }
        public string? Make {  get; set; }
        public string? Model {  get; set; }
        public string? Variant {  get; set; }
        public int NumberOfSeats {  get; set; }
        public int RegistrationYear { get; set; }
    }
}
