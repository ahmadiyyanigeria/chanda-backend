using Application.Commands;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ChandaTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChandaTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}: Guid")]
        public async Task<IActionResult> Get(Guid id)
        {
            var query = new GetChandaType.Query(id);
            var chandaType = await _mediator.Send(query);
            return Ok(chandaType);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(bool usePaging)
        {
            var query = new GetChandaTypes.Query(usePaging);
            var chandaType = await _mediator.Send(query);
            return Ok(chandaType);
        }

        [HttpPost("SeedData")]
        public async Task<IActionResult> SeedData([FromBody] SeedDatas.Command commad)
        {
            var res = await _mediator.Send(commad);
            return Ok(res);
        }
    }
}
