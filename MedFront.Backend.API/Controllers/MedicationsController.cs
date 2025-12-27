using MedFront.Backend.API.Common;
using MedFront.Backend.Application.Services.Medication.Commands;
using MedFront.Backend.Application.Services.Medication.Queries;
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
    public class MedicationsController : BaseController
    {
        private readonly IMediator _mediator;

        public MedicationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<MedicationDto>>> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllMedicationsQuery(), cancellationToken);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<MedicationDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetMedicationByIdQuery(id), cancellationToken);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] MedicationCreateDto dto, CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(new CreateMedicationCommand(dto), cancellationToken);
            return Ok(id);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] MedicationUpdateDto dto, CancellationToken cancellationToken)
        {
            await _mediator.Send(new UpdateMedicationCommand(id, dto), cancellationToken);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteMedicationCommand(id), cancellationToken);
            return NoContent();
        }
    }
}
