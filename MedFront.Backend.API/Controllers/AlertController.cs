using MedFront.Backend.API.Common;
using MedFront.Backend.Application.Services.Alert.Queries;
using MedFront.Backend.Contracts.DTOs.ReadingDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedFront.Backend.API.Controllers
{
    [Authorize]
    public class AlertController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<AlertDto>>> GetAllAlerts()
        {
            var result = await Mediator.Send(new GetAllAlertsQuery());
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<AlertDto>> GetAlertById(Guid id)
        {
            var result = await Mediator.Send(new GetAlertByIdQuery(id));
            return Ok(result);
        }
    }
}
