using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CircuitsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CircuitsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}: Guid")]
        public async Task<IActionResult> Get(Guid id)
        {
            var query = new GetCircuit.Query(id);
            var circuit = await _mediator.Send(query);
            return Ok(circuit);
        }
    }
}
