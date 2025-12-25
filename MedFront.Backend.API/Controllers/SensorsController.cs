using MedFront.Backend.API.Common;
using MedFront.Backend.Application.Services.Sensor.Commands;
using MedFront.Backend.Application.Services.Sensor.Queries;
using MedFront.Backend.Contracts.DTOs.CreateDTOs;
using MedFront.Backend.Contracts.DTOs.ReadingDTOs;
using MedFront.Backend.Contracts.DTOs.UpdateDTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedFront.Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SensorsController : BaseController
    {
        private readonly IMediator _mediator;

        public SensorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<SensorDto>>> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllSensorsQuery(), cancellationToken);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SensorDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetSensorByIdQuery(id), cancellationToken);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("warehouse/{warehouseId:guid}")]
        public async Task<ActionResult<List<SensorDto>>> GetByWarehouse(Guid warehouseId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetSensorsByWarehouseQuery(warehouseId), cancellationToken);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] SensorCreateDto dto, CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(new CreateSensorCommand(dto), cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{id:guid}/thresholds")]
        public async Task<IActionResult> UpdateThresholds(Guid id, [FromBody] SensorThresholdUpdateDto dto, CancellationToken cancellationToken)
        {
            await _mediator.Send(new UpdateSensorThresholdsCommand(id, dto), cancellationToken);
            return NoContent();
        }
    }
}
