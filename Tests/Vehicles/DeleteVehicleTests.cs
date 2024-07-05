using MongoDB.Bson;
using Moq;
using SeatBookingApp.Application.Vehicles.DeleteVehicle;
using SeatBookingApp.Domain.Entities;
using SeatBookingApp.Domain.Repositories;

namespace Tests.Vehicles
{
    public class DeleteVehicleTests
    {
        private readonly Mock<IVehiclesRepository> _mockVehiclesRepository;
        private readonly Mock<ITripsRepository> _mockTripsRepository;
        private readonly DeleteVehicleCommandHandler _handler;
        private readonly string _vehicleId;

        public DeleteVehicleTests()
        {
            _mockVehiclesRepository = new Mock<IVehiclesRepository>();
            _mockTripsRepository = new Mock<ITripsRepository>();
            _handler = new DeleteVehicleCommandHandler(_mockVehiclesRepository.Object, _mockTripsRepository.Object);
            _vehicleId = ObjectId.GenerateNewId().ToString();
        }

        private Vehicle GetTestVehicle()
        {
            return new Vehicle
            {
                Id = _vehicleId,
                OwnerName = "Hugo Esterhuyse",
                Make = "Suzuki",
                Model = "Baleno",
                Variant = "GL",
                RegistrationYear = 2023,
                NumberOfSeats = 5,
            };
        }

        private List<Trip> GetTestTrips()
        {
            return new List<Trip>
            {
                new Trip
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    VehicleId = _vehicleId,
                    TotalSeats = 4,
                    TotalAvailableSeats = 4,
                    StartDestination = "Pretoria",
                    EndDestination = "Johannesburg",
                    DepartureDateTime = DateTime.UtcNow,
                }
            };
        }

        private DeleteVehicleCommand GetTestCommand()
        {
            return new DeleteVehicleCommand
            {
                Id = ObjectId.GenerateNewId().ToString(),
            };
        }

        [Fact]
        public async Task Handle_ThrowsHttpRequestException_WhenVehicleNotFound()
        {
            // Arrange
            var command = GetTestCommand();
            _mockVehiclesRepository.Setup(repo => repo.GetVehicleById(command.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Vehicle)null);

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ThrowsHttpRequestException_WhenVehicleHasRegisterdTrips()
        {
            // Arrange
            var command = GetTestCommand();
            command.Id = _vehicleId;
            var vehicle = GetTestVehicle();
            var trips = GetTestTrips();
            _mockVehiclesRepository.Setup(repo => repo.GetVehicleById(command.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(vehicle);
            _mockTripsRepository.Setup(repo => repo.GetVehicleTrips(command.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(trips);

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_SuccessfullyDeletedVehicle()
        {
            // Arrange
            var command = GetTestCommand();
            command.Id = _vehicleId;
            var vehicle = GetTestVehicle();
            _mockVehiclesRepository.Setup(repo => repo.GetVehicleById(command.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(vehicle);
            _mockTripsRepository.Setup(repo => repo.GetVehicleTrips(command.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync((List<Trip>)null);
            _mockVehiclesRepository.Setup(repo => repo.DeleteVehicle(vehicle, It.IsAny<CancellationToken>())).ReturnsAsync(_vehicleId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(command.Id, result);
        }
    }
}
