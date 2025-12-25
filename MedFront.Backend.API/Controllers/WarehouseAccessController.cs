using MedFront.Backend.Application.Services.WarehouseAccess.Commands;
using MedFront.Backend.Application.Services.WarehouseAccess.Queries;
using MedFront.Backend.Contracts.DTOs.CreateDTOs;
using MedFront.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedFront.Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class WarehouseAccessController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WarehouseAccessController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Grant([FromBody] WarehouseAccessCreateDto dto, CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(new GrantWarehouseAccessCommand(dto), cancellationToken);
            return Ok(id);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Revoke(Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new RevokeWarehouseAccessCommand(id), cancellationToken);
            return NoContent();
        }

        [HttpGet("by-warehouse/{warehouseId:guid}")]
        public async Task<ActionResult<List<WarehouseAccessDto>>> GetByWarehouse(Guid warehouseId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAccessByWarehouseQuery(warehouseId), cancellationToken);
            return Ok(result);
        }
    }
}
