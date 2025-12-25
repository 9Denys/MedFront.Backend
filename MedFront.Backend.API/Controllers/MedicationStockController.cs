using MedFront.Backend.API.Common;
using MedFront.Backend.Application.Services.MedicationStock.Commands;
using MedFront.Backend.Application.Services.MedicationStock.Queries;
using MedFront.Backend.Contracts.DTOs.CreateDTOs;
using MedFront.Backend.Contracts.DTOs.MedicationStock;
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
    public class MedicationStockController : BaseController
    {
        private readonly IMediator _mediator;

        public MedicationStockController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("by-warehouse/{warehouseId:guid}")]
        public async Task<ActionResult<List<MedicationStockDto>>> GetByWarehouse(Guid warehouseId, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetMedicationStocksByWarehouseQuery(warehouseId), ct);
            return Ok(result);
        }

        [HttpGet("{stockId:guid}")]
        public async Task<ActionResult<MedicationStockDto>> GetById(Guid stockId, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetMedicationStockByIdQuery(stockId), ct);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateMedicationStockDto dto, CancellationToken ct)
        {
            var id = await _mediator.Send(new CreateMedicationStockCommand(dto), ct);
            return Ok(id);
        }

        [HttpPut("{stockId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid stockId, [FromBody] UpdateMedicationStockDto dto, CancellationToken ct)
        {
            await _mediator.Send(new UpdateMedicationStockCommand(stockId, dto), ct);
            return NoContent();
        }

        [HttpPost("{stockId:guid}/write-off")]
        public async Task<IActionResult> WriteOff(Guid stockId, [FromBody] MedicationStockWriteOffDto dto, CancellationToken ct)
        {
            await _mediator.Send(new WriteOffMedicationStockCommand(stockId, dto.Quantity), ct);
            return NoContent();
        }

        [HttpDelete("{stockId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid stockId, CancellationToken ct)
        {
            await _mediator.Send(new DeleteMedicationStockCommand(stockId), ct);
            return NoContent();
        }
    }
}
