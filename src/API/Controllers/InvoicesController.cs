using Application.Commands;
using Application.DTOs;
using Application.Queries;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InvoicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvoice([FromBody] InvoiceRequest request)
        {
            var command = request.Adapt<CreateInvoice.Command>();
            var invoice = await _mediator.Send(command);
            return Ok(invoice);
        }

        [HttpPost("group")]
        public async Task<IActionResult> CreateGroupInvoice([FromBody] GroupInvoiceRequest request)
        {
            var command = request.Adapt<CreateGroupInvoice.Command>();
            var invoice = await _mediator.Send(command);
            return Ok(invoice);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoice(Guid id)
        {
            var query = new GetInvoice.Query(id);
            var invoice = await _mediator.Send(query);
            return Ok(invoice);
        }

       
    }
}
