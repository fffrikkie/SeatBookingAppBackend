using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeatBookingApp.Application.Trips;
using SeatBookingApp.Application.Trips.DeleteTrip;
using SeatBookingApp.Application.Trips.GetTripById;
using SeatBookingApp.Application.Trips.GetTrips;
using SeatBookingApp.Application.Trips.GetVehicleTrips;
using SeatBookingApp.Application.Trips.RegisterVehicleTrip;
using SeatBookingApp.Application.Trips.UpdateTrip;

namespace SeatBookingApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TripController : ControllerBase
    {
        private readonly ILogger<TripController> _logger;
        private readonly ISender _mediator;

        public TripController(ILogger<TripController> logger, ISender mediator)
        {
            _logger = logger;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Gets a list of all trips
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetTrips")]
        [ProducesResponseType(typeof(List<TripsDTO>), 200)]
        public async Task<ActionResult<List<TripsDTO>>> GetTrips(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetTripsCommand(), cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Gets a specific trip by it's ID
        /// </summary>
        /// <param name="tripId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetTripById")]
        [ProducesResponseType(typeof(TripsDTO), 200)]
        public async Task<ActionResult<TripsDTO>> GetTripById(string tripId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetTripByIdCommand { TripId = tripId }, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Get a vehicle's trips by the vehicle ID
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetVehicleTrips")]
        [ProducesResponseType(typeof(List<TripsDTO>), 200)]
        public async Task<ActionResult<List<TripsDTO>>> GetVehicleTrips(string vehicleId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetVehicleTripsCommand { VehicleId = vehicleId }, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Register a trip for a specific vehicle
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("RegisterVehicleTrip")]
        [ProducesResponseType(typeof(TripsDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TripsDTO>> RegisterVehicleTrip([FromBody] RegisterVehicleTripCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Created("RegisterVehicleTrip", result);
        }

        /// <summary>
        /// Updates a specific booking's details
        /// </summary>
        /// <param name="tripId"></param>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPatch("UpdateTrip/{tripId}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateTrip([FromRoute] string tripId, [FromBody] UpdateTripCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var updatedCommand = new UpdateTripCommand
                {
                    Id = tripId,
                    StartDestination = command.StartDestination,
                    EndDestination = command.EndDestination,
                    DepartureDateTime = command.DepartureDateTime,
                };
                await _mediator.Send(updatedCommand, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete a booking by it's ID
        /// </summary>
        /// <param name="tripId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("DeleteTrip/{tripId}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteTrip([FromRoute] string tripId, CancellationToken cancellationToken)
        {
            try
            {
                await _mediator.Send(new DeleteTripCommand { Id = tripId }, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
