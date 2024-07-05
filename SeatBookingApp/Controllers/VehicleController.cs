using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeatBookingApp.Application.Vehicles;
using SeatBookingApp.Application.Vehicles.DeleteVehicle;
using SeatBookingApp.Application.Vehicles.GetUserVehicles;
using SeatBookingApp.Application.Vehicles.GetVehicleById;
using SeatBookingApp.Application.Vehicles.GetVehicles;
using SeatBookingApp.Application.Vehicles.RegisterUserVehicle;
using SeatBookingApp.Application.Vehicles.UpdateVehicle;

namespace SeatBookingApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {

        private readonly ILogger<VehicleController> _logger;
        private readonly ISender _mediator;

        public VehicleController(ILogger<VehicleController> logger, ISender mediator)
        {
            _logger = logger;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Gets a list of all vehicles
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetVehicles")]
        [ProducesResponseType(typeof(List<VehiclesDTO>), 200)]
        public async Task<ActionResult<List<VehiclesDTO>>> GetVehicles(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetVehiclesCommand(), cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Gets a specific vehicle by it's ID
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetVehicleById")]
        [ProducesResponseType(typeof(VehiclesDTO), 200)]
        public async Task<ActionResult<VehiclesDTO>> GetVehicleById(string vehicleId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetVehicleByIdCommand { VehicleId = vehicleId }, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Gets a user's vehicles by the owner name
        /// </summary>
        /// <param name="ownerName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetUserVehicles")]
        [ProducesResponseType(typeof(List<VehiclesDTO>), 200)]
        public async Task<ActionResult<List<VehiclesDTO>>> GetUserVehicles(string ownerName, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetUserVehiclesCommand { OwnerName = ownerName}, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Register a vehicle for a specific user
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("RegisterUserVehicle")]
        [ProducesResponseType(typeof(VehiclesDTO), StatusCodes.Status201Created)]
        public async Task<ActionResult<VehiclesDTO>> RegisterUserVehicle([FromBody] RegisterUserVehicleCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Created("RegisterUserVehicle", result);
        }

        /// <summary>
        /// Updates a specific vehicle's details
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPatch("UpdateVehicle/{vehicleId}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateVehicle([FromRoute] string vehicleId, [FromBody] UpdateVehicleCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var updatedCommand = new UpdateVehicleCommand
                {
                    Id = vehicleId,
                    Make = command.Make,
                    Model = command.Model,
                    Variant = command.Variant,
                    RegistrationYear = command.RegistrationYear,
                    NumberOfSeats = command.NumberOfSeats
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
        /// Delete a vehicle by it's ID
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("DeleteVehicle/{vehicleId}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteVehicle([FromRoute] string vehicleId, CancellationToken cancellationToken)
        {
            try
            {
                await _mediator.Send(new DeleteVehicleCommand { Id = vehicleId }, cancellationToken);
                return Ok();
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
