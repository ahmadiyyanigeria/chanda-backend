using Application.Commands;
using Application.DTOs;
using Application.Queries;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RemindersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RemindersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> SetReminder([FromBody] CreateReminder.Command command)
        {
            var reminder = await _mediator.Send(command);
            return Ok(reminder);
        }

        [HttpGet("{memberId}")]
        public async Task<IActionResult> GetMemberReminders(Guid memberId)
        {
            var query = new GetReminders.Query(memberId);
            var reminders = await _mediator.Send(query);
            return Ok(reminders);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateReminder([FromBody] UpdateReminderDto dto)
        {
            var command = dto.Adapt<UpdateReminder.Command>();
            var reminder = await _mediator.Send(command);
            return Ok(reminder);
        }

        [HttpPatch("TurnOn/{id}")]
        public async Task<IActionResult> TurnOnReminder(Guid id)
        {
            var command = new TurnReminderOn_Off.Command(id, true);
            var reminder = await _mediator.Send(command);
            return Ok(reminder);
        }

        [HttpPatch("TurnOff/{id}")]
        public async Task<IActionResult> TurnOffReminder(Guid id)
        {
            var command = new TurnReminderOn_Off.Command(id, false);
            var reminder = await _mediator.Send(command);
            return Ok(reminder);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteReminder(Guid id)
        {
            var command = new DeleteReminder.Command(id);
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
