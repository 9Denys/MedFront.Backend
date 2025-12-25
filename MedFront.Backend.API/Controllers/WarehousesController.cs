using MedFront.Backend.API.Common;
using MedFront.Backend.Application.Services.Warehouse.Commands;
using MedFront.Backend.Application.Services.Warehouse.Queries;
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
    public class WarehousesController : BaseController
    {
        private readonly IMediator _mediator;

        public WarehousesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<WarehouseDto>>> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllWarehousesQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<WarehouseDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetWarehouseByIdQuery(id), cancellationToken);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] WarehouseCreateDto dto, CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(new CreateWarehouseCommand(dto), cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] WarehouseUpdateDto dto, CancellationToken cancellationToken)
        {
            await _mediator.Send(new UpdateWarehouseCommand(id, dto), cancellationToken);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteWarehouseCommand(id), cancellationToken);
            return NoContent();
        }
        /* */
        [Authorize]
        [HttpGet("my")]
        public async Task<ActionResult<List<WarehouseDto>>> GetMy(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetMyWarehousesQuery(), cancellationToken);
            return Ok(result);
        }
    }
}
