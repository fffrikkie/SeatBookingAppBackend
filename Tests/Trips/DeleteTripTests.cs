using MongoDB.Bson;
using Moq;
using SeatBookingApp.Application.Trips.DeleteTrip;
using SeatBookingApp.Domain.Entities;
using SeatBookingApp.Domain.Repositories;

namespace Tests.Trips
{
    public class DeleteTripTests
    {
        private readonly Mock<ITripsRepository> _mockTripsRepository;
        private readonly Mock<IBookingsRepository> _mockBookingsRepository;
        private readonly DeleteTripCommandHandler _handler;
        private readonly string _tripId;

        public DeleteTripTests()
        {
            _mockBookingsRepository = new Mock<IBookingsRepository>();
            _mockTripsRepository = new Mock<ITripsRepository>();
            _handler = new DeleteTripCommandHandler(_mockTripsRepository.Object, _mockBookingsRepository.Object);
            _tripId = ObjectId.GenerateNewId().ToString();
        }

        private Trip GetTestTrip()
        {
            return new Trip
            {
                Id = _tripId,
                VehicleId = ObjectId.GenerateNewId().ToString(),
                DepartureDateTime = DateTime.UtcNow,
                EndDestination = "Pretoria",
                StartDestination = "Johannesburg",
                TotalAvailableSeats = 10,
                TotalSeats = 10,
            };
        }

        private List<Booking> GetTestBookings()
        {
            return new List<Booking>
            {
                new Booking
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    TripId = _tripId,
                    NumberOfSeatsBooked = 4,
                    TravellerName = "John Doe",
                }
            };
        }

        private DeleteTripCommand GetTestCommand()
        {
            return new DeleteTripCommand
            {
                Id = ObjectId.GenerateNewId().ToString(),
            };
        }

        [Fact]
        public async Task Handle_ThrowsHttpRequestException_WhenTripNotFound()
        {
            // Arrange
            var command = GetTestCommand();
            _mockTripsRepository.Setup(repo => repo.GetTripById(command.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Trip)null);

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ThrowsHttpRequestException_WhenTripHasRegisterdBookings()
        {
            // Arrange
            var command = GetTestCommand();
            command.Id = _tripId;
            var trip = GetTestTrip();
            var bookings = GetTestBookings();
            _mockTripsRepository.Setup(repo => repo.GetTripById(command.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(trip);
            _mockBookingsRepository.Setup(repo => repo.GetTripBookings(command.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(bookings);

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_SuccessfullyDeletedTrip()
        {
            // Arrange
            var command = GetTestCommand();
            command.Id = _tripId;
            var trip = GetTestTrip();
            _mockTripsRepository.Setup(repo => repo.GetTripById(command.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(trip);
            _mockBookingsRepository.Setup(repo => repo.GetTripBookings(command.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync((List<Booking>)null);
            _mockTripsRepository.Setup(repo => repo.DeleteTrip(trip, It.IsAny<CancellationToken>())).ReturnsAsync(_tripId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(command.Id, result);
        }
    }
}
