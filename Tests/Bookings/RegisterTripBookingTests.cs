using MongoDB.Bson;
using Moq;
using SeatBookingApp.Application.Bookings.RegisterTripBooking;
using SeatBookingApp.Domain.Entities;
using SeatBookingApp.Domain.Repositories;

namespace Tests.Bookings
{
    public class RegisterTripBookingTests
    {
        private readonly Mock<IBookingsRepository> _mockBookingsRepository;
        private readonly Mock<ITripsRepository> _mockTripsRepository;
        private readonly RegisterTripBookingCommandHandler _handler;

        public RegisterTripBookingTests()
        {
            _mockBookingsRepository = new Mock<IBookingsRepository>();
            _mockTripsRepository = new Mock<ITripsRepository>();
            _handler = new RegisterTripBookingCommandHandler(_mockBookingsRepository.Object, _mockTripsRepository.Object);
        }

        private Trip GetTestTrip()
        {
            return new Trip
            {
                Id = ObjectId.GenerateNewId().ToString(),
                VehicleId = ObjectId.GenerateNewId().ToString(),
                DepartureDateTime = DateTime.UtcNow,
                EndDestination = "Pretoria",
                StartDestination = "Johannesburg",
                TotalAvailableSeats = 10,
                TotalSeats = 10,
            };
        }

        private RegisterTripBookingCommand GetTestCommand()
        {
            return new RegisterTripBookingCommand
            {
                TripId = ObjectId.GenerateNewId().ToString(),
                NumberOfSeatsBooked = 3,
                TravellerName = "John Doe",
            };
        }

        [Fact]
        public async Task Handle_ThrowsHttpRequestException_WhenTripNotFound()
        {
            // Arrange
            var command = GetTestCommand();
            _mockTripsRepository.Setup(repo => repo.GetTripById(command.TripId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Trip)null);

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ThrowsHttpRequestException_WhenNotEnoughSeats()
        {
            // Arrange
            var command = GetTestCommand();
            var trip = GetTestTrip();
            trip.TotalAvailableSeats = 2; // Not enough seats available
            _mockTripsRepository.Setup(repo => repo.GetTripById(command.TripId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(trip);

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_SuccessfullyRegistersBooking_WhenSeatsAvailable()
        {
            // Arrange
            var command = GetTestCommand();
            var trip = GetTestTrip();
            _mockTripsRepository.Setup(repo => repo.GetTripById(command.TripId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(trip);
            _mockBookingsRepository.Setup(repo => repo.RegisterTripBooking(It.IsAny<Booking>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(ObjectId.GenerateNewId().ToString());
            _mockBookingsRepository.Setup(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
            _mockTripsRepository.Setup(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(command.TripId, result.TripId);
            Assert.Equal(command.TravellerName, result.TravellerName);
            Assert.Equal(command.NumberOfSeatsBooked, result.NumberOfSeatsBooked);
        }
    }
}
