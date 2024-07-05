using MediatR;
using SeatBookingApp.Domain.Repositories;

namespace SeatBookingApp.Application.Bookings.GetTripBookings
{
    public class GetTripBookingsCommandHandler : IRequestHandler<GetTripBookingsCommand, TripBookingsDTO>
    {
        private readonly IBookingsRepository _bookingsRepository;
        private readonly ITripsRepository _tripsRepository;
        private readonly IVehiclesRepository _vehiclesRepository;
        public GetTripBookingsCommandHandler(IBookingsRepository bookingsRepository, ITripsRepository tripsRepository, IVehiclesRepository vehiclesRepository)
        {
            _bookingsRepository = bookingsRepository ?? throw new ArgumentNullException(nameof(bookingsRepository));
            _tripsRepository = tripsRepository ?? throw new ArgumentNullException(nameof(tripsRepository));
            _vehiclesRepository = vehiclesRepository ?? throw new ArgumentNullException(nameof(vehiclesRepository));
        }

        public async Task<TripBookingsDTO> Handle(GetTripBookingsCommand command, CancellationToken cancellationToken)
        {
            // Get vehicle, trip and booking info of speicifc trip
            var tripBookings = await _bookingsRepository.GetTripBookings(command.TripId, cancellationToken);
            var tripInformation = await _tripsRepository.GetTripById(command.TripId, cancellationToken);
            var vehicleInformation = await _vehiclesRepository.GetVehicleById(tripInformation.VehicleId, cancellationToken);

            return new TripBookingsDTO
            {
                Bookings = tripBookings,
                VehicleInfo = vehicleInformation,
                TripInfo = tripInformation,
            };
        }
    }
}
