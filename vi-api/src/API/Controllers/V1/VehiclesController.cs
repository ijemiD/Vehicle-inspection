namespace API.Controllers.V1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using VehicleInspection.API.Dtos;
    using VehicleInspection.API.Services;

    namespace Controllers.Controllers
    {
        [Route("api/v{v:apiVersion}/[controller]")]
        [ApiController]
        public class VehiclesController : ControllerBase
        {
            private readonly IVehicleService _vehicleService;
            public VehiclesController(IVehicleService vehicleService)
            {
                _vehicleService = vehicleService;
            }

            [HttpGet("")]
            [ProducesResponseType((int)HttpStatusCode.OK)]
            public async Task<ActionResult<List<VehicleDto>>> GetVehicles()
            {
                return Ok(await _vehicleService.GetVehicles());
            }


            [HttpGet("{id}")]
            [ProducesResponseType((int)HttpStatusCode.OK)]
            [ProducesResponseType((int)HttpStatusCode.NotFound)]
            public ActionResult<VehicleDto> GetVehicleById([FromRoute] Guid id)
            {
                var vehicle = _vehicleService.GetVehicle(id);
                if (vehicle == null)
                {
                    return NotFound();
                }
                return Ok(vehicle);
            }

            [HttpGet("{vin}")]
            [ProducesResponseType((int)HttpStatusCode.OK)]
            [ProducesResponseType((int)HttpStatusCode.NotFound)]
            public ActionResult<VehicleDto> GetVehicleById([FromRoute] string vin)
            {
                var vehicle = _vehicleService.GetVehicle(vin);
                if (vehicle == null)
                {
                    return NotFound();
                }
                return Ok(vehicle);
            }

            /*                        [HttpPost("")]
                                    public async Task<ActionResult<VehicleDto>> CreateVehicle(VehicleDto createDto)
                                    {
                                        // TODO: Your code here
                                        await Task.Yield();

                                        return null;
                                    }

                                    [HttpPut("{id}")]
                                    public async Task<IActionResult> PutTModel(int id, VehicleDto updateDto)
                                    {
                                        // TODO: Your code here
                                        await Task.Yield();

                                        return NoContent();
                                    }

                                    [HttpDelete("{id}")]
                                    public async Task<ActionResult<VehicleDto>> DeleteVehicleById(int id)
                                    {
                                        // TODO: Your code here
                                        await Task.Yield();

                                        return null;
                                    }
                                    */
        }
    }
}