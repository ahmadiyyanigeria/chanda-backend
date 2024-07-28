using Application.Commands;
using Domain.Dtos;
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
        public async Task<IActionResult> CretateInvoice([FromBody] CreateInvoiceRequest request)
        {
            var command = request.Adapt<CreateInvoice.Command>();
            var invoice = await _mediator.Send(command);
            return Ok(invoice);
        }
    }
}
