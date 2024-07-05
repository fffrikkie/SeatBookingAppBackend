using MediatR;
using MongoDB.Bson;
using SeatBookingApp.Domain.Entities;
using SeatBookingApp.Domain.Repositories;

namespace SeatBookingApp.Application.Trips.RegisterVehicleTrip
{
    public class RegisterVehicleTripCommandHandler : IRequestHandler<RegisterVehicleTripCommand, Trip>
    {
        private readonly ITripsRepository _tripsRepository;
        public RegisterVehicleTripCommandHandler(ITripsRepository tripsRepository)
        {
            _tripsRepository = tripsRepository ?? throw new ArgumentNullException(nameof(tripsRepository));
        }

        public async Task<Trip> Handle(RegisterVehicleTripCommand command, CancellationToken cancellationToken)
        {
            // Create new trip document
            var newTrip = new Trip
            {
                Id = ObjectId.GenerateNewId().ToString(),
                StartDestination = command.StartDestination,
                EndDestination = command.EndDestination,
                DepartureDateTime = command.DepartureDateTime,
                TotalAvailableSeats = command.TotalAvailableSeats,
                TotalSeats = command.TotalSeats,
                VehicleId = command.VehicleId,
            };

            var createdTripId = await _tripsRepository.RegisterVehicleTrip(newTrip, cancellationToken);

            if (createdTripId == null)
            {
                throw new Exception("Failed to register trip for vehicle " + command.VehicleId);
            }

            await _tripsRepository.SaveChangesAsync(cancellationToken);
            return newTrip;
        }
    }
}
