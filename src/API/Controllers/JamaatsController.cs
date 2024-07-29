using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class JamaatsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JamaatsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetByCircuit(Guid? circuitId, bool usePaging)
        {
            var query = new GetJamaats.Query(circuitId, usePaging);
            var jamaats = await _mediator.Send(query);
            return Ok(jamaats);
        }

        [HttpGet("{id}: Guid")]
        public async Task<IActionResult> GetJamaat(Guid id)
        {
            var query = new GetJamaat.Query(id);
            var jamaat = await _mediator.Send(query);
            return Ok(jamaat);
        }
    }
}
