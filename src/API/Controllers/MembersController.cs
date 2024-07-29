using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MembersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}: Guid")]
        public async Task<IActionResult> Get(Guid id) 
        {
            var query = new GetMember.Query(id);
            var member = await _mediator.Send(query);
            return Ok(member);
        }


    }
}
