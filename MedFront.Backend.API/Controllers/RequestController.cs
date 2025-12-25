using MedFront.Backend.API.Common;
using MedFront.Backend.Application.Services.Request.Commands;
using MedFront.Backend.Application.Services.Request.Queries;
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
    public class RequestsController : BaseController
    {
        private readonly IMediator _mediator;

        public RequestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<RequestDto>>> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllRequestsQuery(), cancellationToken);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<RequestDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetRequestByIdQuery(id), cancellationToken);
            return Ok(result);
        }

        [Authorize] 
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateRequestDto dto, CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(new CreateRequestCommand(dto), cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }       
    }
}
