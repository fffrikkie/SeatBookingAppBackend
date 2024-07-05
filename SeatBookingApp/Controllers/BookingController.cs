using Microsoft.AspNetCore.Mvc;
using MediatR;
using SeatBookingApp.Application.Bookings.GetBookings;
using SeatBookingApp.Application.Bookings;
using SeatBookingApp.Application.Bookings.RegisterTripBooking;
using SeatBookingApp.Application.Bookings.DeleteBooking;
using SeatBookingApp.Application.Bookings.UpdateBooking;
using SeatBookingApp.Application.Bookings.GetBookingById;
using SeatBookingApp.Application.Bookings.GetTripBookings;

namespace SeatBookingApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly ILogger<BookingController> _logger;
        private readonly ISender _mediator;

        public BookingController(ILogger<BookingController> logger, ISender mediator)
        {
            _logger = logger;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        ///  Gets a list of all bookings
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>asdasds</returns>
        [HttpGet("GetBookings")]
        [ProducesResponseType(typeof(List<BookingsDTO>), 200)]
        public async Task<ActionResult<List<BookingsDTO>>> GetBookings(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetBookingsCommand(), cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Gets a specific booking by it's ID
        /// </summary>
        /// <param name="bookingId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetBookingById")]
        [ProducesResponseType(typeof(BookingsDTO), 200)]
        public async Task<ActionResult<BookingsDTO>> GetBookingById(string bookingId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetBookingByIdCommand{ Id = bookingId }, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Gets a trip's bookings by the trip ID
        /// </summary>
        /// <param name="tripId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetTripBookings")]
        [ProducesResponseType(typeof(TripBookingsDTO), 200)]
        public async Task<ActionResult<TripBookingsDTO>> GetTripBookings(string tripId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetTripBookingsCommand { TripId = tripId }, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Register a booking for a specific trip
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("RegisterTripBooking")]
        [ProducesResponseType(typeof(BookingsDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BookingsDTO>> RegisterTripBooking([FromBody] RegisterTripBookingCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mediator.Send(command, cancellationToken);
                return CreatedAtAction("RegisterTripBooking", result);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates a specific booking's details
        /// </summary>
        /// <param name="bookingId"></param>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPatch("UpdateBooking/{bookingId}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateBooking([FromRoute] string bookingId, [FromBody] UpdateBookingCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var updatedCommand = new UpdateBookingCommand
                {
                    Id = bookingId,
                    TravellerName = command.TravellerName,
                    NumberOfSeatsBooked = command.NumberOfSeatsBooked,
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
        /// Delete a booking by it's ID.
        /// </summary>
        /// <param name="bookingId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("DeleteBooking/{bookingId}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteBooking([FromRoute] string? bookingId, CancellationToken cancellationToken)
        {
            try
            {
                await _mediator.Send(new DeleteBookingCommand { Id = bookingId }, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
