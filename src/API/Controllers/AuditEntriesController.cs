using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuditEntriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuditEntriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuditEntries([FromQuery] GetAuditEntries.Query query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
