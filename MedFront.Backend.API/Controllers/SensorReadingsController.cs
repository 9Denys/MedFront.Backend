using MedFront.Backend.API.Common;
using MedFront.Backend.Application.Services.Sensor.Commands;
using MedFront.Backend.Application.Services.Sensor.Queries;
using MedFront.Backend.Contracts.DTOs.CreateDTOs;
using MedFront.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedFront.Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SensorReadingsController : BaseController
    {
        private readonly IMediator _mediator;

        public SensorReadingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] SensorReadingCreateDto dto, CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(new CreateSensorReadingCommand(dto), cancellationToken);
            return Ok(id);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{sensorId:guid}")]
        public async Task<ActionResult<List<SensorReadingDto>>> GetBySensor(
            Guid sensorId,
            [FromQuery] DateTime? fromUtc,
            [FromQuery] DateTime? toUtc,
            [FromQuery] int? take,
            CancellationToken cancellationToken)
        {
            var filter = new SensorReadingQueryDto
            {
                FromUtc = fromUtc,
                ToUtc = toUtc,
                Take = take
            };

            var result = await _mediator.Send(new GetSensorReadingsQuery(sensorId, filter), cancellationToken);
            return Ok(result);
        }
    }
}
