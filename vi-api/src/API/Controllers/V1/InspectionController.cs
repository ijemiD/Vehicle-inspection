namespace VehicleInspection.API.Controllers.V1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;
    using VehicleInspection.API.Dtos;
    using VehicleInspection.API.Services;

    [Route("api/v{version:apiVersion}")]
    [ApiController]
    public class InspectionsController : ControllerBase
    {
        private readonly IInspectionService _inspectionService;

        public InspectionsController(IInspectionService inspectionService)
        {
            _inspectionService = inspectionService;
        }

        [HttpGet("vehicles/{vehicleId}/[controller]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetInspections([FromRoute] Guid vehicleId)
        {
            var inspections = await _inspectionService.GetInspectionsByVehicleAsync(vehicleId);
            if (!inspections.Any())
            {
                return NotFound();
            }

            return Ok(inspections);
        }

        [HttpGet("vehicles/{vin}/[controller]")]
        public async Task<ActionResult> GetInspections([FromRoute] string vin)
        {
            var inspections = await _inspectionService.GetInspectionsByVehicleAsync(vin);
            if (!inspections.Any())
            {
                return NotFound();
            }

            return Ok(inspections);
        }

        /*
        [HttpGet("vehicles/{vin}/[controller]/{id}")]
        public async Task<ActionResult<InspectionDto>> GetInspectionById(int id)
        {
            //TODO

            return null;
        }

        [HttpPost("")]
        public async Task<ActionResult<InspectionDto>> CreateInspection(InspectionDto createDto)
        {

            //TODO

            return null;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInspection(int id, InspectionDto updateDto)
        {
            //TODO


            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<InspectionDto>> DeleteInspectionById(int id)
        {
            // TODO
            return null;
        }
        */
    }
}