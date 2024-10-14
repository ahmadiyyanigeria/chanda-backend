using Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RemindersController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public RemindersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> SetReminder([FromBody] CreateReminder.Command command)
        {
            var reminder = await _mediator.Send(command);
            return Ok(reminder);
        }
    }
}
