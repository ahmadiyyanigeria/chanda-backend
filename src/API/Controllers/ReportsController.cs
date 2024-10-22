using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("member")]
        public async Task<IActionResult> GetMemberReport([FromQuery] GetMemberReport.Query query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("jamaat")]
        public async Task<IActionResult> GetJamaatReport([FromQuery] GetJamaatReport.Query query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("jamaatMembers")]
        public async Task<IActionResult> GetJamaatMembersReport([FromQuery] GetJamaatMembersReport.Query query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("circuit")]
        public async Task<IActionResult> GetCircuitReport([FromQuery] GetCircuitReport.Query query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("circuitJamaats")]
        public async Task<IActionResult> GetCircuitJamaatsReport([FromQuery] GetCircuitJamaatsReport.Query query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("overrall")]
        public async Task<IActionResult> GetOverrallSummary([FromQuery] GetOverrallSummary.Query query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("jamaatDefaulters")]
        public async Task<IActionResult> GetJamaatDefaulters([FromQuery] GetJamaatDefaulters.Query query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("circuitDefaulters")]
        public async Task<IActionResult> GetCircuitDefaulters([FromQuery] GetCircuitDefaulters.Query query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
